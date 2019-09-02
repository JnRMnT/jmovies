using Microsoft.EntityFrameworkCore.Migrations;

namespace JMovies.App.Migrations
{
    public partial class PhotoSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photos",
                table: "Person");

            migrationBuilder.AddColumn<long>(
                name: "PersonID",
                table: "Image",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_PersonID",
                table: "Image",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Person_PersonID",
                table: "Image",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Person_PersonID",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_PersonID",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Photos",
                table: "Person",
                nullable: true);
        }
    }
}
