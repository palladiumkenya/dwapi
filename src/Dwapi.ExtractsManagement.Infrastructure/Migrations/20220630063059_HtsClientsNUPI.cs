using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsClientsNUPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NUPI",
                table: "TempHtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NUPI",
                table: "HtsClientsExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.DropColumn(
                name: "NUPI",
                table: "TempHtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "NUPI",
                table: "HtsClientsExtracts");
        }
    }
}
