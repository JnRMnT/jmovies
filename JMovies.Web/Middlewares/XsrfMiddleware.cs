using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Web.Middlewares
{
    public class XsrfMiddleware
    {
        private RequestDelegate requestDelegate;
        private IAntiforgery antiforgery;
        public XsrfMiddleware(RequestDelegate requestDelegate, IAntiforgery antiforgery)
        {
            this.requestDelegate = requestDelegate;
            this.antiforgery = antiforgery;
        }

        public async Task Invoke(HttpContext context)
        {
            string path = context.Request.Path.Value;

            if (
                string.Equals(path, "/api/resources", StringComparison.OrdinalIgnoreCase))
            {
                // The request token can be sent as a JavaScript-readable cookie, 
                // and Angular uses it by default.
                var tokens = antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                    new CookieOptions() { HttpOnly = false });
            }
            await requestDelegate.Invoke(context);
        }
    }
}
