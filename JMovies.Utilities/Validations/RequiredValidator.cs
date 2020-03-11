using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Utilities.Validations
{
    public class RequiredValidator : IValidator
    {
        public bool IsValid(object[] values)
        {
            return values != null && values.Length == 1 && values[0] != null;
        }
    }
}
