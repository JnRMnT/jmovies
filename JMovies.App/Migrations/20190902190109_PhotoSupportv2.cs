using Microsoft.EntityFrameworkCore.Migrations;

namespace JMovies.App.Migrations
{
    public partial class PhotoSupportv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductionID",
                table: "Image",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductionID",
                table: "Image",
                column: "ProductionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Production_ProductionID",
                table: "Image",
                column: "ProductionID",
                principalTable: "Production",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Production_ProductionID",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_ProductionID",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ProductionID",
                table: "Image");
        }
    }
}
