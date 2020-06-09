using System;
using System.Threading.Tasks;
using DataCore.Exceptions;
using Entity.Models.Dtos;
using Logger;
using Microsoft.AspNetCore.Http;
using StudentPerformanceControl.Models;

namespace StudentPerformanceControl.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogService logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var spcException = exception as SPCException;
            var statusCode = StatusCodes.Status500InternalServerError;
            var message = exception.Message;

            if (spcException != null)
            {
                statusCode = spcException.StatusCode;
            }
            
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorResponse((int) statusCode, message));
            return context.Response.WriteAsync(json);
        }
    }
}