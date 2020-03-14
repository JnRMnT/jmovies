using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using JMovies.Entities.Responses;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JM.Entities.Framework;
using System.IdentityModel.Tokens.Jwt;
using JMovies.Entities;

namespace JMovies.Web.UI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/login")]
    public class LoginController : BaseApiController
    {
        private IAuthenticationProvider authenticationProvider;
        private ITokenProvider tokenProvider;
        private IContextProvider contextProvider;
        public LoginController(IAuthenticationProvider authenticationProvider, ITokenProvider tokenProvider, IContextProvider contextProvider)
        {
            this.authenticationProvider = authenticationProvider;
            this.tokenProvider = tokenProvider;
            this.contextProvider = contextProvider;
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
                Context context = contextProvider.GetContext();
                context.User = response.User;
                contextProvider.SetContext(context);
            }

            return response;
        }
    }
}
