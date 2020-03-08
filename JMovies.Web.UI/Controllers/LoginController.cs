using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using JMovies.Entities.Responses;
using JMovies.Entities.Interfaces;
using JMovies.Common.Constants;
using JMovies.Entities.Requests;

namespace JMovies.Web.UI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/login")]
    public class LoginController : BaseApiController
    {
        private IJMAppClientProvider jmAppClientProvider;
        public LoginController(IJMAppClientProvider jmAppClientProvider)
        {
            this.jmAppClientProvider = jmAppClientProvider;
        }

        [HttpPost]
        public LoginResponse Post([FromBody]LoginRequest request)
        {
            LoginResponse loginResponse = jmAppClientProvider.CallAction<LoginResponse>(ActionNameConstants.Login, request);

            return loginResponse;
        }
    }
}
