using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HTSRecencyIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "HtsRecencyId",
                table: "TempHtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HtsRecencyId",
                table: "HtsClientsExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "HtsRecencyId",
                table: "TempHtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "HtsRecencyId",
                table: "HtsClientsExtracts");
        }
    }
}
