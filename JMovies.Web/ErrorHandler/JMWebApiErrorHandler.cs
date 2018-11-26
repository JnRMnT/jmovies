using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace JMovies.Web.UI.ErrorHandler
{
    public class JMWebApiErrorHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
        }
    }
}