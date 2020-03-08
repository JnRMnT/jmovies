using JMovies.Entities;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.App.Business.Actions
{
    public class Login : IActionClass
    {
        public void ExecuteAction(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            LoginRequest requestMessage = request as LoginRequest;
            LoginResponse responseMessage = response as LoginResponse;


        }
    }
}
