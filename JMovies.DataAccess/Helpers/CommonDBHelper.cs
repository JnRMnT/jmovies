using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JMovies.DataAccess.Helpers
{
    public class CommonDBHelper
    {
        public static long GetNewID<T>(JMoviesEntities entities, Func<T, long> keySelector) where T : class
        {
            long lastIndex = entities.Set<T>().OrderByDescending(keySelector).Select(keySelector).FirstOrDefault();
            if (lastIndex == default(long))
            {
                lastIndex = 0;
            }
            return lastIndex + 1;
        }

        public static EntityEntry MarkEntityAsUpdated(JMoviesEntities entities, object objectToMark, string[] excludedProperties = null, bool ignoreNulls = false)
        {
            PropertyInfo[] properties = objectToMark.GetType().GetProperties();
            PropertyInfo idProperty = properties.FirstOrDefault(e => e.GetCustomAttribute<KeyAttribute>() != null);
            EntityEntry entry = AttachEntity(entities, objectToMark, idProperty);

            foreach (PropertyInfo property in properties)
            {
                if (property.GetCustomAttribute<NotMappedAttribute>() == null && property.GetCustomAttribute<ForeignKeyAttribute>() == null
                    && (excludedProperties == null || !excludedProperties.Contains(property.Name)))
                {
                    bool isModified = ignoreNulls || property.GetValue(objectToMark) != null;
                    if (property.Name != idProperty.Name)
                    {
                        GetPropertyAccessor(entry, property).IsModified = isModified;
                    }
                    else
                    {
                        GetPropertyAccessor(entry, property).IsModified = false;
                    }
                }
            }

            return entry;
        }
        public static void DetachAllEntries(JMoviesEntities entities)
        {
            var changedEntriesCopy = entities.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
               e.State == EntityState.Modified ||
               e.State == EntityState.Unchanged ||
               e.State == EntityState.Deleted).ToList();

            foreach (var entry in changedEntriesCopy)
            {
                entry.State = EntityState.Detached;
            }
        }

        private static EntityEntry AttachEntity(JMoviesEntities entities, object objectToMark, PropertyInfo idProperty)
        {
            EntityEntry entry = entities.ChangeTracker.Entries().FirstOrDefault(e => e.Metadata.Name == objectToMark.GetType().FullName && idProperty.GetValue(e.Entity).Equals(idProperty.GetValue(objectToMark)));
            if (entry != null)
            {
                entry.State = EntityState.Detached;
            }
            entry = entities.Attach(objectToMark);
            return entry;
        }

        private static MemberEntry GetPropertyAccessor(EntityEntry entry, PropertyInfo property)
        {
            if (entry.Navigations.Any(e => e.Metadata.Name == property.Name))
            {
                return entry.Reference(property.Name);
            }
            else
            {
                return entry.Property(property.Name);
            }
        }
    }
}
