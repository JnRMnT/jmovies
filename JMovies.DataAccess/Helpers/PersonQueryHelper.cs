using JMovies.IMDb.Entities.Common;
using JMovies.IMDb.Entities.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.DataAccess.Helpers
{
    public class PersonQueryHelper
    {
        public static IIncludableQueryable<Person, Image> GetResolvedPersonQuery(JMoviesEntities entities)
        {
            return entities.Person.Include(e => e.PrimaryImage);
        }
    }
}
