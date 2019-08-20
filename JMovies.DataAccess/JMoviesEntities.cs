using JMovies.DataAccess.Converters;
using JMovies.DataAccess.Entities.Common;
using JMovies.DataAccess.Entities.Movies;
using JMovies.DataAccess.Entities.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
            JsonConverter<jm_Image[]> imageConverter = new JsonConverter<jm_Image[]>();
            JsonConverter<string[]> stringArrayConverter = new JsonConverter<string[]>();
            JsonConverter<jm_CreditRoleType[]> creditRoleTypeConverter = new JsonConverter<jm_CreditRoleType[]>();
            JsonConverter<jm_Budget> budgetConverter = new JsonConverter<jm_Budget>();
            JsonConverter<jm_OfficialSite[]> officialSitesConverter = new JsonConverter<jm_OfficialSite[]>();

            modelBuilder.Entity<jm_Person>().Property(e => e.Height).HasConversion(lengthConverter);
            modelBuilder.Entity<jm_Person>().Property(e => e.Photos).HasConversion(imageConverter);
            modelBuilder.Entity<jm_Movie>().Property(e => e.TagLines).HasConversion(stringArrayConverter);
            modelBuilder.Entity<jm_Movie>().Property(e => e.FilmingLocations).HasConversion(stringArrayConverter);
            modelBuilder.Entity<jm_Movie>().Property(e => e.Budget).HasConversion(budgetConverter);
            modelBuilder.Entity<jm_Movie>().Property(e => e.OfficialSites).HasConversion(officialSitesConverter);
            modelBuilder.Entity<jm_Person>().Property(e => e.Roles).HasConversion(creditRoleTypeConverter);

            modelBuilder.Entity<jm_Production>()
         .HasDiscriminator<jm_ProductionTypeEnum>("ProductionType")
         .HasValue<jm_Production>(jm_ProductionTypeEnum.Undefined)
         .HasValue<jm_TVSeries>(jm_ProductionTypeEnum.TVSeries)
         .HasValue<jm_Movie>(jm_ProductionTypeEnum.Movie);


            modelBuilder.Entity<jm_Person>()
         .HasDiscriminator<jm_PersonType>("PersonType")
         .HasValue<jm_Person>(jm_PersonType.Undefined)
         .HasValue<jm_Actor>(jm_PersonType.Actor);

            modelBuilder.Entity<jm_Character>()
         .HasDiscriminator<jm_CharacterType>("CharacterType")
         .HasValue<jm_Character>(jm_CharacterType.Character)
         .HasValue<jm_TVCharacter>(jm_CharacterType.TVCharacter);

            base.OnModelCreating(modelBuilder);
        }


        //Entities
        public DbSet<jm_AKA> AKA { get; set; }
        public DbSet<jm_Character> Character { get; set; }
        public DbSet<jm_Company> Company { get; set; }
        public DbSet<jm_Country> Country { get; set; }
        public DbSet<jm_Credit> Credit { get; set; }
        public DbSet<jm_DataSource> DataSource { get; set; }
        public DbSet<jm_Genre> Genre { get; set; }
        public DbSet<jm_Keyword> Keyword { get; set; }
        public DbSet<jm_Language> Language { get; set; }
        public DbSet<jm_Person> Person { get; set; }
        public DbSet<jm_Production> Production { get; set; }
        public DbSet<jm_ProductionCredit> ProductionCredit { get; set; }
        public DbSet<jm_Rating> Rating { get; set; }
        public DbSet<jm_ReleaseDate> ReleaseDate { get; set; }
    }
}
