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
            entities.SaveChanges();
            entities.Company.RemoveRange(entities.Company);
            entities.SaveChanges();
            entities.Genre.RemoveRange(entities.Genre);
            entities.SaveChanges();
            entities.Keyword.RemoveRange(entities.Keyword);
            entities.SaveChanges();
            entities.Rating.RemoveRange(entities.Rating);
            entities.SaveChanges();
            entities.ReleaseDate.RemoveRange(entities.ReleaseDate);
            entities.SaveChanges();
            entities.ProductionCredit.RemoveRange(entities.ProductionCredit);
            entities.SaveChanges();
            entities.ProductionCountry.RemoveRange(entities.ProductionCountry);
            entities.SaveChanges();
            entities.ProductionLanguage.RemoveRange(entities.ProductionLanguage);
            entities.SaveChanges();
            entities.Country.RemoveRange(entities.Country);
            entities.SaveChanges();
            entities.Language.RemoveRange(entities.Language);
            entities.SaveChanges();
            entities.Character.RemoveRange(entities.Character);
            entities.SaveChanges();
            entities.Credit.RemoveRange(entities.Credit);
            entities.SaveChanges();
            entities.TagLine.RemoveRange(entities.TagLine);
            entities.SaveChanges();
            entities.Production.RemoveRange(entities.Production);
            entities.SaveChanges();
            entities.Image.RemoveRange(entities.Image);
            entities.SaveChanges();
            entities.Person.RemoveRange(entities.Person);
            entities.SaveChanges();
            entities.DataSource.RemoveRange(entities.DataSource);
            entities.SaveChanges();
        }
    }
}
