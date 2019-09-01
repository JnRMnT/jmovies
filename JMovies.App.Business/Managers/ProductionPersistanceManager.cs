using JMovies.DataAccess;
using JMovies.IMDb.Entities.Movies;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JMovies.IMDb.Entities.People;
using System.Transactions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using JMovies.IMDb.Factories;

namespace JMovies.App.Business.Managers
{
    public class ProductionPersistanceManager
    {
        public static void Persist(JMoviesEntities entities, Production production)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                Production savedProduction = entities.Production
                    .Include(e => e.Rating).ThenInclude(e => e.DataSource)
                    .Include(e => ((Movie)e).ReleaseDates)
                    .Include(e => ((Movie)e).AKAs)
                    .Include(e => ((Movie)e).Countries)
                    .Include(e => ((Movie)e).Credits)
                    .Include(e => ((Movie)e).Genres)
                    .Include(e => ((Movie)e).Keywords)
                    .Include(e => ((Movie)e).Languages)
                    .Include(e => ((Movie)e).ProductionCompanies)
                    .Include(e => ((Movie)e).ReleaseDates)
                    .FirstOrDefault(e => e.IMDbID == production.IMDbID);

                bool saved = false;
                if (savedProduction != null)
                {
                    production.ID = savedProduction.ID;
                    saved = true;
                }
                else
                {
                    production.ID = GetNewID<Production>(entities, e => e.ID);
                }

