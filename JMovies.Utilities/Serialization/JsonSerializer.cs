using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Utilities.Serialization
{
    public class JsonSerializer
    {
        private static JsonSerializerSettings settings;
        public static JsonSerializerSettings Settings
        {
            get
            {
                if(settings == null)
                {
                    settings = new JsonSerializerSettings();
                    settings.TypeNameHandling = TypeNameHandling.All;
                }
                return settings;
            }
        }

        public static T DeserializeObject<T>(string json)
        {
            return DeserializeObject<T>(json, Settings);
        }
        public static T DeserializeObject<T>(string json, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
        public static string SerializeObject(object objectToSerialize)
        {
            return SerializeObject(objectToSerialize, Settings);
        }

        public static string SerializeObject(object objectToSerialize, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(objectToSerialize, Settings);
        }
    }
}
