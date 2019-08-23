using JMovies.DataAccess;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using JMovies.IMDb.Entities.Movies;
using JMovies.IMDb.Entities.Common;
using JMovies.IMDb.Entities.People;
using JMovies.IMDb.Entities.Misc;

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

                Movie movie = new Movie();
                movie.AKAs = new AKA[] { new AKA { Description = "Test2", Name = "Test2" } };
                movie.Budget = new Budget { Amount = new Amount { Currency = "$", Value = 50000 }, Description = "Test Budget" };
                Country turkey = new Country { Identifier = "tr", Name = "Turkey" };
                movie.Countries = new Country[] { turkey };
                movie.Credits = new Credit[] { new Credit { Person = new Person
                {
                    FullName = "Test Person"
                }, RoleType = CreditRoleType.Actor } };
                movie.FilmingLocations = new string[] { "Eskiþehir", "Ýstanbul", "Barcelona", "Hamburg" };
                movie.Genres = new Genre[] { new Genre { Identifier = "action", Value = "Action" } };
                movie.IMDbID = 123;
                movie.Keywords = new Keyword[] { new Keyword { Identifier = "punching", Value = "Punching" } };
                movie.Languages = new Language[] { new Language { Identifier = "tr", Name = "Turkish" } };
                movie.OfficialSites = new OfficialSite[] { new OfficialSite { Title = "Official Site", URL = "https://official.io" } };
                movie.OriginalTitle = "Test Movie 2";
                movie.PlotSummary = "This is a summary";
                movie.ProductionCompanies = new Company[] { new Company { Name = "Test Company" } };
                movie.Rating = new Rating { DataSource = new DataSource(DataSourceTypeEnum.IMDb), RateCount = 500, Value = 9.9 };
                movie.ReleaseDates = new ReleaseDate[] { new ReleaseDate { Country = turkey, Date = DateTime.Now } };
                movie.Runtime = TimeSpan.FromMinutes(120);
                movie.StoryLine = "Story line";
                movie.TagLines = new string[] { "test", "line" };
                movie.Title = "Test Movie";
                movie.Year = 2019;

                entities.Production.Add(movie);
                entities.SaveChanges();

                Movie savedMovie = entities.Production.FirstOrDefault(e => e.IMDbID == 123) as Movie;
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

                entities.Production.Add(movie);
                entities.SaveChanges();

                Movie savedMovie = entities.Production.FirstOrDefault(e => e.IMDbID == movie.IMDbID) as Movie;
                Assert.IsNotNull(savedMovie);
                Assert.AreEqual(movie.IMDbID, savedMovie.IMDbID);
                Assert.AreEqual(movie.Title, savedMovie.Title);
            }
        }
    }
}
