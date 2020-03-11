using JMovies.Entities.Misc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace JMovies.Entities.Requests
{
    public class RegisterRequest : BaseRequest
    {
        [JsonConverter(typeof(SecureStringConverter))]
        public SecureString Username { get; set; }
        [JsonConverter(typeof(SecureStringConverter))]
        public SecureString Password { get; set; }
        [JsonConverter(typeof(SecureStringConverter))]
        public SecureString PasswordConfirm { get; set; }

        public string Email { get; set; }
    }
}
