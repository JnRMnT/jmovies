using JMovies.Common.Constants;
using JMovies.DataAccess;
using JMovies.DataAccess.Entities.Enums;
using JMovies.DataAccess.Entities.Persisters;
using JMovies.IMDb.Entities.Misc;
using System;
using System.Linq;

namespace JMovies.CronJobs.Common
{
    public class PersisterHelper
    {
        public static long DetermineTheStartID(EntityTypeEnum entityType, DataSourceTypeEnum dataSourceType, JMoviesEntities entities)
        {
            int entityTypeID = (int)entityType;
            int dataSourceIdentifier = (int)dataSourceType;
            PersisterHistory persisterHistory = entities.PersisterHistory.OrderByDescending(e => e.DataID).FirstOrDefault(e => e.DataSource.Identifier == dataSourceIdentifier && e.EntityTypeID == entityTypeID);
            if (persisterHistory == null)
            {
                return 1;
            }
            else
            {
                long maxID = persisterHistory.DataID;
                if (maxID + 1 > ConfigurationConstants.IMDBMaxID)
                {
                    PersisterHistory lastPersistance = entities.PersisterHistory.OrderByDescending(e => e.ID).FirstOrDefault(e => e.DataSource.Identifier == dataSourceIdentifier && e.EntityTypeID == entityTypeID);
                    if (lastPersistance.DataID + 1 > ConfigurationConstants.IMDBMaxID)
                    {
                        return 1;
                    }
                    else
                    {
                        return lastPersistance.DataID + 1;
                    }
                }
                else
                {
                    return maxID + 1;
                }
            }
        }

        public static void SavePersisterHistory(JMoviesEntities entities, long dataID, DataSourceTypeEnum dataSourceTypeEnum, EntityTypeEnum entityType, string errorMessage)
        {
            PersisterHistory persisterHistory = new PersisterHistory();
            persisterHistory.DataID = dataID;
            persisterHistory.DataSource = new DataSource(dataSourceTypeEnum);

            DataSource existingDataSource = entities.DataSource.FirstOrDefault(e => e.Identifier == persisterHistory.DataSource.Identifier);
            if (existingDataSource != null)
            {
                persisterHistory.DataSourceID = existingDataSource.ID;
                persisterHistory.DataSource = null;
            }

            persisterHistory.EntityType = entityType;
            persisterHistory.ErrorMessage = errorMessage;
            persisterHistory.IsSuccess = string.IsNullOrEmpty(errorMessage);
            persisterHistory.ExecuteDate = DateTime.Now;
            entities.PersisterHistory.Add(persisterHistory);
        }
    }
}
