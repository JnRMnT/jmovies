using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JMovies.Utilities.Validations
{
    public class RegexValidator : IValidator
    {
        public bool IsValid(object[] values)
        {
            if (values != null && values.Length > 1)
            {
                string value = values[0] as string;
                string pattern = values[1] as string;
                string validOnMatch = "true";
                if (values.Length > 2)
                {
                    validOnMatch = values[2] as string;
                }
                Regex regex = new Regex(pattern);
                return regex.IsMatch(value) && validOnMatch.ToBool();
            }
            else
            {
                return false;
            }
        }
    }
}
