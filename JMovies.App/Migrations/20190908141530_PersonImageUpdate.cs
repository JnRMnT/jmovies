using Microsoft.EntityFrameworkCore.Migrations;

namespace JMovies.App.Migrations
{
    public partial class PersonImageUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryImage",
                table: "Person");

            migrationBuilder.AddColumn<long>(
                name: "PrimaryImageID",
                table: "Person",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_PrimaryImageID",
                table: "Person",
                column: "PrimaryImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Image_PrimaryImageID",
                table: "Person",
                column: "PrimaryImageID",
                principalTable: "Image",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Image_PrimaryImageID",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_PrimaryImageID",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PrimaryImageID",
                table: "Person");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryImage",
                table: "Person",
                maxLength: 255,
                nullable: true);
        }
    }
}
