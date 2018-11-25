using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JMovies.Web.UIControllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}