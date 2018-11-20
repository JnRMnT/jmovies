using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class PrimitiveExtensions
{
    public static long ToLong(this string stringValue)
    {
        if (string.IsNullOrEmpty(stringValue))
        {
            return default(long);
        }
        else
        {
            return long.Parse(stringValue);
        }
    }

    public static int ToInteger(this string stringValue)
    {
        if (string.IsNullOrEmpty(stringValue))
        {
            return default(int);
        }
        else
        {
            return int.Parse(stringValue);
        }
    }
}
