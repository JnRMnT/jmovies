using JMovies.DataAccess;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Movies;
using JMovies.IMDb.Entities.People;
using JMovies.IMDb.Entities.Settings;
using System.Linq;
using JMovies.IMDb.Entities.Common;

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
            using (JMoviesEntities entities = new JMoviesEntities())
            {
                person = entities.Person.FirstOrDefault(e => e.ID == id);
                if (settings.MediaImagesFetchCount != 0 && person != null)
                {
                    //fetch primary image
                    person.PrimaryImage = entities.Image.FirstOrDefault(e => e.ID == person.PrimaryImageID);
                    //fetch images
                    int imageCount = settings.MediaImagesFetchCount;
                    if (imageCount <= 0)
                    {
                        //default value
                        imageCount = 5;
                    }
                    person.Photos = entities.Image.Where(e => e.PersonID == person.ID).Take(imageCount).ToArray();
                }
                if (!settings.FetchImageContents && person != null)
                {
                    RemoveImageContentsFromPerson(person);
                }

                return person;
            }
        }

        private static void RemoveImageContentsFromPerson(Person person)
        {
            if (person != null)
            {
                if (person.PrimaryImage != null)
                {
                    person.PrimaryImage.Content = null;
                }
                if (person.Photos != null)
                {
                    foreach (Image image in person.Photos)
                    {
                        image.Content = null;
                    }
                }
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
            using (JMoviesEntities entities = new JMoviesEntities())
            {
                Production production = entities.Production.FirstOrDefault(e => e.ID == id);
                if (settings.MediaImagesFetchCount != 0 && production != null)
                {
                    //fetch poster
                    production.Poster = entities.Image.FirstOrDefault(e => e.ID == production.PosterID);
                    //fetch images
                    int imageCount = settings.MediaImagesFetchCount;
                    if (imageCount <= 0)
                    {
                        //default value
                        imageCount = 5;
                    }
                    production.MediaImages = entities.Image.Where(e => e.ProductionID == production.ID).Take(imageCount).ToArray();
                }

                CleanupImageContents(settings, production);
                return production;
            }
        }

        private static void CleanupImageContents(ProductionDataFetchSettings settings, Production production)
        {
            if (!settings.FetchImageContents && production != null)
            {
                if (production.Poster != null)
                {
                    production.Poster.Content = null;
                }
                if (production.MediaImages != null)
                {
                    foreach (Image image in production.MediaImages)
                    {
                        image.Content = null;
                    }
                }
                if (production is Movie)
                {
                    Movie movie = (Movie)production;
                    if (movie.Credits != null)
                    {
                        foreach (Credit credit in movie.Credits)
                        {
                            RemoveImageContentsFromPerson(credit.Person);
                        }
                    }
                }
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
