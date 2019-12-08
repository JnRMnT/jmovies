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
using JMovies.IMDb.Entities.Common;
using JMovies.IMDb.Entities.Misc;
using JMovies.DataAccess.Helpers;

namespace JMovies.App.Business.Managers
{
    public class ProductionPersistanceManager
    {
        public static void Persist(JMoviesEntities entities, Production production)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.Serializable;
            options.Timeout = new TimeSpan(0, 10, 0);
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                Production savedProduction = entities.Production.FirstOrDefault(e => e.IMDbID == production.IMDbID);

                bool saved = false;
                if (savedProduction != null)
                {
                    production.ID = savedProduction.ID;
                    saved = true;
                }
                else
                {
                    production.ID = CommonDBHelper.GetNewID<Production>(entities, e => e.ID);
                }

                Production trimmedProduction = GetTrimmedProduction(production);
                if (!saved)
                {
                    entities.Production.Add(trimmedProduction);
                }
                entities.SaveChanges();
                HandleAKAs(entities, production, savedProduction);
                HandleCompanies(entities, production, savedProduction);
                HandleDataSources(entities, production, savedProduction);
                HandleGenres(entities, production, savedProduction);
                HandleKeywords(entities, production, savedProduction);
                HandleLanguages(entities, production);
                HandleCountries(entities, production, savedProduction);
                HandlePersons(entities, production);
                HandleCharacters(entities, production, savedProduction);
                HandleRatings(entities, production, savedProduction);
                HandleReleaseDates(entities, production, savedProduction);
                HandleImages(entities, production, savedProduction);
                HandleTagLines(entities, production, savedProduction);
                CommonDBHelper.MarkEntityAsUpdated(entities, trimmedProduction, new string[] { "ProductionType", "Poster", "PosterID" }, true);
                entities.SaveChanges();
                scope.Complete();
            }
        }

        private static void HandleTagLines(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Movie movie = production as Movie;
            Movie savedMovie = savedProduction as Movie;

            if (savedMovie != null)
            {
                savedMovie.TagLines = entities.TagLine.Where(e => e.ProductionID == savedMovie.ID).ToArray();
                entities.TagLine.RemoveRange(savedMovie.TagLines);
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

        private static void HandleReleaseDates(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Movie savedMovie = savedProduction as Movie;
            if (savedMovie != null)
            {
                ReleaseDate[] existingReleaseDates = entities.ReleaseDate.Where(e => e.ProductionID == savedMovie.ID).ToArray();
                entities.ReleaseDate.RemoveRange(existingReleaseDates);
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
                            entry = CommonDBHelper.MarkEntityAsUpdated(entities, releaseDate.Country);
                        }
                        else
                        {
                            releaseDate.Country.ID = CommonDBHelper.GetNewID<Country>(entities, e => e.ID);
                            entry = entities.Country.Add(releaseDate.Country);
                        }
                    }

                    releaseDate.CountryID = releaseDate.Country.ID;
                    releaseDate.Country = null;
                    releaseDate.ProductionID = production.ID;
                    releaseDate.ID = CommonDBHelper.GetNewID<ReleaseDate>(entities, e => e.ID);
                    entities.ReleaseDate.Add(releaseDate);

                    entities.SaveChanges();
                    CommonDBHelper.DetachAllEntries(entities);
                }
            }
        }

        private static void HandleRatings(JMoviesEntities entities, Production production, Production savedProduction)
        {
            if (production.Rating != null)
            {
                EntityEntry entry = null;
                production.Rating.DataSourceID = entities.DataSource.FirstOrDefault(e => e.Identifier == production.Rating.DataSource.Identifier).ID;
                production.Rating.DataSource = null;

                production.Rating.ProductionID = production.ID;
                production.Rating.Production = null;

                Rating existingRating = null;
                if (savedProduction != null)
                {
                    existingRating = entities.Rating.FirstOrDefault(e => e.ProductionID == savedProduction.ID);
                }

                if (existingRating != null)
                {
                    production.Rating.ID = existingRating.ID;
                    entry = CommonDBHelper.MarkEntityAsUpdated(entities, production.Rating);
                }
                else
                {
                    production.Rating.ID = CommonDBHelper.GetNewID<Rating>(entities, e => e.ID);
                    entry = entities.Rating.Add(production.Rating);
                }
                entities.SaveChanges();
                CommonDBHelper.DetachAllEntries(entities);
            }
        }

        private static void HandlePersons(JMoviesEntities entities, Production production)
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
                        //personEntry = CommonDBHelper.MarkEntityAsUpdated(entities, credit.Person, new string[] { "PersonType" });
                    }
                    else
                    {
                        credit.Person.ID = CommonDBHelper.GetNewID<Person>(entities, e => e.ID);
                        personEntry = entities.Person.Add(credit.Person);
                    }
                    entities.SaveChanges();

                    existingCredit = entities.Credit.FirstOrDefault(e => e.Person.ID == credit.Person.ID && e.ProductionID == production.ID);
                    CommonDBHelper.DetachAllEntries(entities);

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
                        entry = CommonDBHelper.MarkEntityAsUpdated(entities, credit, new string[] { "RoleType", "Characters", "Person" });
                    }
                    else
                    {
                        //credit.ID = CommonDBHelper.GetNewID<Credit>(entities, e => e.ID);
                        entry = entities.Credit.Add(credit);
                    }

                    entities.SaveChanges();

                    if (characters != null)
                    {
                        ((ActingCredit)credit).Characters = characters;
                    }
                    CommonDBHelper.DetachAllEntries(entities);
                }
            }
        }

        private static void HandleLanguages(JMoviesEntities entities, Production production)
        {
            Movie movie = production as Movie;

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
                        languageEntry = CommonDBHelper.MarkEntityAsUpdated(entities, productionLanguage.Language);
                    }
                    else
                    {
                        productionLanguage.Language.ID = CommonDBHelper.GetNewID<Language>(entities, e => e.ID);
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
                        entry = CommonDBHelper.MarkEntityAsUpdated(entities, productionLanguage);
                    }
                    else
                    {
                        productionLanguage.ID = CommonDBHelper.GetNewID<ProductionLanguage>(entities, e => e.ID);
                        entry = entities.ProductionLanguage.Add(productionLanguage);
                    }
                    entities.SaveChanges();
                    CommonDBHelper.DetachAllEntries(entities);
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
                        Keyword savedKeyword = entities.Keyword.FirstOrDefault(e => e.Identifier == keyword.Identifier && keyword.ProductionID == movie.ID);
                        if (savedKeyword != null)
                        {
                            keyword.ID = savedKeyword.ID;
                            entry = CommonDBHelper.MarkEntityAsUpdated(entities, keyword);
                            saved = true;
                        }
                    }

                    keyword.ProductionID = production.ID;
                    if (!saved)
                    {
                        keyword.ID = CommonDBHelper.GetNewID<Keyword>(entities, e => e.ID);
                        entry = entities.Keyword.Add(keyword);
                    }

                    entities.SaveChanges();
                    CommonDBHelper.DetachAllEntries(entities);
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
                        Genre savedGenre = entities.Genre.FirstOrDefault(e => e.Identifier == genre.Identifier && genre.ProductionID == savedMovie.ID);
                        if (savedGenre != null)
                        {
                            genre.ID = savedGenre.ID;
                            entry = CommonDBHelper.MarkEntityAsUpdated(entities, genre);
                            saved = true;
                        }
                    }

                    genre.ProductionID = production.ID;
                    if (!saved)
                    {
                        genre.ID = CommonDBHelper.GetNewID<Genre>(entities, e => e.ID);
                        entry = entities.Genre.Add(genre);
                    }
                    entities.SaveChanges();
                    CommonDBHelper.DetachAllEntries(entities);
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
            CommonDBHelper.DetachAllEntries(entities);
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
                        countryEntry = CommonDBHelper.MarkEntityAsUpdated(entities, productionCountry.Country);
                    }
                    else
                    {
                        productionCountry.Country.ID = CommonDBHelper.GetNewID<Country>(entities, e => e.ID);
                        countryEntry = entities.Country.Add(productionCountry.Country);
                    }
                    entities.SaveChanges();
                    productionCountry.ProductionID = productionCountry.Production.ID;
                    productionCountry.Production = null;
                    productionCountry.CountryID = productionCountry.Country.ID;
                    productionCountry.Country = null;

                    ProductionCountry savedProductionCountry = null;
                    if (savedMovie != null)
                    {
                        savedProductionCountry = entities.ProductionCountry.FirstOrDefault(e => e.ProductionID == savedMovie.ID && e.CountryID == productionCountry.CountryID);
                    }

                    if (savedProductionCountry != null)
                    {
                        productionCountry.ID = savedProductionCountry.ID;
                        entry = CommonDBHelper.MarkEntityAsUpdated(entities, productionCountry);
                    }
                    else
                    {
                        CommonDBHelper.DetachAllEntries(entities);
                        productionCountry.ID = CommonDBHelper.GetNewID<ProductionCountry>(entities, e => e.ID);
                        entry = entities.ProductionCountry.Add(productionCountry);
                    }

                    entities.SaveChanges();
                    CommonDBHelper.DetachAllEntries(entities);
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
                        Company savedCompany = entities.Company.FirstOrDefault(e => e.Name == company.Name && company.ProductionID == savedMovie.ID);
                        if (savedCompany != null)
                        {
                            company.ID = savedCompany.ID;
                            entry = CommonDBHelper.MarkEntityAsUpdated(entities, company);
                            saved = true;
                        }
                    }

                    company.ProductionID = production.ID;
                    if (!saved)
                    {
                        company.ID = CommonDBHelper.GetNewID<Company>(entities, e => e.ID);
                        entry = entities.Company.Add(company);
                    }
                    entities.SaveChanges();
                    CommonDBHelper.DetachAllEntries(entities);
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
                                if (savedMovie.Credits == null)
                                {
                                    savedMovie.Credits = entities.Credit.Where(e => e.ProductionID == savedMovie.ID).ToArray();
                                }
                                Character savedCharacter = null;
                                if (savedMovie.Credits != null)
                                {
                                    long[] existingCreditIDs = savedMovie.Credits.Where(x => x.RoleType == CreditRoleType.Acting).Select(x => x.ID).ToArray();
                                    savedCharacter = entities.Character.FirstOrDefault(currentCharacter => existingCreditIDs.Contains(currentCharacter.CreditID) && currentCharacter.Name == character.Name && currentCharacter.IMDbID == character.IMDbID);
                                }

                                if (savedCharacter != null)
                                {
                                    character.ID = savedCharacter.ID;
                                    entry = CommonDBHelper.MarkEntityAsUpdated(entities, character, new string[] { "CharacterType" });
                                    saved = true;
                                }
                            }

                            character.CreditID = actingCredit.ID;
                            if (!saved)
                            {
                                character.ID = CommonDBHelper.GetNewID<Character>(entities, e => e.ID);
                                entry = entities.Character.Add(character);
                            }
                            entities.SaveChanges();
                            CommonDBHelper.DetachAllEntries(entities);
                        }
                    }
                }
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
                        AKA savedAKA = entities.AKA.FirstOrDefault(e => e.Name == aka.Name && e.Description == aka.Description && aka.ProductionID == savedMovie.ID);
                        if (savedAKA != null)
                        {
                            aka.ID = savedAKA.ID;
                            entry = CommonDBHelper.MarkEntityAsUpdated(entities, aka);
                            saved = true;
                        }
                    }

                    aka.ProductionID = production.ID;
                    if (!saved)
                    {
                        aka.ID = CommonDBHelper.GetNewID<AKA>(entities, e => e.ID);
                        entry = entities.AKA.Add(aka);
                    }
                    entities.SaveChanges();
                    CommonDBHelper.DetachAllEntries(entities);
                }
            }
        }

        private static void HandleImages(JMoviesEntities entities, Production production, Production savedProduction)
        {
            Image oldPoster = savedProduction?.Poster;
            if (savedProduction != null && oldPoster == null)
            {
                oldPoster = entities.Image.FirstOrDefault(e => e.ID == savedProduction.PosterID);
            }

            Production trackedProduction = entities.Production.FirstOrDefault(e => e.ID == production.ID);
            if (savedProduction != null && oldPoster != null)
            {
                trackedProduction.PosterID = null;
                trackedProduction.Poster = null;
                entities.Image.Remove(oldPoster);
                entities.SaveChanges();
            }

            if (production.Poster != null)
            {
                production.Poster.ProductionID = production.ID;
                production.Poster.ID = CommonDBHelper.GetNewID<Image>(entities, e => e.ID);
                entities.Image.Add(production.Poster);
                trackedProduction.PosterID = production.Poster.ID;
            }
            entities.SaveChanges();
            CommonDBHelper.DetachAllEntries(entities);

            if (production.MediaImages != null)
            {
                foreach (Image image in production.MediaImages)
                {
                    Image savedImage = entities.Image.FirstOrDefault(e => e.URL == image.URL);
                    if (savedImage == null)
                    {
                        image.ID = CommonDBHelper.GetNewID<Image>(entities, e => e.ID);
                        image.ProductionID = production.ID;
                        entities.Add(image);
                    }
                    entities.SaveChanges();
                    CommonDBHelper.DetachAllEntries(entities);
                }
            }
        }
    }
}
