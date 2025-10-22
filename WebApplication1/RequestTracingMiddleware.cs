using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;

namespace WebApplication1
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestTracingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTracingMiddleware> _logger;

        public RequestTracingMiddleware(RequestDelegate next, ILogger<RequestTracingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Handling request: " + httpContext.Request.GetDisplayUrl());
            httpContext.Items["RequestID"] = Guid.NewGuid().ToString();

            httpContext.Response.OnStarting(() => {
                if (httpContext.Items.ContainsKey("RequestID"))
                {
                    httpContext.Response.Headers.Add("X-Request-ID", httpContext.Items["RequestID"].ToString());
                }
                return Task.CompletedTask;
            });

            _logger.LogInformation("Finished handling request.");
            return _next(httpContext);          
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestTracingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestTracingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestTracingMiddleware>();
        }
    }
}
