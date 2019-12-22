﻿using JMovies.Jobs.Common;
using Microsoft.Extensions.Configuration;
using JMovies.Jobs.Common.Configuration;
using JMovies.Utilities.Common;
using Microsoft.Extensions.DependencyInjection;
using JMovies.App.ElasticSearch;
using JMovies.App.ElasticSearch.Interfaces;
using Nest;
using JMovies.Entities.Framework;
using JMovies.Common.Constants;
using JMovies.DataAccess;
using JMovies.Jobs.ElasticSearchSync.Documents;
using System.Linq;

namespace JMovies.Jobs.ElasticSearchSync
{
    class Program
    {
        private static readonly string ProductionsIndexName = "productions";
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"appsettings.{EnvironmentUtilities.GetEnvironmentName()}.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .AddCommandLine(args);

            var config = builder.Build();
            BaseJobConfiguration configuration = config.Get<BaseJobConfiguration>();
            CustomConfiguration customConfiguration = config.Get<CustomConfiguration>();
            InitializationHelper.Initialize(configuration);
            var services = new ServiceCollection();

            services.AddOptions();
            services.Configure<CustomConfiguration>(config.GetSection(ConfigurationConstants.CustomConfigurationSectionName));
            services.AddSingleton<IElasticSearchConnectionProvider, ElasticSearchConnector>();
            var serviceProvider = services.BuildServiceProvider();

            IElasticSearchConnectionProvider elasticSearchProvider = serviceProvider.GetRequiredService<IElasticSearchConnectionProvider>();
            ElasticClient elasticClient = elasticSearchProvider.GetElasticClient(ProductionsIndexName);

            using (JMoviesEntities entities = new JMoviesEntities())
            {
                foreach (var production in entities.Production)
                {
                    long productionID = production.ID;
                    if (production is JMovies.IMDb.Entities.Movies.Movie)
                    {
                        using (JMoviesEntities innerEntities = new JMoviesEntities())
                        {
                            JMovies.IMDb.Entities.Movies.Movie movie = production as JMovies.IMDb.Entities.Movies.Movie;
                            movie.AKAs = innerEntities.AKA.Where(e => e.ProductionID == productionID).ToArray();
                            movie.Genres = innerEntities.Genre.Where(e => e.ProductionID == productionID).ToArray();
                            movie.Keywords = innerEntities.Keyword.Where(e => e.ProductionID == productionID).ToArray();
                            movie.TagLines = innerEntities.TagLine.Where(e => e.ProductionID == productionID).ToArray();
                        }
                    }
                    elasticClient.IndexDocument(MapProduction(production as IMDb.Entities.Movies.Movie));
                }
            }
        }

        private static Production MapProduction(IMDb.Entities.Movies.Movie production)
        {
            return new Production
            {
                AKAs = production.AKAs?.Select(e => e.Description).ToArray(),
                Genres = production.Genres?.Select(e => e.Value).ToArray(),
                ID = production.ID,
                IMDbID = production.IMDbID,
                Keywords = production.Keywords?.Select(e => e.Value).ToArray(),
                OriginalTitle = production.OriginalTitle,
                ProductionType = production.ProductionType,
                TagLines = production.TagLines?.Select(e => e.Content).ToArray(),
                Title = production.Title,
                Year = production.Year
            };
        }
    }
}
