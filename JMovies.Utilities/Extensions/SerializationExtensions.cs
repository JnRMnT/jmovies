using JMovies.Utilities.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class SerializationExtensions
{
    public static T FromJsonObject<T>(this string json)
    {
        return JsonSerializer.DeserializeObject<T>(json);
    }

    public static string ToJson(this object objectToSerialize)
    {
        return JsonSerializer.SerializeObject(objectToSerialize);
    }
}