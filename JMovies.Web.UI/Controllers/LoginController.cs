using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using JMovies.Entities.Responses;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JM.Entities.Framework;
using System.IdentityModel.Tokens.Jwt;

namespace JMovies.Web.UI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/login")]
    public class LoginController : BaseApiController
    {
        private IAuthenticationProvider authenticationProvider;
        private ITokenProvider tokenProvider;
        public LoginController(IAuthenticationProvider authenticationProvider, ITokenProvider tokenProvider)
        {
            this.authenticationProvider = authenticationProvider;
            this.tokenProvider = tokenProvider;
        }

        [HttpPost]
        public LoginResponse Post([FromBody]LoginRequest request)
        {
            LoginResponse response = authenticationProvider.Authenticate(request);
            if (response.Result != Entities.UserManagement.LoginResultEnum.Successful)
            {
                throw new JMException(response.Result.ToString());
            }
            else
            {
                //issue token
                response.Token = tokenProvider.IssueToken(request.Username.ToPlainString());
            }

            return response;
        }
    }
}
