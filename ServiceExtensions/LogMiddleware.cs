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
                _logger.LogError(ex, "!!!An error occoured!!! \n ElapsedTime => {requestTime} " +
                    "\n statusCode => {StatusCode} \n method => {method} \n path => {Path}" +
                    "\n queryString => {QueryString}, \n requestHeader => {requestHeader}" +
                    "\n requestBodyContent => {requestBodyContent}",
                    "\n responseBodyContent => {responseBodyContent}",
                    "\n clientIp => {clientIp}",
                    requestTime,
                    stopWatch.ElapsedMilliseconds,
                    response.StatusCode,
                    request.Method,
                    request.Path,
                    request.QueryString.ToString(),
                    requestHeader.ToString(),
                    requestBodyContent,
                    responseBodyContent,
                    clientIp);
            }
            finally
            {
                if (!hasError)
                {
                    _logger.LogInformation(
                        "info \n" +
                        requestTime + "\n" +
                        stopWatch.ElapsedMilliseconds + "\n" +
                        response.StatusCode + "\n" +
                        request.Method + "\n" +
                        request.Path + "\n" +
                        request.QueryString.ToString() + "\n" +
                        //requestHeader.ToString() + "\n" +
                        requestBodyContent + "\n" +
                        responseBodyContent + "\n" +
                        clientIp);
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



/*
using FUZULEV.PRIM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUZULEV.PRIM
{
    public class TokenCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private ContextBase _context;
        public TokenCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ContextBase context)
        {
            var stopWatch = Stopwatch.StartNew();
            _context = context;

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
                
                if (request.Path.StartsWithSegments(new PathString("/api")))
                {
                    
                    requestBodyContent = await ReadRequestBody(request);
                    using (var responseBody = new MemoryStream())
                    {
                        request.Headers.Select(x => "\"" + x.Key + "\": \"" + x.Value + "\",").ToList().ForEach(x => requestHeader.Append(x.ToString() + "\n"));
                        requestHeader.Append("}");
                        response.Body = responseBody;
                        await _next(httpContext);
                        stopWatch.Stop();

                       
                        responseBodyContent = await ReadResponseBody(response);
                        await responseBody.CopyToAsync(originalBodyStream);

                        


                        LogParameters(requestTime,
                           stopWatch.ElapsedMilliseconds,
                           response.StatusCode,
                           request.Method,
                           request.Path,
                           request.QueryString.ToString(),
                           requestHeader.ToString(),
                           requestBodyContent,
                           responseBodyContent,
                           clientIp
                           );
                    }
                }
                else
                {
                    await _next(httpContext);
                }
            }
            catch (Exception ex)
            {
                //await _next(httpContext);
                stopWatch.Stop();
                using (var responseBody = new MemoryStream())
                {
                    response.Body = responseBody;
                    responseBodyContent = await ReadResponseBody(response);
                    await responseBody.CopyToAsync(originalBodyStream);
                    LogParameters(requestTime,
                           stopWatch.ElapsedMilliseconds,
                           response.StatusCode,
                           request.Method,
                           request.Path,
                           request.QueryString.ToString(),
                           requestHeader.ToString(),
                           requestBodyContent,
                           responseBodyContent,
                           clientIp
                           );
                }
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        private void LogParameters(DateTime requestTime, long responseMillis, int statusCode, string method, string path, string queryString, string requestHeader, string requestBody, string responseBody, string clientIp)
        {
            //if (path.ToLower().StartsWith("/api/login"))
            //{
            //    requestBody = "(Request logging disabled for /api/login)";
            //    responseBody = "(Response logging disabled for /api/login)";
            //}

            var log = new Logs
            {
                ELAPSED_TIME = responseMillis,
                REQUEST_TIME = requestTime,
                STATUS_CODE = statusCode,
                METHOD = method,
                PATH = path,
                QUERY_STRING = queryString,
                REQUEST_HEADER = requestHeader,
                REQUEST_BODY = requestBody,
                RESPONSE_BODY = responseBody,
                CLIENT_IP = clientIp,
            };
            var addedEntity = _context.Entry(log);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();
        }
    }
}
 
 
 */