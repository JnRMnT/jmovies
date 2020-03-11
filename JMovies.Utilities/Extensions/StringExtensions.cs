using System;
using System.Collections.Generic;
using System.Text;

public static class StringExtensions
{
    public static bool ToBool(this string value)
    {
        return value.ToStringOrEmpty().ToLowerInvariant() == "true";
    }

    public static string ToStringOrEmpty(this string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value;
        }
        else
        {
            return string.Empty;
        }
    }
}
