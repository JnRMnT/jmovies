using JMovies.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JMovies.Web.Providers
{
    public class SessionBasedContextProvider : IContextProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private Entities.Context temporaryContext;

        public SessionBasedContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Context GetContext()
        {
            if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.Session != null && httpContextAccessor.HttpContext.Session.Keys.Any(e => e == "Context"))
            {
                return httpContextAccessor.HttpContext.Session.GetObject<Context>("Context");
            }
            else if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.Items != null && httpContextAccessor.HttpContext.Items["Context"] != null)
            {
                return httpContextAccessor.HttpContext.Items["Context"] as Entities.Context;
            }
            else
            {
                return temporaryContext;
            }
        }

        public void SetContext(Entities.Context context)
        {
            if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.Session != null)
            {
                httpContextAccessor.HttpContext.Session.SetObject("Context", context);
            }
            else if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.Items != null)
            {
                httpContextAccessor.HttpContext.Items["Context"] = context;
            }
            else
            {
                temporaryContext = context;
            }
        }
    }
}
