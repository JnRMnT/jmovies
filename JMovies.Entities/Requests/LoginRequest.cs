using JMovies.Entities.Misc;
using Newtonsoft.Json;
using System.Security;

namespace JMovies.Entities.Requests
{
    public class LoginRequest: BaseRequest
    {
        [JsonConverter(typeof(SecureStringConverter))]
        public SecureString Username { get; set; }
        [JsonConverter(typeof(SecureStringConverter))]
        public SecureString Password { get; set; }
    }
}
