using JMovies.Utilities.Serialization;

public static class SerializationExtensions
{

    public static T FromJsonObject<T>(this string json)
    {
        return JsonSerializer.DeserializeObject<T>(json);
    }
    public static T FromJsonObject<T>(this string json, Newtonsoft.Json.JsonSerializerSettings settings)
    {
        return JsonSerializer.DeserializeObject<T>(json, settings);
    }

    public static string ToJson(this object objectToSerialize)
    {
        return JsonSerializer.SerializeObject(objectToSerialize);
    }
    public static string ToJson(this object objectToSerialize, Newtonsoft.Json.JsonSerializerSettings settings)
    {
        return JsonSerializer.SerializeObject(objectToSerialize, settings);
    }
}