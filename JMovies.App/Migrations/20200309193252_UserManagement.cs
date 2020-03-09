using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JMovies.App.Migrations
{
    public partial class UserManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Password",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Salt = table.Column<string>(maxLength: 128, nullable: false),
                    Hash = table.Column<string>(maxLength: 256, nullable: false),
                    HashType = table.Column<int>(nullable: false),
                    RetryCount = table.Column<int>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Password", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(maxLength: 55, nullable: false),
                    PasswordID = table.Column<long>(nullable: true),
                    Email = table.Column<string>(maxLength: 128, nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Password_PasswordID",
                        column: x => x.PasswordID,
                        principalTable: "Password",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_PasswordID",
                table: "User",
                column: "PasswordID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Password");
        }
    }
}
