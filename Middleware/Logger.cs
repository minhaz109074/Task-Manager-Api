using Task_Manager_Api.Repositories;
using Task_Manager_Api.Models;
using Task_Manager_Api.Services;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System;
using Task_Manager_Api.Exceptions;
using System.Text.Json;
using Task_Manager_Api.DTOs.ErrorDetails;

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

            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
                await logService.CreateExceptionLog(ex);
            }
            
            dynamic responseLogs = ResponceLoggingInfo(context);
            
            await logService.CreateRequestResponseLog(requestLogs, responseLogs);
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

        private async Task HandleException(HttpContext context, Exception exception)
        {
            
            context.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                RecordAlreadyExistException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
            var response = new ErrorDetailsDto
            {
                StatusCode = context.Response.StatusCode,
                IsSuccess = false,
                ErrorMessage = exception.Message

            };
            var jsonResponse = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
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
