using JMovies.Common.Constants;
using JMovies.CronJobs.Common;
using JMovies.DataAccess;
using JMovies.DataAccess.Entities.Enums;
using System.Linq;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.Misc;
using JMovies.IMDb.Entities.Movies;
using JMovies.IMDb.Providers;
using System;
using JMovies.App.Business.Managers;

namespace JMovies.CronJobs.IMDB.ProductionPersister
{
    class Program
    {
        private static readonly EntityTypeEnum EntityType = EntityTypeEnum.Production;
        private static readonly DataSourceTypeEnum DataSource = DataSourceTypeEnum.IMDb;

        static void Main(string[] args)
        {
            using (JMoviesEntities entities = new JMoviesEntities())
            {
                IIMDbDataProvider imdbDataProvider = new IMDbScraperDataProvider();
                long startID = PersisterHelper.DetermineTheStartID(EntityType, DataSource, entities);
                for (int i = 0; i < ConfigurationConstants.PersisterRecordCountPerRun; i++)
                {
                    long dataID = startID + i;
                    if (dataID > ConfigurationConstants.IMDBMaxID)
                    {
                        dataID = 1;
                    }

                    try
                    {
                        Production production = imdbDataProvider.GetProduction(dataID, true);
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
                }
                entities.SaveChanges();
            }
        }
    }
}
