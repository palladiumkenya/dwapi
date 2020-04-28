using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class EmrSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmrSetup",
                table: "EmrSystems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmrSetup",
                table: "EmrSystems");
        }
    }
}
