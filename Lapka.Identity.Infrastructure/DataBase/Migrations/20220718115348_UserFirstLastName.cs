using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapka.Identity.Infrastructure.DataBase.Migrations
{
    public partial class UserFirstLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UserExtended",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "UserExtended",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UserExtended");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "UserExtended");
        }
    }
}
