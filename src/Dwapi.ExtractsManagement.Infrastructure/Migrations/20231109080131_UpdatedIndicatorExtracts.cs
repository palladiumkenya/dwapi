using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class UpdatedIndicatorExtracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SiteCode",
                table: "TempIndicatorExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SiteCode",
                table: "IndicatorExtracts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteCode",
                table: "TempIndicatorExtracts");

            migrationBuilder.DropColumn(
                name: "SiteCode",
                table: "IndicatorExtracts");
        }
    }
}
