using JMovies.Web.Filters;
using JMovies.Web.UI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace JMovies.Web.Controllers
{
    [JMExceptionFilter]
    [ResponseActionFilter]

    public class BaseApiController : Controller
    {

    }
}
