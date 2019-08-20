using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JMovies.App.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    IMDbID = table.Column<long>(nullable: true),
                    CharacterType = table.Column<int>(maxLength: 2, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    EpisodeCount = table.Column<int>(maxLength: 4, nullable: true),
                    StartYear = table.Column<int>(maxLength: 4, nullable: true),
                    EndYear = table.Column<int>(maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DataSource",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSource", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    PersonType = table.Column<int>(maxLength: 4, nullable: false),
                    IMDbID = table.Column<long>(nullable: false),
                    FullName = table.Column<string>(maxLength: 128, nullable: false),
                    PrimaryImage = table.Column<string>(maxLength: 255, nullable: true),
                    Roles = table.Column<string>(maxLength: 128, nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    BirthPlace = table.Column<string>(maxLength: 128, nullable: true),
                    BirthName = table.Column<string>(maxLength: 128, nullable: true),
                    Height = table.Column<int>(maxLength: 4, nullable: true),
                    NickName = table.Column<string>(maxLength: 128, nullable: true),
                    MiniBiography = table.Column<string>(nullable: true),
                    Photos = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Value = table.Column<double>(nullable: false),
                    RateCount = table.Column<long>(nullable: false),
                    DataSourceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rating_DataSource_DataSourceID",
                        column: x => x.DataSourceID,
                        principalTable: "DataSource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Production",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    IMDbID = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Year = table.Column<int>(maxLength: 4, nullable: false),
                    ProductionID = table.Column<long>(nullable: false),
                    ProductionType = table.Column<int>(maxLength: 4, nullable: false),
                    OriginalTitle = table.Column<string>(maxLength: 128, nullable: true),
                    PlotSummary = table.Column<string>(maxLength: 512, nullable: true),
                    StoryLine = table.Column<string>(nullable: true),
                    TagLines = table.Column<string>(maxLength: 128, nullable: true),
                    OfficialSites = table.Column<string>(maxLength: 256, nullable: true),
                    FilmingLocations = table.Column<string>(maxLength: 256, nullable: true),
                    Budget = table.Column<string>(maxLength: 128, nullable: true),
                    Runtime = table.Column<TimeSpan>(nullable: true),
                    EndYear = table.Column<int>(maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Production_Rating_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Rating",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AKA",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Description = table.Column<string>(maxLength: 64, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    ProductionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AKA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AKA_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    ProductionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Company_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Identifier = table.Column<string>(maxLength: 64, nullable: true),
                    ProductionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Country_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Credit",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    PersonID = table.Column<long>(nullable: true),
                    RoleType = table.Column<int>(maxLength: 2, nullable: false),
                    ProductionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Credit_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Credit_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Identifier = table.Column<string>(maxLength: 36, nullable: true),
                    ProductionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Genre_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Keyword",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Identifier = table.Column<string>(maxLength: 36, nullable: true),
                    ProductionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Keyword_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Identifier = table.Column<string>(maxLength: 36, nullable: true),
                    ProductionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Language_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseDate",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    CountryID = table.Column<long>(nullable: true),
                    ProductionID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseDate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReleaseDate_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReleaseDate_Production_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Production",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionCredit",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreditID = table.Column<long>(nullable: true),
                    ProductionID = table.Column<long>(nullable: true),
                    PersonID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCredit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductionCredit_Credit_CreditID",
                        column: x => x.CreditID,
                        principalTable: "Credit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionCredit_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionCredit_Production_ProductionID",
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
                name: "IX_Company_ProductionID",
                table: "Company",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Country_ProductionID",
                table: "Country",
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
                name: "IX_Keyword_ProductionID",
                table: "Keyword",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Language_ProductionID",
                table: "Language",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Production_ProductionID",
                table: "Production",
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
                name: "IX_Rating_DataSourceID",
                table: "Rating",
                column: "DataSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseDate_CountryID",
                table: "ReleaseDate",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseDate_ProductionID",
                table: "ReleaseDate",
                column: "ProductionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Language");

            migrationBuilder.DropTable(
                name: "ProductionCredit");

            migrationBuilder.DropTable(
                name: "ReleaseDate");

            migrationBuilder.DropTable(
                name: "Credit");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Production");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "DataSource");
        }
    }
}
