using JMovies.Common.Constants;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Movies;
using JMovies.IMDb.Entities.People;
using JMovies.IMDb.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Web.Providers
{
    public class ActionBasedIMDbDataProvider : IIMDbDataProvider
    {
        private IJMAppClientProvider jmAppClientProvider;
        public ActionBasedIMDbDataProvider(IJMAppClientProvider jmAppClientProvider)
        {
            this.jmAppClientProvider = jmAppClientProvider;
        }

        private static readonly PersonDataFetchSettings defaultPersonDataFetchSettings = new PersonDataFetchSettings { FetchBioPage = true, FetchImageContents = false, MediaImagesFetchCount = 5 };
        private static readonly ProductionDataFetchSettings defaultProductionDataFetchSettings = new ProductionDataFetchSettings { FetchDetailedCast = true, FetchImageContents = false, MediaImagesFetchCount = 5, CastFetchCount = 8 };

        /// <summary>
        /// Gets Person information
        /// </summary>
        /// <param name="id">ID of the Person</param>
        /// <param name="person">Person instance to be populated</param>
        /// <param name="settings">Object containing Data Fetch settings</param>
        /// <returns>Person instance containing retreived information</returns>
        public Person GetPerson(long id, Person person, PersonDataFetchSettings settings)
        {
            GetPersonDetailsRequest request = new GetPersonDetailsRequest { ID = id, Settings = settings };
            GetPersonDetailsResponse response = jmAppClientProvider.CallAction<GetPersonDetailsResponse>(ActionNameConstants.GetPersonDetails, request);
            return response.Person;
        }

        /// <summary>
        /// Gets Production Information iresspective of type
        /// </summary>
        /// <param name="id">ID of the production</param>
        /// <param name="settings">Object containing Data Fetch settings</param>
        /// <returns>Production instance containing retreived information</returns>
        public Production GetProduction(long id, ProductionDataFetchSettings settings)
        {
            GetProductionDetailsRequest request = new GetProductionDetailsRequest { ID = id, Settings = settings };
            GetProductionDetailsResponse response = jmAppClientProvider.CallAction<GetProductionDetailsResponse>(ActionNameConstants.GetProductionDetails, request);
            return response.Production;
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
            return GetProduction(id, defaultProductionDataFetchSettings);
        }

        /// <summary>
        /// Gets Movie information using the default settings
        /// </summary>
        /// <param name="id">ID of the movie</param>
        /// <returns>Movie instance containing retreived information</returns>
        public Movie GetMovie(long id)
        {
            return GetMovie(id, defaultProductionDataFetchSettings);
        }

        /// <summary>
        /// Gets TV Series information using the default settings
        /// </summary>
        /// <param name="id">ID of the TV Series</param>
        /// <returns>TV Series instance containing retreived information</returns>
        public TVSeries GetTvSeries(long id)
        {
            return GetTvSeries(id, defaultProductionDataFetchSettings);
        }

        /// <summary>
        /// Gets Person information using the default settings
        /// </summary>
        /// <param name="id">ID of the Person</param>
        /// <returns>Person instance containing retreived information</returns>
        public Person GetPerson(long id)
        {
            return GetPerson(id, defaultPersonDataFetchSettings);
        }
    }
}
