using Task_Manager_Api.Repositories;
using Task_Manager_Api.Models;
using Task_Manager_Api.Services;
using Microsoft.AspNetCore.Diagnostics;

namespace Task_Manager_Api.Middleware
{
    public class Logger
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public Logger(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<Logger>();
           
        }

        public async Task Invoke(HttpContext context, ILogService logService)
        {
            dynamic requestLogs = RequestLoggingInfo(context);
            await _next(context);
            dynamic responseLogs = ResponceLoggingInfo(context);
            
            logService?.CreateRequestResponseLog(requestLogs, responseLogs);
        }

        private Dictionary<string, dynamic> RequestLoggingInfo(HttpContext context)
        {
            
            var logs = new Dictionary<string, dynamic>();
            logs["RequestMethod"] = context.Request!.Method;
            logs["RequestPath"] = context.Request.Path.Value ?? String.Empty;
            logs["RequestArriveTime"] = DateTime.Now;
            
            return logs;
        }

        private Dictionary<string, dynamic> ResponceLoggingInfo(HttpContext context)
        {
            
            var logs = new Dictionary<string, dynamic>();
            logs["StatusCode"] = context.Response!.StatusCode;
            logs["RequestLeaveTime"] = DateTime.Now;

            return logs;
        }

    }
    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomRequestResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Logger>();
        }
    }
}
