﻿// <auto-generated />
using System;
using JMovies.DataAccess;
using JMovies.IMDb.Entities.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JMovies.App.Migrations
{
    [DbContext(typeof(JMoviesEntities))]
    [Migration("20191105204930_NullableImages")]
    partial class NullableImages
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("JMovies.DataAccess.Entities.Persisters.PersisterHistory", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("DataID")
                        .HasColumnType("bigint");

                    b.Property<int>("DataSourceID")
                        .HasColumnType("integer");

                    b.Property<int>("EntityTypeID")
                        .HasColumnType("integer")
                        .HasMaxLength(4);

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExecuteDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<short>("IsSuccess")
                        .HasColumnType("smallint");

                    b.HasKey("ID");

                    b.HasIndex("DataSourceID");

                    b.ToTable("PersisterHistory");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Resource", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.HasKey("ID");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.ResourceTranslation", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Culture")
                        .IsRequired()
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.Property<long>("ResourceID")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("ResourceID");

                    b.ToTable("ResourceTranslation");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Common.Image", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("Content")
                        .HasColumnType("BYTEA");

                    b.Property<long?>("PersonID")
                        .HasColumnName("PersonID")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProductionID")
                        .HasColumnName("ProductionID")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("URL")
                        .HasColumnName("SourceURL")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Misc.DataSource", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Identifier")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(32)")
                        .HasMaxLength(32);

                    b.HasKey("ID");

                    b.ToTable("DataSource");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.AKA", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<long>("ProductionID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("AKA");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Character", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CharacterType")
                        .HasColumnType("integer")
                        .HasMaxLength(2);

                    b.Property<long>("CreditID")
                        .HasColumnType("bigint");

                    b.Property<long?>("IMDbID")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.HasKey("ID");

                    b.HasIndex("CreditID");

                    b.ToTable("Character");

                    b.HasDiscriminator<int>("CharacterType").HasValue(0);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Company", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<long>("ProductionID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Country", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Identifier")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.HasKey("ID");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Credit", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("PersonID")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductionID")
                        .HasColumnType("bigint");

                    b.Property<int>("RoleType")
                        .HasColumnType("integer")
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Identifier")
                        .HasColumnType("character varying(36)")
                        .HasMaxLength(36);

                    b.Property<long>("ProductionID")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnName("Name")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Keyword", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Identifier")
                        .HasColumnType("character varying(36)")
                        .HasMaxLength(36);

                    b.Property<long>("ProductionID")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnName("Name")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("Keyword");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Language", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Identifier")
                        .HasColumnType("character varying(36)")
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.HasKey("ID");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Production", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("IMDbID")
                        .HasColumnType("bigint");

                    b.Property<long?>("PosterID")
                        .HasColumnType("bigint");

                    b.Property<int>("ProductionType")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasMaxLength(4);

                    b.HasKey("ID");

                    b.HasIndex("PosterID");

                    b.ToTable("Production");

                    b.HasDiscriminator<int>("ProductionType").HasValue(0);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ProductionCountry", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CountryID")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductionID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.HasIndex("ProductionID");

                    b.ToTable("ProductionCountry");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ProductionLanguage", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("LanguageID")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductionID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("LanguageID");

                    b.HasIndex("ProductionID");

                    b.ToTable("ProductionLanguage");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Rating", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DataSourceID")
                        .HasColumnType("integer");

                    b.Property<long>("ProductionID")
                        .HasColumnType("bigint");

                    b.Property<long>("RateCount")
                        .HasColumnType("bigint");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("ID");

                    b.HasIndex("DataSourceID");

                    b.HasIndex("ProductionID")
                        .IsUnique();

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ReleaseDate", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CountryID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.Property<long>("ProductionID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.HasIndex("ProductionID");

                    b.ToTable("ReleaseDate");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.TagLine", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("character varying(512)")
                        .HasMaxLength(512);

                    b.Property<long?>("ProductionID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("ProductionID");

                    b.ToTable("TagLine");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.People.Person", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("BirthName")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("BirthPlace")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<int>("Gender")
                        .HasColumnType("integer")
                        .HasMaxLength(2);

                    b.Property<int?>("Height")
                        .HasColumnType("integer")
                        .HasMaxLength(4);

                    b.Property<long>("IMDbID")
                        .HasColumnType("bigint");

                    b.Property<string>("MiniBiography")
                        .HasColumnType("text");

                    b.Property<string>("NickName")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<long?>("PrimaryImageID")
                        .HasColumnType("bigint");

                    b.Property<string>("Roles")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.HasKey("ID");

                    b.HasIndex("PrimaryImageID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.People.ProductionCredit", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("CreditID")
                        .HasColumnType("bigint");

                    b.Property<long?>("PersonID")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProductionID")
                        .HasColumnType("bigint");

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
                        .HasColumnType("integer")
                        .HasMaxLength(4);

                    b.Property<int>("EpisodeCount")
                        .HasColumnType("integer")
                        .HasMaxLength(4);

                    b.Property<int?>("StartYear")
                        .HasColumnType("integer")
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
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("FilmingLocations")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("OfficialSites")
                        .HasColumnType("character varying(512)")
                        .HasMaxLength(512);

                    b.Property<string>("OriginalTitle")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("PlotSummary")
                        .HasColumnType("character varying(512)")
                        .HasMaxLength(512);

                    b.Property<TimeSpan>("Runtime")
                        .HasColumnType("interval");

                    b.Property<string>("StoryLine")
                        .HasColumnType("text");

                    b.ToTable("Movie");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.TVSeries", b =>
                {
                    b.HasBaseType("JMovies.IMDb.Entities.Movies.Movie");

                    b.Property<int?>("EndYear")
                        .HasColumnType("integer")
                        .HasMaxLength(4);

                    b.ToTable("TVSeries");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.Persisters.PersisterHistory", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Misc.DataSource", "DataSource")
                        .WithMany()
                        .HasForeignKey("DataSourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMovies.DataAccess.Entities.ResourceTranslation", b =>
                {
                    b.HasOne("JMovies.DataAccess.Entities.Resource", null)
                        .WithMany("Translations")
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Common.Image", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.People.Person", null)
                        .WithMany("Photos")
                        .HasForeignKey("PersonID");

                    b.HasOne("JMovies.IMDb.Entities.Movies.Production", null)
                        .WithMany("MediaImages")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.AKA", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie", null)
                        .WithMany("AKAs")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Character", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.ActingCredit", null)
                        .WithMany("Characters")
                        .HasForeignKey("CreditID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Company", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie", null)
                        .WithMany("ProductionCompanies")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Credit", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.People.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie", null)
                        .WithMany("Credits")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Genre", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie", null)
                        .WithMany("Genres")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Keyword", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie", null)
                        .WithMany("Keywords")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie", null)
                        .WithMany("Countries")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JMovies.IMDb.Entities.Movies.Production", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID")
                        .HasConstraintName("FK_ProductionCountry_Production_ProductionID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ProductionLanguage", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie", null)
                        .WithMany("Languages")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JMovies.IMDb.Entities.Movies.Production", "Production")
                        .WithMany()
                        .HasForeignKey("ProductionID")
                        .HasConstraintName("FK_ProductionLanguage_Production_ProductionID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.Rating", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Misc.DataSource", "DataSource")
                        .WithMany()
                        .HasForeignKey("DataSourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JMovies.IMDb.Entities.Movies.Production", "Production")
                        .WithOne("Rating")
                        .HasForeignKey("JMovies.IMDb.Entities.Movies.Rating", "ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.ReleaseDate", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie", null)
                        .WithMany("ReleaseDates")
                        .HasForeignKey("ProductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.Movies.TagLine", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Movie", null)
                        .WithMany("TagLines")
                        .HasForeignKey("ProductionID");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.People.Person", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Common.Image", "PrimaryImage")
                        .WithMany()
                        .HasForeignKey("PrimaryImageID");
                });

            modelBuilder.Entity("JMovies.IMDb.Entities.People.ProductionCredit", b =>
                {
                    b.HasOne("JMovies.IMDb.Entities.Movies.Credit", "Credit")
                        .WithMany()
                        .HasForeignKey("CreditID");

                    b.HasOne("JMovies.IMDb.Entities.People.Person", null)
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
