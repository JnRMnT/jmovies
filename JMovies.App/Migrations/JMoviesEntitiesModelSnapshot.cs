﻿// <auto-generated />
using System;
using JMovies.DataAccess;
using JMovies.DataAccess.Entities.Movies;
using JMovies.DataAccess.Entities.People;
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

            modelBuilder.Entity("JMovies.DataAccess.Entities.Common.jm_DataSource", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(32);

                    b.HasKey("ID");

                    b.ToTable("DataSource");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_AKA", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(64);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<long?>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("AKA");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Character", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CharacterType")
                        .HasMaxLength(2);

                    b.Property<long?>("IMDbID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("ID");

                    b.ToTable("Character");

                    b.HasDiscriminator<int>("CharacterType").HasValue(0);
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Company", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<long?>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Country", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Identifier")
                        .HasMaxLength(64);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<long?>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Credit", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("PersonID");

                    b.Property<long?>("ProductionID");

                    b.Property<int>("RoleType")
                        .HasMaxLength(2);

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Credit");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Genre", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Identifier")
                        .HasMaxLength(36);

                    b.Property<long?>("ProductionID");

                    b.Property<string>("Value")
                        .HasColumnName("Name")
                        .HasMaxLength(64);

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Keyword", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Identifier")
                        .HasMaxLength(36);

                    b.Property<long?>("ProductionID");

                    b.Property<string>("Value")
                        .HasColumnName("Name")
                        .HasMaxLength(64);

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Keyword");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Language", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Identifier")
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .HasMaxLength(64);

                    b.Property<long?>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Production", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("IMDbID");

                    b.Property<long>("ProductionID");

                    b.Property<int>("ProductionType")
                        .HasMaxLength(4);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("Year")
                        .HasMaxLength(4);

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Production");

                    b.HasDiscriminator<int>("ProductionType").HasValue(0);
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Rating", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DataSourceID");

                    b.Property<long>("RateCount");

                    b.Property<double>("Value");

                    b.HasKey("ID");

                    b.HasIndex("DataSourceID");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_ReleaseDate", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CountryID");

                    b.Property<DateTime>("Date");

                    b.Property<long?>("ProductionID");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.HasIndex("ProductionID");

                    b.ToTable("ReleaseDate");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.People.jm_Person", b =>
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

                    b.Property<int>("PersonType")
                        .HasMaxLength(4);

                    b.Property<string>("Photos");

                    b.Property<string>("PrimaryImage")
                        .HasMaxLength(255);

                    b.Property<string>("Roles")
                        .HasMaxLength(128);

                    b.HasKey("ID");

                    b.ToTable("Person");

                    b.HasDiscriminator<int>("PersonType").HasValue(0);
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.People.jm_ProductionCredit", b =>
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

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_TVCharacter", b =>
                {
                    b.HasBaseType("JMovies.DataAccess.Entities.Movies.jm_Character");

                    b.Property<int?>("EndYear")
                        .HasMaxLength(4);

                    b.Property<int>("EpisodeCount")
                        .HasMaxLength(4);

                    b.Property<int?>("StartYear")
                        .HasMaxLength(4);

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Movie", b =>
                {
                    b.HasBaseType("JMovies.DataAccess.Entities.Movies.jm_Production");

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

                    b.Property<string>("TagLines")
                        .HasMaxLength(128);

                    b.ToTable("Movie");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.People.jm_Actor", b =>
                {
                    b.HasBaseType("JMovies.DataAccess.Entities.People.jm_Person");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_TVSeries", b =>
                {
                    b.HasBaseType("JMovies.DataAccess.Entities.Movies.jm_Movie");

                    b.Property<int?>("EndYear")
                        .HasMaxLength(4);

                    b.ToTable("TVSeries");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_AKA", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Movie")
                        .WithMany("AKAs")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Company", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Movie")
                        .WithMany("ProductionCompanies")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Country", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Movie")
                        .WithMany("Countries")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Credit", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.People.jm_Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID");

                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Movie")
                        .WithMany("Credits")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Genre", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Movie")
                        .WithMany("Genres")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Keyword", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Movie")
                        .WithMany("Keywords")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Language", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Movie")
                        .WithMany("Languages")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Production", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Rating", "Rating")
                        .WithMany()
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_Rating", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Common.jm_DataSource", "DataSource")
                        .WithMany()
                        .HasForeignKey("DataSourceID");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Movies.jm_ReleaseDate", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID");

                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Movie")
                        .WithMany("ReleaseDates")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.People.jm_ProductionCredit", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Credit", "Credit")
                        .WithMany()
                        .HasForeignKey("CreditID");

                    b.HasOne("JMovies.DataAccess.Entities.People.jm_Person")
                        .WithMany("KnownFor")
                        .HasForeignKey("PersonID");

                    b.HasOne("JMovies.DataAccess.Entities.Movies.jm_Production", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID");
                });
#pragma warning restore 612, 618
        }
    }
}
