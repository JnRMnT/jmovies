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
                return null;
            }
        }

        public void SetContext(Entities.Context context)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session["Context"] = context;
            }
            else
            {
                HttpContext.Current.Items["Context"] = context;
            }
        }
    }
}
