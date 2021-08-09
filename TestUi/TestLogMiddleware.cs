using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUi
{
    internal class TestLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TestLogMiddleware> _logger;
        public TestLogMiddleware(RequestDelegate next, ILogger<TestLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var stopWatch = Stopwatch.StartNew();

            var request = httpContext.Request;
            var clientIp = request.HttpContext.Connection.RemoteIpAddress.ToString();
            var requestTime = DateTime.UtcNow;
            var response = httpContext.Response;
            var originalBodyStream = httpContext.Response.Body;
            var requestHeader = new StringBuilder("{");
            string responseBodyContent = string.Empty;
            var requestBodyContent = string.Empty;
            try
            {
                requestBodyContent = await ReadRequestBodyAsync(request);
                using var responseBody = new MemoryStream();
                request.Headers.Select(x => "\"" + x.Key + "\": \"" + x.Value + "\",").ToList().ForEach(x => requestHeader.Append(x.ToString() + "\n"));
                requestHeader.Append("}");
                response.Body = responseBody;
                await _next(httpContext);
                stopWatch.Stop();


                responseBodyContent = await ReadResponseBodyAsync(response);
                await responseBody.CopyToAsync(originalBodyStream);

                _logger.LogInformation("Method => {method}, Request => {request}, Response => {response}, Elapsed Time {elapsedTime}", request.Path, requestBodyContent, responseBodyContent, stopWatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                using var responseBody = new MemoryStream();
                response.Body = responseBody;
                responseBodyContent = await ReadResponseBodyAsync(response);
                await responseBody.CopyToAsync(originalBodyStream);

                _logger.LogError("Method => {method}, Request => {request}, Response => {response}, Elapsed Time {elapsedTime}, Error => {error}", request.Path, requestBodyContent, responseBodyContent, stopWatch.ElapsedMilliseconds, ex);

            }
        }

        private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        private static async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }
    }
}
