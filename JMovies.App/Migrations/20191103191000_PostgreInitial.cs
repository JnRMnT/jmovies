﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JMovies.App.Migrations
{
    public partial class PostgreInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Identifier = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DataSource",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Identifier = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSource", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Identifier = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PersisterHistory",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataSourceID = table.Column<int>(nullable: false),
                    EntityTypeID = table.Column<int>(maxLength: 4, nullable: false),
                    DataID = table.Column<long>(nullable: false),
                    ExecuteDate = table.Column<DateTime>(nullable: false),
                    IsSuccess = table.Column<short>(nullable: false),
                    ErrorMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersisterHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersisterHistory_DataSource_DataSourceID",
                        column: x => x.DataSourceID,
                        principalTable: "DataSource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceTranslation",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ResourceID = table.Column<long>(nullable: false),
                    Culture = table.Column<string>(maxLength: 8, nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceTranslation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResourceTranslation_Resource_ResourceID",
                        column: x => x.ResourceID,
                        principalTable: "Resource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionCountry",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductionID = table.Column<long>(nullable: false),
                    CountryID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCountry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductionCountry_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseDate",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    CountryID = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 64, nullable: true),
                    ProductionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseDate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReleaseDate_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterType = table.Column<int>(maxLength: 2, nullable: false),
                    IMDbID = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    CreditID = table.Column<long>(nullable: false),
                    EpisodeCount = table.Column<int>(maxLength: 4, nullable: true),
                    StartYear = table.Column<int>(maxLength: 4, nullable: true),
                    EndYear = table.Column<int>(maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductionCredit",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreditID = table.Column<long>(nullable: true),
                    ProductionID = table.Column<long>(nullable: true),
                    PersonID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCredit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<double>(nullable: false),
                    RateCount = table.Column<long>(nullable: false),
                    DataSourceID = table.Column<int>(nullable: false),
                    ProductionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rating_DataSource_DataSourceID",
                        column: x => x.DataSourceID,
                        principalTable: "DataSource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IMDbID = table.Column<long>(nullable: false),
                    FullName = table.Column<string>(maxLength: 128, nullable: false),
                    PrimaryImageID = table.Column<long>(nullable: true),
                    Roles = table.Column<string>(maxLength: 128, nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    BirthPlace = table.Column<string>(maxLength: 128, nullable: true),
                    BirthName = table.Column<string>(maxLength: 128, nullable: true),
                    Height = table.Column<int>(maxLength: 4, nullable: true),
                    NickName = table.Column<string>(maxLength: 128, nullable: true),
                    MiniBiography = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Production",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IMDbID = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Year = table.Column<int>(maxLength: 4, nullable: false),
                    PosterID = table.Column<long>(nullable: true),
                    ProductionType = table.Column<int>(nullable: false),
                    OriginalTitle = table.Column<string>(maxLength: 128, nullable: true),
                    PlotSummary = table.Column<string>(maxLength: 512, nullable: true),
                    StoryLine = table.Column<string>(nullable: true),
                    OfficialSites = table.Column<string>(maxLength: 512, nullable: true),
                    FilmingLocations = table.Column<string>(maxLength: 256, nullable: true),
                    Budget = table.Column<string>(maxLength: 256, nullable: true),
                    Runtime = table.Column<TimeSpan>(nullable: true),
                    EndYear = table.Column<int>(maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AKA",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(maxLength: 64, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    ProductionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AKA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AKA_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    ProductionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Company_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credit",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonID = table.Column<long>(nullable: false),
                    RoleType = table.Column<int>(maxLength: 2, nullable: false),
                    ProductionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Credit_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Credit_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Identifier = table.Column<string>(maxLength: 36, nullable: true),
                    ProductionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Genre_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    SourceURL = table.Column<string>(maxLength: 255, nullable: true),
                    Content = table.Column<byte[]>(type: "BYTEA", nullable: true),
                    PersonID = table.Column<long>(nullable: false),
                    ProductionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Image_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Image_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Keyword",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Identifier = table.Column<string>(maxLength: 36, nullable: true),
                    ProductionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Keyword_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionLanguage",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductionID = table.Column<long>(nullable: false),
                    LanguageID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLanguage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductionLanguage_Language_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Language",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionLanguage_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionLanguage_Production_ProductionID1",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagLine",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(maxLength: 512, nullable: false),
                    ProductionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagLine", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TagLine_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AKA_ProductionID",
                table: "AKA",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Character_CreditID",
                table: "Character",
                column: "CreditID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ProductionID",
                table: "Company",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Credit_PersonID",
                table: "Credit",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Credit_ProductionID",
                table: "Credit",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_ProductionID",
                table: "Genre",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Image_PersonID",
                table: "Image",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductionID",
                table: "Image",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_ProductionID",
                table: "Keyword",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_PersisterHistory_DataSourceID",
                table: "PersisterHistory",
                column: "DataSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PrimaryImageID",
                table: "Person",
                column: "PrimaryImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Production_PosterID",
                table: "Production",
                column: "PosterID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionCountry_CountryID",
                table: "ProductionCountry",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionCountry_ProductionID",
                table: "ProductionCountry",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionCredit_CreditID",
                table: "ProductionCredit",
                column: "CreditID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionCredit_PersonID",
                table: "ProductionCredit",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionCredit_ProductionID",
                table: "ProductionCredit",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLanguage_LanguageID",
                table: "ProductionLanguage",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLanguage_ProductionID",
                table: "ProductionLanguage",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_DataSourceID",
                table: "Rating",
                column: "DataSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ProductionID",
                table: "Rating",
                column: "ProductionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseDate_CountryID",
                table: "ReleaseDate",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseDate_ProductionID",
                table: "ReleaseDate",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceTranslation_ResourceID",
                table: "ResourceTranslation",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_TagLine_ProductionID",
                table: "TagLine",
                column: "ProductionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionCountry_Production_ProductionID",
                table: "ProductionCountry",
                column: "ProductionID",
                principalTable: "Production",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionCountry_Production_ProductionID1",
                table: "ProductionCountry",
                column: "ProductionID",
                principalTable: "Production",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseDate_Production_ProductionID",
                table: "ReleaseDate",
                column: "ProductionID",
                principalTable: "Production",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Credit_CreditID",
                table: "Character",
                column: "CreditID",
                principalTable: "Credit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionCredit_Production_ProductionID",
                table: "ProductionCredit",
                column: "ProductionID",
                principalTable: "Production",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionCredit_Credit_CreditID",
                table: "ProductionCredit",
                column: "CreditID",
                principalTable: "Credit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionCredit_Person_PersonID",
                table: "ProductionCredit",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Production_ProductionID",
                table: "Rating",
                column: "ProductionID",
                principalTable: "Production",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Image_PrimaryImageID",
                table: "Person",
                column: "PrimaryImageID",
                principalTable: "Image",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Production_Image_PosterID",
                table: "Production",
                column: "PosterID",
                principalTable: "Image",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Production_ProductionID",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Person_PersonID",
                table: "Image");

            migrationBuilder.DropTable(
                name: "AKA");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Keyword");

            migrationBuilder.DropTable(
                name: "PersisterHistory");

            migrationBuilder.DropTable(
                name: "ProductionCountry");

            migrationBuilder.DropTable(
                name: "ProductionCredit");

            migrationBuilder.DropTable(
                name: "ProductionLanguage");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "ReleaseDate");

            migrationBuilder.DropTable(
                name: "ResourceTranslation");

            migrationBuilder.DropTable(
                name: "TagLine");

            migrationBuilder.DropTable(
                name: "Credit");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "DataSource");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "Production");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
