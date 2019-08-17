using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;

namespace JMovies.Entities.UserManagement
{
    public class User
    {
        [JsonProperty("id")]
        public string ID;
    }
}
