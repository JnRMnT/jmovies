using JMovies.DataAccess;
using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMovies.App.Business.Validations
{
    public class EmailExistsValidator : IValidator
    {
        public bool IsValid(object[] values)
        {
            if (values != null && values.Length == 1)
            {
                string email = values[0] as string;

                using (JMoviesEntities entities = new JMoviesEntities())
                {
                    return !entities.User.Any(e => e.Email == email);
                }
            }
            else
            {
                return false;
            }
        }
    }
}
