using JMovies.Utilities.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JMovies.DataAccess.Converters
{
    public class JsonConverter<T> : ValueConverter<T, string>
    {
        private static Newtonsoft.Json.JsonSerializerSettings Settings
        {
            get
            {
                Newtonsoft.Json.JsonSerializerSettings settings = JsonSerializer.Settings;
                settings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None;
                return settings;
            }
        }
        public JsonConverter(ConverterMappingHints mappingHints = default)
   : base(ConvertTo, ConvertFrom, mappingHints)
        {
        }

        static Expression<Func<T, string>> ConvertTo = x => x.ToJson(Settings);
        static Expression<Func<string, T>> ConvertFrom = x => x.FromJsonObject<T>(Settings);
    }
}
