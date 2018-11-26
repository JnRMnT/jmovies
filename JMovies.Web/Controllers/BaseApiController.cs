using JMovies.Web.UI.ErrorHandler;
using System.Web.Http;

namespace JMovies.Web.Controllers
{
    [JMWebApiErrorHandler]
    public class BaseApiController : ApiController
    {
    }
}
