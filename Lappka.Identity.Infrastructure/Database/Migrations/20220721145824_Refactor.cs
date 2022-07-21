using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lappka.Identity.Infrastructure.Database.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserExtended_AspNetUsers_Id",
                table: "UserExtended");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExtended",
                table: "UserExtended");

            migrationBuilder.RenameTable(
                name: "UserExtended",
                newName: "UsersExtended");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersExtended",
                table: "UsersExtended",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersExtended_AspNetUsers_Id",
                table: "UsersExtended",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersExtended_AspNetUsers_Id",
                table: "UsersExtended");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersExtended",
                table: "UsersExtended");

            migrationBuilder.RenameTable(
                name: "UsersExtended",
                newName: "UserExtended");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExtended",
                table: "UserExtended",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserExtended_AspNetUsers_Id",
                table: "UserExtended",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
