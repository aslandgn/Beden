using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceExtensions
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly ILogger<LogMiddleware> _logger;
        public LogMiddleware(RequestDelegate request, ILogger<LogMiddleware> logger)
        {
            _request = request;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            bool hasError = false;
            var stopWatch = Stopwatch.StartNew();

            var request = httpContext.Request;
            var clientIp = request.HttpContext.Connection.RemoteIpAddress.ToString();
            var requestTime = DateTime.UtcNow;
            var response = httpContext.Response;
            var originalBodyStream = httpContext.Response.Body;
            var requestHeader = new StringBuilder("{");
            string responseBodyContent = "";
            var requestBodyContent = "";
            try
            {

                requestBodyContent = await ReadRequestBody(request);
                using var responseBody = new MemoryStream();
                request.Headers.Select(x => "\"" + x.Key + "\": \"" + x.Value + "\",").ToList().ForEach(x => requestHeader.Append(x.ToString() + "\n"));
                requestHeader.Append("}");
                response.Body = responseBody;
                await _request(httpContext);
                stopWatch.Stop();
                responseBodyContent = await ReadResponseBody(response);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                using var responseBody = new MemoryStream();
                response.Body = responseBody;
                responseBodyContent = await ReadResponseBody(response);
                await responseBody.CopyToAsync(originalBodyStream);
                _logger.LogError(ex, "requestTime => {requestTime} \t  ElapsedMilliseconds => {ElapsedMilliseconds} \t StatusCode =>{StatusCode} \t Method => {Method} \t clientIp => {clientIp} \n request => {request} \n response => {response}",
                    requestTime, stopWatch.ElapsedMilliseconds, response.StatusCode, request.Path, clientIp, requestBodyContent, responseBodyContent);
            }
            finally
            {
                if (!hasError)
                {
                    _logger.LogInformation("requestTime => {requestTime} \t  ElapsedMilliseconds => {ElapsedMilliseconds} \t StatusCode =>{StatusCode} \t Method => {Method} \t clientIp => {clientIp} \n request => {request} \n response => {response}",
                        requestTime, stopWatch.ElapsedMilliseconds, response.StatusCode, request.Path, clientIp, requestBodyContent, responseBodyContent);
                }
            }
        }

        private static async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }
        private static async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length));
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }
    }
}