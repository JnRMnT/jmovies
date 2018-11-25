using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(JMovies.Web.UI.Startup))]

namespace JMovies.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
