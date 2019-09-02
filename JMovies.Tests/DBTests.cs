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
using JMovies.App.Business.Managers;
using System.Collections.Generic;
using JMovies.IMDb.Entities.Settings;
using JMovies.IMDb.Entities.Settings.Presets;

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
                //DBHelper.EmptyDB(entities);

                Movie movie = new Movie();
                movie.AKAs = new List<AKA> { new AKA { Description = "Test2", Name = "Test2" } };
                movie.Budget = new Budget { Amount = new Amount { Currency = "$", Value = 50000 }, Description = "Test Budget" };
                Country turkey = new Country { Identifier = "tr", Name = "Turkey" };
                movie.Countries = new List<ProductionCountry> { new ProductionCountry { Country = turkey, Production = movie } };
                movie.Credits = new List<Credit> { new Credit { Person = new Person
                {
                    FullName = "Test Person"
                }, RoleType = CreditRoleType.Actor } };
                movie.FilmingLocations = new List<string> { "Eskiþehir", "Ýstanbul", "Barcelona", "Hamburg" };
                movie.Genres = new List<Genre> { new Genre { Identifier = "action", Value = "Action" } };
                movie.IMDbID = 123;
                movie.Keywords = new List<Keyword> { new Keyword { Identifier = "punching", Value = "Punching" } };
                movie.Languages = new List<ProductionLanguage> { new ProductionLanguage { Language = new Language { Identifier = "tr", Name = "Turkish" }, Production = movie } };
                movie.OfficialSites = new List<OfficialSite> { new OfficialSite { Title = "Official Site", URL = "https://official.io" } };
                movie.OriginalTitle = "Test Movie 2";
                movie.PlotSummary = "This is a summary";
                movie.ProductionCompanies = new Company[] { new Company { Name = "Test Company" } };
                movie.Rating = new Rating { DataSource = new DataSource(DataSourceTypeEnum.IMDb), RateCount = 500, Value = 9.9 };
                movie.ReleaseDates = new List<ReleaseDate> { new ReleaseDate { Country = turkey, Date = DateTime.Now } };
                movie.Runtime = TimeSpan.FromMinutes(120);
                movie.StoryLine = "Story line";
                movie.TagLines = new List<string> { "test", "line" };
                movie.Title = "Test Movie";
                movie.Year = 2019;

                ProductionPersistanceManager.Persist(entities, movie);
                //entities.Add(movie);
                //entities.SaveChanges();

                Movie savedMovie = entities.Production.SingleOrDefault(e => e.IMDbID == 123) as Movie;
                Assert.IsNotNull(savedMovie);
                Assert.AreEqual(movie.IMDbID, savedMovie.IMDbID);
                Assert.AreEqual(movie.Title, savedMovie.Title);
            }
        }


        [TestMethod]
        public void InsertScrapedMovieTest()
        {
            IServiceProvider serviceProvider = DIHelper.Initialize();
            ProductionDataFetchSettings productionDataFetchSettings = new FullProductionDataFetchSettings();
            using (JMoviesEntities entities = new JMoviesEntities())
            {
                //DBHelper.EmptyDB(entities);
                IIMDbDataProvider iMDbDataProvider = serviceProvider.GetRequiredService<IIMDbDataProvider>();
                Movie movie = iMDbDataProvider.GetMovie(1477834, productionDataFetchSettings);

                ProductionPersistanceManager.Persist(entities, movie);

                Movie savedMovie = entities.Production.SingleOrDefault(e => e.IMDbID == movie.IMDbID) as Movie;
                Assert.IsNotNull(savedMovie);
                Assert.AreEqual(movie.IMDbID, savedMovie.IMDbID);
                Assert.AreEqual(movie.Title, savedMovie.Title);
            }
        }



        [TestMethod]
        public void EmptyDBTest()
        {
            using (JMoviesEntities entities = new JMoviesEntities())
            {
                DBHelper.EmptyDB(entities);

                Assert.AreEqual(0, entities.AKA.Count());
                Assert.AreEqual(0, entities.Character.Count());
                Assert.AreEqual(0, entities.Company.Count());
                Assert.AreEqual(0, entities.Country.Count());
                Assert.AreEqual(0, entities.Credit.Count());
                Assert.AreEqual(0, entities.DataSource.Count());
                Assert.AreEqual(0, entities.Genre.Count());
                Assert.AreEqual(0, entities.Keyword.Count());
                Assert.AreEqual(0, entities.Language.Count());
                Assert.AreEqual(0, entities.Person.Count());
                Assert.AreEqual(0, entities.Production.Count());
                Assert.AreEqual(0, entities.ProductionCountry.Count());
                Assert.AreEqual(0, entities.ProductionCredit.Count());
                Assert.AreEqual(0, entities.ProductionLanguage.Count());
                Assert.AreEqual(0, entities.Rating.Count());
                Assert.AreEqual(0, entities.ReleaseDate.Count());
            }
        }
    }
}
