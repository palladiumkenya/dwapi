using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsClientsPkv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pkv",
                table: "TempHtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pkv",
                table: "TempHtsClientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pkv",
                table: "HtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pkv",
                table: "HtsClientExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pkv",
                table: "TempHtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "Pkv",
                table: "TempHtsClientExtracts");

            migrationBuilder.DropColumn(
                name: "Pkv",
                table: "HtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "Pkv",
                table: "HtsClientExtracts");
        }
    }
}
