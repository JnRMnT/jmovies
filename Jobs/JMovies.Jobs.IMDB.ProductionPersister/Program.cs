using JMovies.Common.Constants;
using JMovies.Jobs.Common;
using JMovies.DataAccess;
using JMovies.DataAccess.Entities.Enums;
using System.Linq;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Misc;
using JMovies.IMDb.Entities.Movies;
using JMovies.IMDb.Providers;
using System;
using JMovies.App.Business.Managers;
using JMovies.IMDb.Entities.Settings;
using JMovies.IMDb.Entities.Settings.Presets;
using Microsoft.Extensions.Configuration;
using JMovies.Jobs.Common.Configuration;
using JMovies.Utilities.Common;

namespace JMovies.Jobs.IMDB.ProductionPersister
{
    class Program
    {
        private static readonly EntityTypeEnum EntityType = EntityTypeEnum.Production;
        private static readonly DataSourceTypeEnum DataSource = DataSourceTypeEnum.IMDb;
        private static readonly ProductionDataFetchSettings ProductionDataFetchSettings = new FullProductionDataFetchSettings();

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"appsettings.{EnvironmentUtilities.GetEnvironmentName()}.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .AddCommandLine(args);

            BaseJobConfiguration configuration = builder.Build().Get<BaseJobConfiguration>();
            if (configuration.MaxRecordCount == default(int))
            {
                configuration.MaxRecordCount = ConfigurationConstants.PersisterRecordCountPerRun;
            }

            using (JMoviesEntities entities = new JMoviesEntities())
            {
                IIMDbDataProvider imdbDataProvider = new IMDbScraperDataProvider();
                if (configuration.StartRecordID == default(long) || configuration.WorkingType == PersisterWorkingTypeEnum.UpdateInternalData)
                {
                    configuration.StartRecordID = PersisterHelper.DetermineTheStartID(EntityType, DataSource, configuration.WorkingType, configuration.StartRecordID, entities);
                }
                long dataID = configuration.StartRecordID;
                for (int i = 0; i < configuration.MaxRecordCount; i++)
                {
                    if (i != 0)
                    {
                        dataID = PersisterHelper.GetNextID(EntityType, DataSource, configuration.WorkingType, entities, dataID);
                    }

                    if (dataID != default(long))
                    {
                        if (dataID > ConfigurationConstants.IMDBMaxID)
                        {
                            dataID = 1;
                        }

                        try
                        {
                            Production production = imdbDataProvider.GetProduction(dataID, ProductionDataFetchSettings);
                            using (JMoviesEntities productionPersistanceEntities = new JMoviesEntities())
                            {
                                ProductionPersistanceManager.Persist(productionPersistanceEntities, production);
                            }
                            PersisterHelper.SavePersisterHistory(entities, dataID, DataSource, EntityType, string.Empty);
                        }
                        catch (Exception exception)
                        {
                            PersisterHelper.SavePersisterHistory(entities, dataID, DataSource, EntityType, exception.ToString());
                        }
                        entities.SaveChanges();
                    }
                }
            }
        }
    }
}
