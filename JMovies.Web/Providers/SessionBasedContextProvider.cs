using JMovies.Entities;
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
        private Entities.Context temporaryContext;
        public Entities.Context GetContext()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["Context"] != null)
            {
                return HttpContext.Current.Session["Context"] as Entities.Context;
            }
            else if (HttpContext.Current != null && HttpContext.Current.Items != null && HttpContext.Current.Items["Context"] != null)
            {
                return HttpContext.Current.Items["Context"] as Entities.Context;
            }
            else
            {
                return temporaryContext;
            }
        }

        public void SetContext(Entities.Context context)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session["Context"] = context;
            }
            else if (HttpContext.Current != null && HttpContext.Current.Items != null)
            {
                HttpContext.Current.Items["Context"] = context;
            }
            else
            {
                temporaryContext = context;
            }
        }
    }
}
