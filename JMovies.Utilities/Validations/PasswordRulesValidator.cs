using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Security;
using System.Linq;

namespace JMovies.Utilities.Validations
{
    public class PasswordRulesValidator : IValidator
    {
        public bool IsValid(object[] values)
        {
            if (values == null || values.Length != 1 || !(values[0] is SecureString))
            {
                SecureString secureString = values[0] as SecureString;
                bool isValid = true;

                if (secureString.Length < 6)
                {
                    isValid = false;
                }
                else if (secureString.Length > 24)
                {
                    isValid = false;
                }

                return isValid;
            }
            else
            {
                return false;
            }
        }
    }
}
