using Microsoft.EntityFrameworkCore.Migrations;

namespace JMovies.App.Migrations
{
    public partial class ImageUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Person_PersonID",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Production_ProductionID",
                table: "Image");

            migrationBuilder.AlterColumn<long>(
                name: "ProductionID",
                table: "Image",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PersonID",
                table: "Image",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Person_PersonID",
                table: "Image",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Production_ProductionID",
                table: "Image",
                column: "ProductionID",
                principalTable: "Production",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Person_PersonID",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Production_ProductionID",
                table: "Image");

            migrationBuilder.AlterColumn<long>(
                name: "ProductionID",
                table: "Image",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "PersonID",
                table: "Image",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Person_PersonID",
                table: "Image",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Production_ProductionID",
                table: "Image",
                column: "ProductionID",
                principalTable: "Production",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
