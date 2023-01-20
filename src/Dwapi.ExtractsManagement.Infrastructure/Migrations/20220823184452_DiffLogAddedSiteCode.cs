using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class DiffLogAddedSiteCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SiteCode",
                table: "DiffLogs",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.Sql(@"DELETE FROM DiffLogs");

        } 

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteCode",
                table: "DiffLogs");
        }
    }
}
