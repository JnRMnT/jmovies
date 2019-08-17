using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JMovies.Controllers
{
    public class DefaultController : BaseApiController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
