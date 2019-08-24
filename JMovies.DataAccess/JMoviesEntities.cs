﻿using JMovies.DataAccess.Converters;
using JMovies.IMDb.Entities.Common;
using JMovies.IMDb.Entities.Misc;
using JMovies.IMDb.Entities.Movies;
using JMovies.IMDb.Entities.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            optionsBuilder.UseMySQL("server=remotemysql.com;port=3306;database=3HOGbi1TUW;user=3HOGbi1TUW;password=wUu4OBDGA8", b => b.MigrationsAssembly("JMovies.App"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            LengthConverter lengthConverter = new LengthConverter();
            JsonConverter<Image[]> imageConverter = new JsonConverter<Image[]>();
            JsonConverter<ICollection<string>> stringArrayConverter = new JsonConverter<ICollection<string>>();
            JsonConverter<ICollection<CreditRoleType>> creditRoleTypeConverter = new JsonConverter<ICollection<CreditRoleType>>();
            JsonConverter<Budget> budgetConverter = new JsonConverter<Budget>();
            JsonConverter<ICollection<OfficialSite>> officialSitesConverter = new JsonConverter<ICollection<OfficialSite>>();

            modelBuilder.Entity<Person>().Property(e => e.Height).HasConversion(lengthConverter);
            modelBuilder.Entity<Person>().Property(e => e.Photos).HasConversion(imageConverter);
            modelBuilder.Entity<Movie>().Property(e => e.TagLines).HasConversion(stringArrayConverter);
            modelBuilder.Entity<Movie>().Property(e => e.FilmingLocations).HasConversion(stringArrayConverter);
            modelBuilder.Entity<Movie>().Property(e => e.Budget).HasConversion(budgetConverter);
            modelBuilder.Entity<Movie>().Property(e => e.OfficialSites).HasConversion(officialSitesConverter);
            modelBuilder.Entity<Person>().Property(e => e.Roles).HasConversion(creditRoleTypeConverter);

            modelBuilder.Entity<Production>()
         .HasDiscriminator<ProductionTypeEnum>("ProductionType")
         .HasValue<Production>(ProductionTypeEnum.Undefined)
         .HasValue<TVSeries>(ProductionTypeEnum.TVSeries)
         .HasValue<Movie>(ProductionTypeEnum.Movie);


            modelBuilder.Entity<Person>()
         .HasDiscriminator<PersonTypeEnum>("PersonType")
         .HasValue<Person>(PersonTypeEnum.Undefined)
         .HasValue<Actor>(PersonTypeEnum.Actor);

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
        public DbSet<Keyword> Keyword { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Production> Production { get; set; }
        public DbSet<ProductionCredit> ProductionCredit { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<ReleaseDate> ReleaseDate { get; set; }
    }
}