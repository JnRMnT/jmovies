using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JMovies.App.Migrations
{
    public partial class ResultConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagLine_Production_ProductionID",
                table: "TagLine");

            migrationBuilder.AlterColumn<long>(
                name: "ProductionID",
                table: "TagLine",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ResultConfiguration",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ErrorCode = table.Column<string>(maxLength: 255, nullable: false),
                    RedirectionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultConfiguration", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ResultMessage",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ResultConfigurationID = table.Column<long>(nullable: false),
                    Culture = table.Column<string>(maxLength: 8, nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultMessage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResultMessage_ResultConfiguration_ResultConfigurationID",
                        column: x => x.ResultConfigurationID,
                        principalTable: "ResultConfiguration",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultMessage_ResultConfigurationID",
                table: "ResultMessage",
                column: "ResultConfigurationID");

            migrationBuilder.AddForeignKey(
                name: "FK_TagLine_Production_ProductionID",
                table: "TagLine",
                column: "ProductionID",
                principalTable: "Production",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagLine_Production_ProductionID",
                table: "TagLine");

            migrationBuilder.DropTable(
                name: "ResultMessage");

            migrationBuilder.DropTable(
                name: "ResultConfiguration");

            migrationBuilder.AlterColumn<long>(
                name: "ProductionID",
                table: "TagLine",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_TagLine_Production_ProductionID",
                table: "TagLine",
                column: "ProductionID",
                principalTable: "Production",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
