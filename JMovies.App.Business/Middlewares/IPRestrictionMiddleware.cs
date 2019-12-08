using JMovies.App.Business.Configuration;
using JMovies.Utilities.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JMovies.App.Business.Middlewares
{
    public class IPRestrictionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppConfiguration options;
        public IPRestrictionMiddleware(RequestDelegate next, IOptions<AppConfiguration> applicationOptionsAccessor)
        {
            _next = next;
            options = applicationOptionsAccessor.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress;
            string[] whiteListIPList = options.WhiteList;

            var isInwhiteListIPList = whiteListIPList
                .Where(a => IPAddress.Parse(a)
                .Equals(ipAddress))
                .Any();
            if (!isInwhiteListIPList)
            {
                DefaultLogger.Warn($"{ipAddress} is not an allowed IP Address!");
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }
            await _next.Invoke(context);
        }
    }
}
