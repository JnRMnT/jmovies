using JMovies.DataAccess;
using JMovies.DataAccess.Entities.Movies;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using JMovies.IMDb.Entities.Movies;

namespace JMovies.Tests
{
    [TestClass]
    public class DBTests
    {

        [TestMethod]
        public void InsertMovieTest()
        {
            using (JMoviesEntities entities = new JMoviesEntities())
            {
                IServiceProvider serviceProvider = DIHelper.Initialize();
                DBHelper.EmptyDB(entities);

                jm_Movie movie = new jm_Movie();
                movie.AKAs = new jm_AKA[] { new jm_AKA { Description = "Test2", Name = "Test2" } };
                movie.Budget = new jm_Budget { Amount = new DataAccess.Entities.Common.jm_Amount { Currency = "$", Value = 50000 }, Description = "Test Budget" };
                jm_Country turkey = new jm_Country { Identifier = "tr", Name = "Turkey" };
                movie.Countries = new jm_Country[] { turkey };
                movie.Credits = new jm_Credit[] { new jm_Credit { Person = new DataAccess.Entities.People.jm_Person
                {
                    FullName = "Test Person"
                }, RoleType = jm_CreditRoleType.Actor } };
                movie.FilmingLocations = new string[] { "Eskiþehir", "Ýstanbul", "Barcelona", "Hamburg" };
                movie.Genres = new jm_Genre[] { new jm_Genre { Identifier = "action", Value = "Action" } };
                movie.IMDbID = 123;
                movie.Keywords = new jm_Keyword[] { new jm_Keyword { Identifier = "punching", Value = "Punching" } };
                movie.Languages = new jm_Language[] { new jm_Language { Identifier = "tr", Name = "Turkish" } };
                movie.OfficialSites = new jm_OfficialSite[] { new jm_OfficialSite { Title = "Official Site", URL = "https://official.io" } };
                movie.OriginalTitle = "Test Movie 2";
                movie.PlotSummary = "This is a summary";
                movie.ProductionCompanies = new jm_Company[] { new jm_Company { Name = "Test Company" } };
                movie.ProductionType = jm_ProductionTypeEnum.Movie;
                movie.Rating = new jm_Rating { DataSource = new DataAccess.Entities.Common.jm_DataSource(DataAccess.Entities.Common.jm_DataSourceType.IMDb), RateCount = 500, Value = 9.9 };
                movie.ReleaseDates = new jm_ReleaseDate[] { new jm_ReleaseDate { Country = turkey, Date = DateTime.Now } };
                movie.Runtime = TimeSpan.FromMinutes(120);
                movie.StoryLine = "Story line";
                movie.TagLines = new string[] { "test", "line" };
                movie.Title = "Test Movie";
                movie.Year = 2019;

                entities.Production.Add(movie);
                entities.SaveChanges();

                jm_Movie savedMovie = entities.Production.FirstOrDefault(e => e.IMDbID == 123) as jm_Movie;
                Assert.IsNotNull(savedMovie);
                Assert.AreEqual(movie.IMDbID, savedMovie.IMDbID);
                Assert.AreEqual(movie.Title, savedMovie.Title);
            }
        }


        [TestMethod]
        public void InsertScrapedMovieTest()
        {
            IServiceProvider serviceProvider = DIHelper.Initialize();

            using (JMoviesEntities entities = new JMoviesEntities())
            {
                DBHelper.EmptyDB(entities);
                IIMDbDataProvider iMDbDataProvider = serviceProvider.GetRequiredService<IIMDbDataProvider>();
                Movie movie = iMDbDataProvider.GetMovie(2139881, false);

            }
        }
    }
}
