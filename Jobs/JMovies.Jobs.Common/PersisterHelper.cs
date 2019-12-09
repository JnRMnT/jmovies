using JMovies.Common.Constants;
using JMovies.DataAccess;
using JMovies.DataAccess.Entities.Enums;
using JMovies.DataAccess.Entities.Persisters;
using JMovies.IMDb.Entities.Misc;
using JMovies.IMDb.Entities.Movies;
using Microsoft.EntityFrameworkCore;
using JMovies.IMDb.Entities.People;
using System;
using System.Linq;

namespace JMovies.Jobs.Common
{
    public class PersisterHelper
    {
        public static long DetermineTheStartID(EntityTypeEnum entityType, DataSourceTypeEnum dataSourceType, PersisterWorkingTypeEnum workingType, long startID, JMoviesEntities entities)
        {
            int entityTypeID = (int)entityType;
            int dataSourceIdentifier = (int)dataSourceType;

            long lastID = default(long);
            if (workingType == PersisterWorkingTypeEnum.FetchByExternalID)
            {
                PersisterHistory persisterHistory = entities.PersisterHistory.OrderByDescending(e => e.DataID).FirstOrDefault(e => e.DataSource.Identifier == dataSourceIdentifier && e.EntityTypeID == entityTypeID);
                if (persisterHistory != null)
                {
                    lastID = persisterHistory.DataID;
                }
            }
            else if (workingType == PersisterWorkingTypeEnum.UpdateInternalData)
            {
                if (startID == default(long) && entityType == EntityTypeEnum.Person)
                {
                    //Find the first data that seems empty for persons 
                    Person person = entities.Person.OrderBy(e => e.IMDbID).FirstOrDefault(e => e.BirthDate == null && string.IsNullOrEmpty(e.BirthName) && string.IsNullOrEmpty(e.BirthPlace) && e.PrimaryImageID == null && string.IsNullOrEmpty(e.MiniBiography) && string.IsNullOrEmpty(e.NickName));
                    if (person != null)
                    {
                        return person.IMDbID;
                    }
                }
                return GetNextID(entityType, dataSourceType, workingType, entities, startID - 1);
            }

            if (lastID == default(long))
            {
                return 1;
            }
            else
            {
                return GetNextID(entityType, dataSourceType, workingType, entities, lastID);
            }
        }

        public static long GetNextID(EntityTypeEnum entityType, DataSourceTypeEnum dataSourceType, PersisterWorkingTypeEnum workingType, JMoviesEntities entities, long lastID)
        {
            int entityTypeID = (int)entityType;
            int dataSourceIdentifier = (int)dataSourceType;

            long maxID = lastID;
            if (workingType == PersisterWorkingTypeEnum.FetchByExternalID)
            {
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
            else if (workingType == PersisterWorkingTypeEnum.UpdateInternalData)
            {
                if (entityType == EntityTypeEnum.Person)
                {
                    Person person = entities.Person.OrderBy(e => e.IMDbID).FirstOrDefault(e => e.IMDbID > lastID);
                    if (person != null)
                    {
                        return person.IMDbID;
                    }
                    else
                    {
                        person = entities.Person.OrderBy(e => e.IMDbID).FirstOrDefault();
                        if (person != null)
                        {
                            return person.IMDbID;
                        }
                        else
                        {
                            return default(long);
                        }
                    }
                }
                else
                {
                    Production production = entities.Production.OrderBy(e => e.IMDbID).FirstOrDefault(e => e.IMDbID > lastID);
                    if (production != null)
                    {
                        return production.IMDbID;
                    }
                    else
                    {
                        production = entities.Production.OrderBy(e => e.IMDbID).FirstOrDefault();
                        if (production != null)
                        {
                            return production.IMDbID;
                        }
                        else
                        {
                            return default(long);
                        }
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
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
