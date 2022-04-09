using System;
using System.Net;
using FluentValidation;
using System.Threading.Tasks;
using BlogEngineApp.core;
using core.models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace services.middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var httpStatusCode = GetHttpStatusCode(ex);
            var httpStatusCodeNumber = (int)httpStatusCode;
            var exceptionCaught = GetErrorDetailFromException(httpStatusCodeNumber, ex);

            _logger.LogError("ExceptionMiddleware: {exceptionCaught}", exceptionCaught);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = httpStatusCodeNumber;
            return httpContext.Response.WriteAsync(exceptionCaught.RemoveDetail().ToString());
        }

        private static HttpStatusCode GetHttpStatusCode(Exception ex)
        {
            if (ex is NotFoundResponseException)
                return HttpStatusCode.NotFound;
            if (ex is BadRequestResponseException)
                return HttpStatusCode.BadRequest;
            if (ex is UnauthorizedAccessException)
                return HttpStatusCode.Unauthorized;
            return HttpStatusCode.InternalServerError;
        }

        private static ErrorDetail GetErrorDetailFromException(int httpStatusCodeNumber, Exception ex)
        {
            string message = ex.Message;
            if (ex is ValidationException)
            {
                var validationException = ex as ValidationException;
                message = string.Join('|', validationException.Errors);
            }

            return new ErrorDetail
            {
                StatusCode = httpStatusCodeNumber,
                Message = message,
                InnerException = ex.InnerException?.Message,
                TargetSite = ex.TargetSite.ToString(),
                Source = ex.Source,
                Detail = ex.StackTrace
            };
        }
    }
}
