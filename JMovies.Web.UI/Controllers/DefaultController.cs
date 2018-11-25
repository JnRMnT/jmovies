using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace JMovies.Web.UI.Controllers
{
    public class DefaultController : Controller
    {
        public FileResult Index()
        {
            return new FilePathResult("~/App/index.html", "text/html");
        }
    }
}