                Production trimmedProduction = GetTrimmedProduction(production);
                if (!saved)
                {
                    entities.Production.Add(trimmedProduction);
                }
                HandleAKAs(entities, production, savedProduction);
                HandleCompanies(entities, production, savedProduction);
                HandleDataSources(entities, production, savedProduction);
                HandleGenres(entities, production, savedProduction);
                HandleKeywords(entities, production, savedProduction);
                HandleLanguages(entities, production, savedProduction);
                HandleCountries(entities, production, savedProduction);
                HandlePersons(entities, production, savedProduction);
                HandleCharacters(entities, production, savedProduction);
                HandleRatings(entities, production, savedProduction);
                HandleReleaseDates(entities, production, savedProduction);
                MarkEntityAsUpdated(entities, trimmedProduction, new string[] { "ProductionType" }, true);
                entities.SaveChanges();
                scope.Complete();
            }
        }

        private static Production GetTrimmedProduction(Production production)
        {
            ProductionFactory productionFactory = new ProductionFactory();
            Production trimmedProduction = productionFactory.Build(production.ProductionType);

            trimmedProduction.ID = production.ID;
            trimmedProduction.IMDbID = production.IMDbID;
            trimmedProduction.Title = production.Title;
            trimmedProduction.Year = production.Year;
            trimmedProduction.ProductionType = production.ProductionType;

            if (trimmedProduction is Movie)
            {
                Movie movie = production as Movie;
                Movie trimmedMovie = trimmedProduction as Movie;
                trimmedMovie.Budget = movie.Budget;
                trimmedMovie.FilmingLocations = movie.FilmingLocations;
                trimmedMovie.OriginalTitle = movie.OriginalTitle;
                trimmedMovie.PlotSummary = movie.PlotSummary;
                trimmedMovie.Runtime = movie.Runtime;
                trimmedMovie.StoryLine = movie.StoryLine;
                trimmedMovie.TagLines = movie.TagLines;
            }

            if (trimmedProduction is TVSeries)
            {
                TVSeries tvSeries = production as TVSeries;
                TVSeries trimmedTVSeries = trimmedProduction as TVSeries;
                trimmedTVSeries.EndYear = tvSeries.EndYear;
            }

            return trimmedProduction;
        }

        private static long GetNewID<T>(JMoviesEntities entities, Func<T, long> keySelector) where T : class
        {
            long lastIndex = entities.Set<T>().OrderByDescending(keySelector).Select(keySelector).FirstOrDefault();
            if (lastIndex == default(long))
            {
                lastIndex = 0;
            }
            return lastIndex + 1;
        }

        private static EntityEntry MarkEntityAsUpdated(JMoviesEntities entities, object objectToMark, string[] excludedProperties = null, bool ignoreNulls = false)
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

        private static void HandleReleaseDates(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Movie savedMovie = savedProduction as Movie;
            if (savedMovie != null)
            {
                entities.ReleaseDate.RemoveRange(savedMovie.ReleaseDates);
            }

            Movie movie = production as Movie;
            if (movie != null)
            {
                foreach (ReleaseDate releaseDate in movie.ReleaseDates.ToArray())
                {
                    EntityEntry entry = null;
                    if (releaseDate.Country != null)
                    {
                        Country existingCountry = entities.Country.FirstOrDefault(e => e.Identifier == releaseDate.Country.Identifier);
                        if (existingCountry != null)
                        {
                            releaseDate.Country.ID = existingCountry.ID;
                            entry = MarkEntityAsUpdated(entities, releaseDate.Country);
                        }
                        else
                        {
                            releaseDate.Country.ID = GetNewID<Country>(entities, e => e.ID);
                            entry = entities.Country.Add(releaseDate.Country);
                        }
                    }

                    releaseDate.CountryID = releaseDate.Country.ID;
                    releaseDate.Country = null;
                    releaseDate.ProductionID = production.ID;
                    releaseDate.ID = GetNewID<ReleaseDate>(entities, e => e.ID);
                    entities.ReleaseDate.Add(releaseDate);

                    entities.SaveChanges();
                    DetachAllEntries(entities);
                }
            }
        }

        private static void HandleRatings(JMoviesEntities entities, Production production, Production savedProduction)
        {
            EntityEntry entry = null;

            production.Rating.DataSourceID = entities.DataSource.FirstOrDefault(e => e.DataSourceType == production.Rating.DataSource.DataSourceType).ID;
            production.Rating.DataSource = null;

            production.Rating.ProductionID = production.Rating.Production.ID;
            production.Rating.Production = null;
            if (savedProduction != null && savedProduction.Rating != null)
            {
                production.Rating.ID = savedProduction.Rating.ID;
                entry = MarkEntityAsUpdated(entities, production.Rating);
            }
            else
            {
                production.Rating.ID = GetNewID<Rating>(entities, e => e.ID);
                entry = entities.Rating.Add(production.Rating);
            }
            entities.SaveChanges();
            DetachAllEntries(entities);
        }

        private static void HandlePersons(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Movie movie = production as Movie;

            if (movie != null)
            {
                foreach (Credit credit in movie.Credits.ToArray())
                {
                    EntityEntry personEntry = null;
                    Credit existingCredit = null;

                    Person existingPerson = entities.Person.FirstOrDefault(e => e.IMDbID == credit.Person.IMDbID);
                    if (existingPerson != null)
                    {
                        credit.Person.ID = existingPerson.ID;
                        //personEntry = MarkEntityAsUpdated(entities, credit.Person, new string[] { "PersonType" });
                    }
                    else
                    {
                        credit.Person.ID = GetNewID<Person>(entities, e => e.ID);
                        personEntry = entities.Person.Add(credit.Person);
                    }
                    entities.SaveChanges();

                    existingCredit = entities.Credit.FirstOrDefault(e => e.Person.ID == credit.Person.ID && e.ProductionID == production.ID);

                    ICollection<Character> characters = null;

                    credit.PersonID = credit.Person.ID;
                    credit.Person = null;
                    credit.ProductionID = production.ID;
                    EntityEntry entry = null;

                    if (credit is ActingCredit)
                    {
                        characters = ((ActingCredit)credit).Characters;
                        ((ActingCredit)credit).Characters = null;
                    }

                    if (existingCredit != null)
                    {
                        credit.ID = existingCredit.ID;
                        entry = MarkEntityAsUpdated(entities, credit, new string[] { "RoleType", "Characters", "Person" });
                    }
                    else
                    {
                        //credit.ID = GetNewID<Credit>(entities, e => e.ID);
                        entry = entities.Credit.Add(credit);
                    }

                    entities.SaveChanges();

                    if (characters != null)
                    {
                        ((ActingCredit)credit).Characters = characters;
                    }
                    DetachAllEntries(entities);
                }
            }
        }

        private static void HandleLanguages(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Movie movie = production as Movie;
            Movie savedMovie = savedProduction as Movie;

            if (movie != null && movie.Languages != null)
            {
                foreach (ProductionLanguage productionLanguage in movie.Languages.ToArray())
                {
                    EntityEntry entry = null;
                    EntityEntry languageEntry = null;
                    Language existingLanguage = entities.Language.FirstOrDefault(e => e.Identifier == productionLanguage.Language.Identifier);
                    if (existingLanguage != null)
                    {
                        productionLanguage.Language.ID = existingLanguage.ID;
                        languageEntry = MarkEntityAsUpdated(entities, productionLanguage.Language);
                    }
                    else
                    {
                        productionLanguage.Language.ID = GetNewID<Language>(entities, e => e.ID);
                        languageEntry = entities.Language.Add(productionLanguage.Language);
                    }

                    ProductionLanguage existingProductionLanguage = entities.ProductionLanguage.FirstOrDefault(e => e.Language.Identifier == productionLanguage.Language.Identifier && e.Production.IMDbID == productionLanguage.Production.IMDbID);

                    productionLanguage.ProductionID = productionLanguage.Production.ID;
                    productionLanguage.Production = null;
                    productionLanguage.LanguageID = productionLanguage.Language.ID;
                    productionLanguage.Language = null;
                    if (existingProductionLanguage != null)
                    {
                        productionLanguage.ID = existingProductionLanguage.ID;
                        entry = MarkEntityAsUpdated(entities, productionLanguage);
                    }
                    else
                    {
                        productionLanguage.ID = GetNewID<ProductionLanguage>(entities, e => e.ID);
                        entry = entities.ProductionLanguage.Add(productionLanguage);
                    }
                    entities.SaveChanges();
                    DetachAllEntries(entities);
                }
            }
        }

        private static void HandleKeywords(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Movie movie = production as Movie;
            Movie savedMovie = savedProduction as Movie;
            if (movie != null && movie.Keywords != null)
            {
                foreach (Keyword keyword in movie.Keywords.ToArray())
                {
                    EntityEntry entry = null;
                    bool saved = false;
                    if (savedMovie != null)
                    {
                        Keyword savedKeyword = savedMovie.Keywords.FirstOrDefault(e => e.Identifier == keyword.Identifier);
                        if (savedKeyword != null)
                        {
                            keyword.ID = savedKeyword.ID;
                            entry = MarkEntityAsUpdated(entities, keyword);
                            saved = true;
                        }
                    }

                    keyword.ProductionID = production.ID;
                    if (!saved)
                    {
                        keyword.ID = GetNewID<Keyword>(entities, e => e.ID);
                        entry = entities.Keyword.Add(keyword);
                    }

                    entities.SaveChanges();
                    DetachAllEntries(entities);
                }
            }
        }

        private static void HandleGenres(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Movie movie = production as Movie;
            Movie savedMovie = savedProduction as Movie;
            if (movie != null && movie.Genres != null)
            {
                foreach (Genre genre in movie.Genres.ToArray())
                {
                    EntityEntry entry = null;
                    bool saved = false;
                    if (savedMovie != null)
                    {
                        Genre savedGenre = savedMovie.Genres?.FirstOrDefault(e => e.Identifier == genre.Identifier);
                        if (savedGenre != null)
                        {
                            genre.ID = savedGenre.ID;
                            entry = MarkEntityAsUpdated(entities, genre);
                            saved = true;
                        }
                    }

                    genre.ProductionID = production.ID;
                    if (!saved)
                    {
                        genre.ID = GetNewID<Genre>(entities, e => e.ID);
                        entry = entities.Genre.Add(genre);
                    }
                    entities.SaveChanges();
                    DetachAllEntries(entities);
                }
            }
        }

        private static void HandleDataSources(JMoviesEntities entities, Production production, Production savedProduction)
        {
            EntityEntry entry = null;
            if (production.Rating != null)
            {
                if (entities.DataSource.FirstOrDefault(e => e.Identifier == production.Rating.DataSource.Identifier) == null)
                {
                    entry = entities.DataSource.Add(production.Rating.DataSource);
                }
            }
            entities.SaveChanges();
            DetachAllEntries(entities);
        }

        private static void HandleCountries(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Movie movie = production as Movie;
            Movie savedMovie = savedProduction as Movie;

            if (movie != null && movie.Countries != null)
            {
                foreach (ProductionCountry productionCountry in movie.Countries.ToArray())
                {
                    EntityEntry entry = null;
                    EntityEntry countryEntry = null;
                    Country savedCountry = entities.Country.FirstOrDefault(e => e.Identifier == productionCountry.Country.Identifier);
                    if (savedCountry != null)
                    {
                        productionCountry.Country.ID = savedCountry.ID;
                        countryEntry = MarkEntityAsUpdated(entities, productionCountry.Country);
                    }
                    else
                    {
                        productionCountry.Country.ID = GetNewID<Country>(entities, e => e.ID);
                        countryEntry = entities.Country.Add(productionCountry.Country);
                    }
                    entities.SaveChanges();
                    productionCountry.ProductionID = productionCountry.Production.ID;
                    productionCountry.Production = null;
                    productionCountry.CountryID = productionCountry.Country.ID;
                    productionCountry.Country = null;

                    ProductionCountry savedProductionCountry = savedMovie?.Countries?.FirstOrDefault(e => e.CountryID == productionCountry.CountryID);
                    if (savedProductionCountry != null)
                    {
                        productionCountry.ID = savedProductionCountry.ID;
                        entry = MarkEntityAsUpdated(entities, productionCountry);
                    }
                    else
                    {
                        DetachAllEntries(entities);
                        productionCountry.ID = GetNewID<ProductionCountry>(entities, e => e.ID);
                        entry = entities.ProductionCountry.Add(productionCountry);
                    }

                    entities.SaveChanges();
                    DetachAllEntries(entities);
                }
            }
        }

        private static void HandleCompanies(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Movie movie = production as Movie;
            Movie savedMovie = savedProduction as Movie;

            if (movie != null && movie.ProductionCompanies != null)
            {
                foreach (Company company in movie.ProductionCompanies.ToArray())
                {
                    EntityEntry entry = null;
                    bool saved = false;
                    if (savedMovie != null)
                    {
                        Company savedCompany = savedMovie.ProductionCompanies?.FirstOrDefault(e => e.Name == company.Name);
                        if (savedCompany != null)
                        {
                            company.ID = savedCompany.ID;
                            entry = MarkEntityAsUpdated(entities, company);
                            saved = true;
                        }
                    }

                    company.ProductionID = production.ID;
                    if (!saved)
                    {
                        company.ID = GetNewID<Company>(entities, e => e.ID);
                        entry = entities.Company.Add(company);
                    }
                    entities.SaveChanges();
                    DetachAllEntries(entities);
                }
                movie.ProductionCompanies = null;
            }
        }

        private static void HandleCharacters(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Movie movie = production as Movie;
            Movie savedMovie = savedProduction as Movie;

            if (movie != null && movie.Credits != null)
            {
                foreach (Credit credit in movie.Credits.ToArray())
                {
                    if (credit is ActingCredit)
                    {
                        ActingCredit actingCredit = credit as ActingCredit;
                        foreach (Character character in actingCredit.Characters.ToArray())
                        {
                            EntityEntry entry = null;
                            bool saved = false;
                            if (savedMovie != null)
                            {
                                Character savedCharacter = entities.Character.FirstOrDefault(e => FindCharacter(e, savedMovie, character));
                                if (savedCharacter != null)
                                {
                                    character.ID = savedCharacter.ID;
                                    entry = MarkEntityAsUpdated(entities, character, new string[] { "CharacterType" });
                                    saved = true;
                                }
                            }

                            character.CreditID = actingCredit.ID;
                            if (!saved)
                            {
                                character.ID = GetNewID<Character>(entities, e => e.ID);
                                entry = entities.Character.Add(character);
                            }
                            entities.SaveChanges();
                            DetachAllEntries(entities);
                        }
                    }
                }
            }
        }

        private static bool FindCharacter(Character currentCharacter, Movie savedMovie, Character searchedCharacter)
        {
            return savedMovie.Credits?.Where(x => x.RoleType == CreditRoleType.Acting).Select(x => x.ID).ToArray().Contains(currentCharacter.CreditID) == true && currentCharacter.Name == searchedCharacter.Name && currentCharacter.IMDbID == searchedCharacter.IMDbID;
        }

        private static void DetachAllEntries(JMoviesEntities entities)
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

        private static void HandleAKAs(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Movie movie = production as Movie;
            Movie savedMovie = savedProduction as Movie;

            if (movie != null && movie.AKAs != null)
            {
                foreach (AKA aka in movie.AKAs.ToArray())
                {
                    EntityEntry entry = null;
                    bool saved = false;
                    if (savedMovie != null)
                    {
                        AKA savedAKA = savedMovie.AKAs?.FirstOrDefault(e => e.Name == aka.Name && e.Description == aka.Description);
                        if (savedAKA != null)
                        {
                            aka.ID = savedAKA.ID;
                            entry = MarkEntityAsUpdated(entities, aka);
                            saved = true;
                        }
                    }

                    aka.ProductionID = production.ID;
                    if (!saved)
                    {
                        aka.ID = GetNewID<AKA>(entities, e => e.ID);
                        entry = entities.AKA.Add(aka);
                    }
                    entities.SaveChanges();
                    DetachAllEntries(entities);
                }
            }
        }
    }
}
