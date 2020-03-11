using JM.Entities.Framework;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace JMovies.Utilities.Helpers
{
    public class ValidationHelper
    {
        public static void ExecuteValidations(FlowConfiguration flowConfiguration, object request)
        {
            if (flowConfiguration != null && flowConfiguration.Validations != null && flowConfiguration.Validations.Length != 0)
            {
                foreach (Validation validation in flowConfiguration.Validations)
                {
                    if (!GetValidation(validation)?.IsValid(GetValues(validation, request)) == true)
                    {
                        throw new JMException(validation.ErrorResource);
                    }
                }
            }
        }

        private static IValidator GetValidation(Validation validation)
        {
            Type validationType = Type.GetType(validation.ServerValidator);
            if (validationType != null)
            {
                return Activator.CreateInstance(validationType) as IValidator;
            }
            else
            {
                return null;
            }
        }

        private static object[] GetValues(Validation validation, object request)
        {
            List<object> values = new List<object>();
            foreach (string propertyName in validation.RequestProperties)
            {
                if (propertyName.StartsWith("$$"))
                {
                    //constant
                    values.Add(propertyName.Substring(2));
                }
                else
                {
                    values.Add(request.GetType().GetProperty(propertyName).GetValue(request));
                }
            }
            return values.ToArray();
        }
    }
}
