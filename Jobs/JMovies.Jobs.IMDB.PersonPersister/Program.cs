using JMovies.App.Business.Managers;
using JMovies.Common.Constants;
using JMovies.DataAccess;
using JMovies.DataAccess.Entities.Enums;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Misc;
using JMovies.IMDb.Entities.People;
using JMovies.IMDb.Entities.Settings;
using JMovies.IMDb.Entities.Settings.Presets;
using JMovies.IMDb.Providers;
using JMovies.Jobs.Common;
using JMovies.Jobs.Common.Configuration;
using JMovies.Utilities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JMovies.Jobs.IMDB.PersonPersister
{
    class Program
    {
        private static readonly EntityTypeEnum EntityType = EntityTypeEnum.Person;
        private static readonly DataSourceTypeEnum DataSource = DataSourceTypeEnum.IMDb;
        private static readonly PersonDataFetchSettings PersonDataFetchSettings = new FullPersonDataFetchSettings();

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"appsettings.{EnvironmentUtilities.GetEnvironmentName()}.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .AddCommandLine(args);

            BaseJobConfiguration configuration = builder.Build().Get<BaseJobConfiguration>();
            InitializationHelper.Initialize(configuration);
            var services = new ServiceCollection();
            services.AddOptions();
            var serviceProvider = services.BuildServiceProvider();


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
                            Person person = imdbDataProvider.GetPerson(dataID, PersonDataFetchSettings);
                            DbContextOptionsBuilder<JMoviesEntities> dbContextOptionsBuilder = new DbContextOptionsBuilder<JMoviesEntities>();
                            dbContextOptionsBuilder.UseLazyLoadingProxies(true);
                            using (JMoviesEntities productionPersistanceEntities = new JMoviesEntities(dbContextOptionsBuilder.Options))
                            {
                                PersonPersistanceManager.Persist(productionPersistanceEntities, person);
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
