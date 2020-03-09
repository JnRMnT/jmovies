using System;
using System.Collections.Generic;
using System.Text;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;

namespace JMovies.Entities.Interfaces
{
    public interface IAuthenticationProvider
    {
        LoginResponse Authenticate(LoginRequest request);
    }
}
