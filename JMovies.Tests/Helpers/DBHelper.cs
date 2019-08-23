using JMovies.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JMovies.Tests.Helpers
{
    public class DBHelper
    {
        public static void EmptyDB(JMoviesEntities entities)
        {
            entities.AKA.RemoveRange(entities.AKA);
            entities.Character.RemoveRange(entities.Character);
            entities.Company.RemoveRange(entities.Company);
            entities.Genre.RemoveRange(entities.Genre);
            entities.Keyword.RemoveRange(entities.Keyword);
            entities.Language.RemoveRange(entities.Language);
            entities.Rating.RemoveRange(entities.Rating);
            entities.ReleaseDate.RemoveRange(entities.ReleaseDate);
            entities.ProductionCredit.RemoveRange(entities.ProductionCredit);
            entities.Production.RemoveRange(entities.Production);
            entities.DataSource.RemoveRange(entities.DataSource);
            entities.Country.RemoveRange(entities.Country);
            entities.Credit.RemoveRange(entities.Credit);
            entities.Person.RemoveRange(entities.Person);
            entities.SaveChanges();
        }
    }
}
