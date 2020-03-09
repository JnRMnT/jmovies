using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using JMovies.Entities.Responses;
using JMovies.Entities.Interfaces;
using JMovies.Common.Constants;
using JMovies.Entities.Requests;
using System.Security;
using JM.Entities.Framework;

namespace JMovies.Web.UI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/login")]
    public class LoginController : BaseApiController
    {
        private IAuthenticationProvider authenticationProvider;
        public LoginController(IAuthenticationProvider authenticationProvider)
        {
            this.authenticationProvider = authenticationProvider;
        }

        [HttpPost]
        public LoginResponse Post([FromBody]LoginRequest request)
        {
            LoginResponse response = authenticationProvider.Authenticate(request);
            if (response.Result != Entities.UserManagement.LoginResultEnum.Successful)
            {
                throw new JMException(response.Result.ToString());
            }

            return response;
        }
    }
}
