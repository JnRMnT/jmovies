using JMovies.DataAccess;
using JMovies.DataAccess.Helpers;
using JMovies.IMDb.Entities.Common;
using JMovies.IMDb.Entities.People;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Transactions;

namespace JMovies.App.Business.Managers
{
    public class PersonPersistanceManager
    {
        public static void Persist(JMoviesEntities entities, Person person)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.Serializable;
            options.Timeout = new TimeSpan(0, 10, 0);
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                Person savedPerson = entities.Person.FirstOrDefault(e => e.IMDbID == person.IMDbID);

                bool saved = false;
                if (savedPerson != null)
                {
                    person.ID = savedPerson.ID;
                    saved = true;
                }
                else
                {
                    person.ID = CommonDBHelper.GetNewID<Person>(entities, e => e.ID);
                }

                Person trimmedPerson = GetTrimmedPerson(person);
                if (!saved)
                {
                    EntityEntry entry = entities.Person.Add(trimmedPerson);
                }
                entities.SaveChanges();
                HandleImages(entities, person, savedPerson);
                CommonDBHelper.DetachAllEntries(entities);
                CommonDBHelper.MarkEntityAsUpdated(entities, trimmedPerson, new string[] { "PersonType", "PrimaryImage", "PrimaryImageID" }, true);
                entities.SaveChanges();
                scope.Complete();
            }
        }

        private static void HandleImages(JMoviesEntities entities, Person person, Person savedPerson)
        {
            Image oldPrimaryImage = null;
            if (savedPerson != null)
            {
                oldPrimaryImage = entities.Image.FirstOrDefault(e => e.ID == savedPerson.PrimaryImageID);
            }

            Person trackedPerson = entities.Person.FirstOrDefault(e => e.ID == person.ID);
            if (savedPerson != null && oldPrimaryImage != null)
            {
                trackedPerson.PrimaryImage = null;
                trackedPerson.PrimaryImageID = null;
                entities.Image.Remove(oldPrimaryImage);
                entities.SaveChanges();
            }

            if (person.PrimaryImage != null)
            {
                person.PrimaryImage.PersonID = person.ID;
                person.PrimaryImage.ID = CommonDBHelper.GetNewID<Image>(entities, e => e.ID);
                entities.Image.Add(person.PrimaryImage);
                trackedPerson.PrimaryImageID = person.PrimaryImage.ID;
            }
            entities.SaveChanges();
            CommonDBHelper.DetachAllEntries(entities);

            if (person.Photos != null)
            {
                foreach (Image image in person.Photos)
                {
                    Image savedImage = entities.Image.FirstOrDefault(e => e.URL == image.URL);
                    if (savedImage == null)
                    {
                        image.ID = CommonDBHelper.GetNewID<Image>(entities, e => e.ID);
                        image.PersonID = person.ID;
                        entities.Add(image);
                    }
                    entities.SaveChanges();
                    CommonDBHelper.DetachAllEntries(entities);
                }
            }
        }

        private static Person GetTrimmedPerson(Person person)
        {
            Person trimmedPerson = new Person();
            trimmedPerson.BirthDate = person.BirthDate;
            trimmedPerson.BirthName = person.BirthName;
            trimmedPerson.BirthPlace = person.BirthPlace;
            trimmedPerson.FullName = person.FullName;
            trimmedPerson.Gender = person.Gender;
            trimmedPerson.Height = person.Height;
            trimmedPerson.ID = person.ID;
            trimmedPerson.IMDbID = person.IMDbID;
            trimmedPerson.MiniBiography = person.MiniBiography;
            trimmedPerson.NickName = person.NickName;
            trimmedPerson.PersonType = person.PersonType;
            trimmedPerson.Roles = person.Roles;

            return trimmedPerson;
        }
    }
}
