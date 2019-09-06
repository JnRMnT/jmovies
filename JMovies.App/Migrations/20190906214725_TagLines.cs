using Microsoft.EntityFrameworkCore.Migrations;

namespace JMovies.App.Migrations
{
    public partial class TagLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagLines",
                table: "Production");

            migrationBuilder.CreateTable(
                name: "TagLine",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
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
                name: "IX_TagLine_ProductionID",
                table: "TagLine",
                column: "ProductionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagLine");

            migrationBuilder.AddColumn<string>(
                name: "TagLines",
                table: "Production",
                maxLength: 128,
                nullable: true);
        }
    }
}
