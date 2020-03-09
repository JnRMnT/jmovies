using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace JMovies.Entities.Misc
{
    public class SecureStringConverter : JsonConverter<SecureString>
    {
        public override void WriteJson(JsonWriter writer, SecureString value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToPlainString());
        }

        public override SecureString ReadJson(JsonReader reader, Type objectType, SecureString existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string stringValue = (string)reader.Value;
            return stringValue.ToSecureString();
        }
    }
}
