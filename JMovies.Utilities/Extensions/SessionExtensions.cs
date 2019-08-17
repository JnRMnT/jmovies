using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

public static class SessionExtensions
{
    public static void SetObject(this ISession session, string key, object value)
    {
        session.Set(key, Encoding.Default.GetBytes(JsonConvert.SerializeObject(value)));
    }

    public static T GetObject<T>(this ISession session, string key)
    {
        byte[] value;
        if (session.TryGetValue(key, out value))
        {
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(value));
        }
        else
        {
            return default(T);
        }
    }
}