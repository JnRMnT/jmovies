using JMovies.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.Responses
{
    public class LoginResponse : BaseResponse
    {
        public LoginResultEnum Result { get; set; }
        public User User { get; set; }
        public object Token { get; set; }
    }
}
