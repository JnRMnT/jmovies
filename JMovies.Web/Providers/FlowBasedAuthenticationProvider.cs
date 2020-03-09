using JMovies.Common.Constants;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;

namespace JMovies.Web.Providers
{
    public class FlowBasedAuthenticationProvider : IAuthenticationProvider
    {
        private IJMAppClientProvider jmAppClientProvider;
        public FlowBasedAuthenticationProvider(IJMAppClientProvider jmAppClientProvider)
        {
            this.jmAppClientProvider = jmAppClientProvider;
        }

        public LoginResponse Authenticate(LoginRequest request)
        {
            return jmAppClientProvider.CallAction<LoginResponse>(ActionNameConstants.Login, request);
        }
    }
}
