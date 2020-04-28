using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class RestAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "RestProtocols",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "RestProtocols",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "RestProtocols");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "RestProtocols");
        }
    }
}
