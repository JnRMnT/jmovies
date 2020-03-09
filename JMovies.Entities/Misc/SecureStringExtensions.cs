using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

public static class SecureStringExtensions
{
    public static string ToPlainString(this SecureString secureString)
    {
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            return Marshal.PtrToStringUni(valuePtr);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }
    }
    public static SecureString ToSecureString(this string text)
    {
        SecureString secureString = new SecureString();
        foreach (char character in text)
        {
            secureString.AppendChar(character);
        }
        secureString.MakeReadOnly();
        return secureString;
    }
}
