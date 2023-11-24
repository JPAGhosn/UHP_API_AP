using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using UHP.Application.Exceptions;
using UHP.WebApi.ActionFilters.Models;

namespace UHP.WebApi.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UHPExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<UHPExceptionFilterAttribute> _logger;
        public UHPExceptionFilterAttribute(ILogger<UHPExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            var code = context.Exception switch
            {
                NotFoundException _ => HttpStatusCode.NotFound,
                UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int) code;
            var errors = new List<string>();
            var innerException = context.Exception.InnerException;
            while (innerException != null)
            {
                errors.Add(innerException.Message);
                innerException = innerException.InnerException;
            }

            context.Result = new JsonResult(new ExceptionResponse
            {
                Code = (int) code,
                Message = new List<string>() {context.Exception.Message},
                Error = errors,
                StackTrace = context.Exception.StackTrace
            });
        }
    }
}
