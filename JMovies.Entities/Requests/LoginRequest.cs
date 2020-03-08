using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace JMovies.Entities.Requests
{
    public class LoginRequest: BaseRequest
    {
        public SecureString Username;
        public SecureString Password;
    }
}
