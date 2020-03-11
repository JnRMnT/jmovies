using JMovies.DataAccess;
using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace JMovies.App.Business.Validations
{
    public class UsernameRulesValidator : IValidator
    {
        public bool IsValid(object[] values)
        {
            if (values != null && values.Length == 1)
            {
                SecureString username = values[0] as SecureString;
                bool isValid = true;
                if (username.Length < 6 || username.Length > 55)
                {
                    isValid = false;
                }
                if (isValid)
                {
                    using (JMoviesEntities entities = new JMoviesEntities())
                    {
                        isValid = !entities.User.Any(e => e.UserName == username.ToPlainString());
                    }
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
