using JMovies.Common.Constants;
using JMovies.DataAccess.Converters;
using JMovies.DataAccess.Entities;
using JMovies.DataAccess.Entities.Persisters;
using JMovies.DataAccess.Entities.ResultHandling;
using JMovies.Entities.UserManagement;
using JMovies.IMDb.Entities.Common;
using JMovies.IMDb.Entities.Misc;
using JMovies.IMDb.Entities.Movies;
using JMovies.IMDb.Entities.People;
using JMovies.Utilities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies.Internal;
using System;
using System.Collections.Generic;

namespace JMovies.DataAccess
{
    public class JMoviesEntities : DbContext
    {
        public JMoviesEntities(DbContextOptions<JMoviesEntities> options)
               : base(options)
        {
        }

        public JMoviesEntities() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ProxiesOptionsExtension proxiesOptionsExtension = optionsBuilder.Options?.FindExtension<ProxiesOptionsExtension>();
            if (proxiesOptionsExtension == null)
            {
                optionsBuilder.UseLazyLoadingProxies(false);
            }
            optionsBuilder.EnableDetailedErrors();
            if (!EnvironmentUtilities.IsProduction())
            {
                optionsBuilder.EnableSensitiveDataLogging();
            }

            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable(ConfigurationConstants.ConnectionStringEnvironmentName), b =>
            {
                b.MigrationsAssembly("JMovies.App");
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            LengthConverter lengthConverter = new LengthConverter();
            JsonConverter<ICollection<string>> stringArrayConverter = new JsonConverter<ICollection<string>>();
            JsonConverter<ICollection<CreditRoleType>> creditRoleTypeConverter = new JsonConverter<ICollection<CreditRoleType>>();
            JsonConverter<Budget> budgetConverter = new JsonConverter<Budget>();
            JsonConverter<ICollection<OfficialSite>> officialSitesConverter = new JsonConverter<ICollection<OfficialSite>>();

            modelBuilder.Entity<Person>().Property(e => e.Height).HasConversion(lengthConverter);
            modelBuilder.Entity<Movie>().Property(e => e.FilmingLocations).HasConversion(stringArrayConverter);
            modelBuilder.Entity<Movie>().Property(e => e.Budget).HasConversion(budgetConverter);
            modelBuilder.Entity<Movie>().Property(e => e.OfficialSites).HasConversion(officialSitesConverter);
            modelBuilder.Entity<Person>().Property(e => e.Roles).HasConversion(creditRoleTypeConverter);
            modelBuilder.Entity<PersisterHistory>().Property(e => e.IsSuccess).HasConversion<short>();
            modelBuilder.Entity<Image>().Property(e => e.Content).HasColumnType("BYTEA");

            modelBuilder.Entity<Production>()
             .HasDiscriminator<ProductionTypeEnum>("ProductionType")
             .HasValue<Production>(ProductionTypeEnum.Undefined)
             .HasValue<TVSeries>(ProductionTypeEnum.TVSeries)
             .HasValue<Movie>(ProductionTypeEnum.Movie);

            modelBuilder.Entity<Credit>()
         .HasDiscriminator<CreditRoleType>("RoleType")
         .HasValue<Credit>(CreditRoleType.Undefined)
         .HasValue<ActingCredit>(CreditRoleType.Actor)
         .HasValue<ActingCredit>(CreditRoleType.Actress)
         .HasValue<Credit>(CreditRoleType.Composer)
         .HasValue<Credit>(CreditRoleType.Creator)
         .HasValue<Credit>(CreditRoleType.Director)
         .HasValue<Credit>(CreditRoleType.MusicDepartment)
         .HasValue<Credit>(CreditRoleType.Producer)
         .HasValue<Credit>(CreditRoleType.Writer)
         .HasValue<ActingCredit>(CreditRoleType.Acting);

            modelBuilder.Entity<Character>()
         .HasDiscriminator<CharacterTypeEnum>("CharacterType")
         .HasValue<Character>(CharacterTypeEnum.Character)
         .HasValue<TVCharacter>(CharacterTypeEnum.TVCharacter);

            base.OnModelCreating(modelBuilder);
        }


        //Entities
        public DbSet<AKA> AKA { get; set; }
        public DbSet<Character> Character { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Credit> Credit { get; set; }
        public DbSet<DataSource> DataSource { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Keyword> Keyword { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Production> Production { get; set; }
        public DbSet<ProductionCredit> ProductionCredit { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<ReleaseDate> ReleaseDate { get; set; }
        public DbSet<Resource> Resource { get; set; }
        public DbSet<ResourceTranslation> ResourceTranslation { get; set; }
        public DbSet<ResultConfiguration> ResultConfiguration { get; set; }
        public DbSet<ResultMessage> ResultMessage { get; set; }
        public DbSet<PersisterHistory> PersisterHistory { get; set; }
        public DbSet<ProductionCountry> ProductionCountry { get; set; }
        public DbSet<ProductionLanguage> ProductionLanguage { get; set; }
        public DbSet<TagLine> TagLine { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Password> Password { get; set; }
    }
}
