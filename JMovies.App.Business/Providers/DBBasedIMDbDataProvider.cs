using JMovies.DataAccess;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Movies;
using JMovies.IMDb.Entities.People;
using JMovies.IMDb.Entities.Settings;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using JMovies.DataAccess.Helpers;

namespace JMovies.App.Business.Providers
{
    public class DBBasedIMDbDataProvider : IIMDbDataProvider
    {
        /// <summary>
        /// Gets Person information
        /// </summary>
        /// <param name="id">ID of the Person</param>
        /// <param name="person">Person instance to be populated</param>
        /// <param name="settings">Object containing Data Fetch settings</param>
        /// <returns>Person instance containing retreived information</returns>
        public Person GetPerson(long id, Person person, PersonDataFetchSettings settings)
        {
            DbContextOptionsBuilder<JMoviesEntities> dbContextOptionsBuilder = new DbContextOptionsBuilder<JMoviesEntities>();
            dbContextOptionsBuilder.UseLazyLoadingProxies(false);
            using (JMoviesEntities entities = new JMoviesEntities(dbContextOptionsBuilder.Options))
            {
                return entities.Person.FirstOrDefault(e => e.ID == id);
            }
        }

        /// <summary>
        /// Gets Production Information iresspective of type
        /// </summary>
        /// <param name="id">ID of the production</param>
        /// <param name="settings">Object containing Data Fetch settings</param>
        /// <returns>Production instance containing retreived information</returns>
        public Production GetProduction(long id, ProductionDataFetchSettings settings)
        {
            DbContextOptionsBuilder<JMoviesEntities> dbContextOptionsBuilder = new DbContextOptionsBuilder<JMoviesEntities>();
            dbContextOptionsBuilder.UseLazyLoadingProxies(false);
            using (JMoviesEntities entities = new JMoviesEntities(dbContextOptionsBuilder.Options))
            {
                return ProductionQueryHelper.GetResolvedProductionQuery(entities).FirstOrDefault(e => e.ID == id);
            }
        }

        /// <summary>
        /// Gets Person information
        /// </summary>
        /// <param name="id">ID of the Person</param>
        /// <param name="settings">Object containing Data Fetch settings</param>
        /// <returns>Person instance containing retreived information</returns>
        public Person GetPerson(long id, PersonDataFetchSettings settings)
        {
            Person person = new Person();
            return GetPerson(id, person, settings);
        }

        /// <summary>
        /// Gets Movie information
        /// </summary>
        /// <param name="id">ID of the movie</param>
        /// <param name="settings">Object containing Data Fetch settings</param>
        /// <returns>Movie instance containing retreived information</returns>
        public Movie GetMovie(long id, ProductionDataFetchSettings settings)
        {
            return GetProduction(id, settings) as Movie;
        }

        /// <summary>
        /// Gets TV Series information
        /// </summary>
        /// <param name="id">ID of the TV Series</param>
        /// <param name="settings">Object containing Data Fetch settings</param>
        /// <returns>TV Series instance containing retreived information</returns>
        public TVSeries GetTvSeries(long id, ProductionDataFetchSettings settings)
        {
            return GetProduction(id, settings) as TVSeries;
        }

        /// <summary>
        /// Gets Production Information iresspective of type using the default settings
        /// </summary>
        /// <param name="id">ID of the production</param>
        /// <returns>Production instance containing retreived information</returns>
        public Production GetProduction(long id)
        {
            return GetProduction(id, new ProductionDataFetchSettings());
        }

        /// <summary>
        /// Gets Movie information using the default settings
        /// </summary>
        /// <param name="id">ID of the movie</param>
        /// <returns>Movie instance containing retreived information</returns>
        public Movie GetMovie(long id)
        {
            return GetMovie(id, new ProductionDataFetchSettings());
        }

        /// <summary>
        /// Gets TV Series information using the default settings
        /// </summary>
        /// <param name="id">ID of the TV Series</param>
        /// <returns>TV Series instance containing retreived information</returns>
        public TVSeries GetTvSeries(long id)
        {
            return GetTvSeries(id, new ProductionDataFetchSettings());
        }

        /// <summary>
        /// Gets Person information using the default settings
        /// </summary>
        /// <param name="id">ID of the Person</param>
        /// <returns>Person instance containing retreived information</returns>
        public Person GetPerson(long id)
        {
            return GetPerson(id, new PersonDataFetchSettings());
        }
    }
}
