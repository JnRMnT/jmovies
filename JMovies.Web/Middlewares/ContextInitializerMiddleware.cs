using JMovies.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Web.Middlewares
{
    public class ContextInitializerMiddleware
    {
        private RequestDelegate requestDelegate;
        private IContextProvider contextProvider;
        public ContextInitializerMiddleware(RequestDelegate requestDelegate, IContextProvider contextProvider)
        {
            this.requestDelegate = requestDelegate;
            this.contextProvider = contextProvider;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Context context = contextProvider.GetContext();
            if (context == null)
            {
                context = new Context();
                contextProvider.SetContext(context);
            }
            await requestDelegate.Invoke(httpContext);
        }
    }
}
