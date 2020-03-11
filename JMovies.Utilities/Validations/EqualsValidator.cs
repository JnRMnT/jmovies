using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace JMovies.Utilities.Validations
{
    public class EqualsValidator : IValidator
    {
        public bool IsValid(object[] values)
        {
            if (values != null && values.Length == 2)
            {
                object value1 = values[0];
                object value2 = values[1];
                if (value1 is SecureString && value2 is SecureString)
                {
                    return (value1 as SecureString).ToPlainString() == (value2 as SecureString).ToPlainString();
                }
                else
                {
                    return value1 == value2;
                }
            }
            else
            {
                return false;
            }
        }
    }
}