using JMovies.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.App.Business.Context
{
    public class ContextProvider : IContextProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Entities.Context GetContext()
        {
            return httpContextAccessor.HttpContext.Items["Context"] as Entities.Context;
        }

        public void SetContext(Entities.Context context)
        {
            httpContextAccessor.HttpContext.Items["Context"] = context;
        }
    }
}
