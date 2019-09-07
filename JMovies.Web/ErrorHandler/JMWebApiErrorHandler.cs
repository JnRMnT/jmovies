using JM.Entities.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Net.Http;

namespace JMovies.Web.UI.ErrorHandler
{
    public class JMExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            JMException customException = null;
            if (context.Exception is JMException)
            {
                // handle custom exception
                var contextCustomException = context.Exception as JMException;
                context.Exception = null;
                customException = contextCustomException;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                context.HttpContext.Response.StatusCode = 401;
                // handle logging here
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

                //customException.detail = callStack;
                //customException = new ApiError(errorMessage);

                context.HttpContext.Response.StatusCode = 500;

                // handle logging here
            }

            //always return a JSON result
            context.Result = new JsonResult(customException);
            base.OnException(context);
        }
    }
}