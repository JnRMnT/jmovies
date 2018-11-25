using System.Threading.Tasks;
using System.Security.Claims;
using Cosmonaut;
using Newtonsoft.Json;

namespace JMovies.Entities.UserManagement
{
    public class User: CosmosEntity
    {
        [JsonProperty("id")]
        public string ID;
    }
}
