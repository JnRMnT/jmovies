using System;
using JMovies.Entities.IMDB;
using JMovies.Entities.Interfaces;
using JMovies.Tests.Helpers;
using JMovies.Utilities.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JMovies.Tests
{
    [TestClass]
    public class IMDbScrapingTests
    {
        [TestMethod]
        public void MovieScraping()
        {
            long movieID = 1477834;//397442;//6412452;
            UnityHelper.Initialize();
            IIMDbDataProvider imdbDataProvider = SingletonUnity.Resolve<IIMDbDataProvider>();
            Movie movie = imdbDataProvider.GetMovie(movieID, false);
            Assert.IsNotNull(movie);
            Assert.AreEqual(movieID, movie.IMDbID);
        }

        [TestMethod]
        public void DetailedMovieScraping()
        {
            long movieID = 1477834;//1477834;//397442;//6412452;
            UnityHelper.Initialize();
            IIMDbDataProvider imdbDataProvider = SingletonUnity.Resolve<IIMDbDataProvider>();
            Movie movie = imdbDataProvider.GetMovie(movieID, true);
            Assert.IsNotNull(movie);
            Assert.AreEqual(movieID, movie.IMDbID);
        }

        [TestMethod]
        public void PersonScraping()
        {
            long personID = 1297015;
            UnityHelper.Initialize();
            IIMDbDataProvider imdbDataProvider = SingletonUnity.Resolve<IIMDbDataProvider>();
            Person person = imdbDataProvider.GetPerson(personID);
            Assert.IsNotNull(person);
            Assert.AreEqual(personID, person.IMDbID);
        }
    }
}