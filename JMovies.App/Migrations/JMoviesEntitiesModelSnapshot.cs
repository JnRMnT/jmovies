﻿// <auto-generated />
using System;
using JMovies.DataAccess;
using JMovies.IMDb.Entities.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JMovies.App.Migrations
{
    [DbContext(typeof(JMoviesEntities))]
    partial class JMoviesEntitiesModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("JMovies.DataAccess.Entities.Persisters.PersisterHistory", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("DataID");

                    b.Property<int>("DataSourceID");

                    b.Property<int>("EntityTypeID")
                        .HasMaxLength(4);

                    b.Property<string>("ErrorMessage");

                    b.Property<DateTime>("ExecuteDate");

                    b.Property<short>("IsSuccess");

                    b.HasKey("ID");

                    b.HasIndex("DataSourceID");

                    b.ToTable("PersisterHistory");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Resource", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("ID");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.ResourceTranslation", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Culture")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.Property<long>("ResourceID");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("ResourceID");

                    b.ToTable("ResourceTranslation");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Common.Image", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Content")
                        .HasColumnType("MEDIUMBLOB");

                    b.Property<long?>("PersonID");

                    b.Property<long?>("ProductionID");

                    b.Property<string>("Title")
                        .HasMaxLength(255);

                    b.Property<string>("URL")
                        .HasColumnName("SourceURL")
                        .HasMaxLength(255);

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Misc.DataSource", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Identifier");

                    b.Property<string>("Name")
                        .HasMaxLength(32);

                    b.HasKey("ID");

                    b.ToTable("DataSource");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.AKA", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(64);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<long>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("AKA");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Character", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CharacterType")
                        .HasMaxLength(2);

                    b.Property<long>("CreditID");

                    b.Property<long?>("IMDbID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("ID");

                    b.HasIndex("CreditID");

                    b.ToTable("Character");

                    b.HasDiscriminator<int>("CharacterType").HasValue(0);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Company", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<long>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Country", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Identifier")
                        .HasMaxLength(64);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.HasKey("ID");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Credit", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("PersonID");

                    b.Property<long>("ProductionID");

                    b.Property<int>("RoleType")
                        .HasMaxLength(2);

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Credit");

                    b.HasDiscriminator<int>("RoleType").HasValue(3);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Genre", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Identifier")
                        .HasMaxLength(36);

                    b.Property<long>("ProductionID");

                    b.Property<string>("Value")
                        .HasColumnName("Name")
                        .HasMaxLength(64);

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Keyword", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Identifier")
                        .HasMaxLength(36);

                    b.Property<long>("ProductionID");

                    b.Property<string>("Value")
                        .HasColumnName("Name")
                        .HasMaxLength(64);

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Keyword");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Language", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Identifier")
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .HasMaxLength(64);

                    b.HasKey("ID");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Production", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("IMDbID");

                    b.Property<long?>("PosterID");

                    b.Property<int>("ProductionType");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("Year")
                        .HasMaxLength(4);

                    b.HasKey("ID");

                    b.HasIndex("PosterID");

                    b.ToTable("Production");

                    b.HasDiscriminator<int>("ProductionType").HasValue(0);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ProductionCountry", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CountryID");

                    b.Property<long>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.HasIndex("ProductionID");

                    b.ToTable("ProductionCountry");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ProductionLanguage", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("LanguageID");

                    b.Property<long>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("LanguageID");

                    b.HasIndex("ProductionID");

                    b.ToTable("ProductionLanguage");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Rating", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DataSourceID");

                    b.Property<long>("ProductionID");

                    b.Property<long>("RateCount");

                    b.Property<double>("Value");

                    b.HasKey("ID");

                    b.HasIndex("DataSourceID");

                    b.HasIndex("ProductionID")
                        .IsUnique();

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ReleaseDate", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CountryID");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .HasMaxLength(64);

                    b.Property<long>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.HasIndex("ProductionID");

                    b.ToTable("ReleaseDate");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.TagLine", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<long?>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("TagLine");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.People.Person", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("BirthName")
                        .HasMaxLength(128);

                    b.Property<string>("BirthPlace")
                        .HasMaxLength(128);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("Gender")
                        .HasMaxLength(2);

                    b.Property<int?>("Height")
                        .HasMaxLength(4);

                    b.Property<long>("IMDbID");

                    b.Property<string>("MiniBiography");

                    b.Property<string>("NickName")
                        .HasMaxLength(128);

                    b.Property<string>("PrimaryImage")
                        .HasMaxLength(255);

                    b.Property<string>("Roles")
                        .HasMaxLength(128);

                    b.HasKey("ID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.People.ProductionCredit", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreditID");

                    b.Property<long?>("PersonID");

                    b.Property<long?>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("CreditID");

                    b.HasIndex("PersonID");

                    b.HasIndex("ProductionID");

                    b.ToTable("ProductionCredit");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.TVCharacter", b =>
                {
                    b.HasBaseType("JMovies.IMDb.Entities.Movies.Character");

                    b.Property<int?>("EndYear")
                        .HasMaxLength(4);

                    b.Property<int>("EpisodeCount")
                        .HasMaxLength(4);

                    b.Property<int?>("StartYear")
                        .HasMaxLength(4);

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ActingCredit", b =>
                {
                    b.HasBaseType("JMovies.IMDb.Entities.Movies.Credit");

                    b.ToTable("Credit");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Movie", b =>
                {
                    b.HasBaseType("JMovies.IMDb.Entities.Movies.Production");

                    b.Property<string>("Budget")
                        .HasMaxLength(256);

                    b.Property<string>("FilmingLocations")
                        .HasMaxLength(256);

                    b.Property<string>("OfficialSites")
                        .HasMaxLength(512);

                    b.Property<string>("OriginalTitle")
                        .HasMaxLength(128);

                    b.Property<string>("PlotSummary")
                        .HasMaxLength(512);

                    b.Property<TimeSpan>("Runtime");

                    b.Property<string>("StoryLine");

                    b.ToTable("Movie");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.TVSeries", b =>
                {
                    b.HasBaseType("JMovies.IMDb.Entities.Movies.Movie");

                    b.Property<int?>("EndYear")
                        .HasMaxLength(4);

                    b.ToTable("TVSeries");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Persisters.PersisterHistory", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Misc.DataSource", "DataSource")
                        .WithMany()
                        .HasForeignKey("DataSourceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.ResourceTranslation", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Resource")
                        .WithMany("Translations")
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Common.Image", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.People.Person")
                        .WithMany("Photos")
                        .HasForeignKey("PersonID");

                    b.HasOne("JMovies.IMDb.Entities.Movies.Production")
                        .WithMany("MediaImages")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.AKA", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie")
                        .WithMany("AKAs")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Character", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.ActingCredit")
                        .WithMany("Characters")
                        .HasForeignKey("CreditID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Company", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie")
                        .WithMany("ProductionCompanies")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Credit", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.People.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie")
                        .WithMany("Credits")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Genre", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie")
                        .WithMany("Genres")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Keyword", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie")
                        .WithMany("Keywords")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Production", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Common.Image", "Poster")
                        .WithMany()
                        .HasForeignKey("PosterID");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ProductionCountry", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie")
                        .WithMany("Countries")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JMovies.IMDb.Entities.Movies.Production", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID")
                        .HasConstraintName("FK_ProductionCountry_Production_ProductionID1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ProductionLanguage", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie")
                        .WithMany("Languages")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JMovies.IMDb.Entities.Movies.Production", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID")
                        .HasConstraintName("FK_ProductionLanguage_Production_ProductionID1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Rating", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Misc.DataSource", "DataSource")
                        .WithMany()
                        .HasForeignKey("DataSourceID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JMovies.IMDb.Entities.Movies.Production", "Production")
                        .WithOne("Rating")
                        .HasForeignKey("JMovies.IMDb.Entities.Movies.Rating", "ProductionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ReleaseDate", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie")
                        .WithMany("ReleaseDates")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.TagLine", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie")
                        .WithMany("TagLines")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.People.ProductionCredit", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Credit", "Credit")
                        .WithMany()
                        .HasForeignKey("CreditID");

                    b.HasOne("JMovies.IMDb.Entities.People.Person")
                        .WithMany("KnownFor")
                        .HasForeignKey("PersonID");

                    b.HasOne("JMovies.IMDb.Entities.Movies.Production", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID");
                });
#pragma warning restore 612, 618
        }
    }
}
