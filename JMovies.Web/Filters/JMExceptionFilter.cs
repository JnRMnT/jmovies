using JM.Entities;
using JM.Entities.Framework;
using JM.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.Extensions.DependencyInjection;
using JMovies.Utilities.Logging;

namespace JMovies.Web.UI.Filters
{
    public class JMExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is AggregateException)
            {
                AggregateException aggregateException = context.Exception as AggregateException;
                foreach (Exception exception in aggregateException.InnerExceptions)
                {
                    DefaultLogger.Error(exception);
                }
                context.Exception = aggregateException.InnerException;
            }

            if (context.Exception is UnauthorizedAccessException)
            {
                context.HttpContext.Response.StatusCode = 401;
            }
            else
            {
                // Unhandled errors
#if !DEBUG
                var errorMessage = "An unhandled error occurred.";                
                string callStack = null;
#else
                var errorMessage = context.Exception.GetBaseException().Message;
                string callStack = context.Exception.StackTrace;
#endif

                context.HttpContext.Response.StatusCode = 500;
            }
            // handle logging here
            JMResult result = context.HttpContext.RequestServices.GetRequiredService<IExceptionHandler>().HandleException(context.Exception);
            //always return a JSON result
            context.Result = new JsonResult(result);
            base.OnException(context);
        }
    }
}