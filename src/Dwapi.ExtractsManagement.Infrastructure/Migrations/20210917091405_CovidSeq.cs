using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CovidSeq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "COVID19TestResult",
                table: "TempCovidExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sequence",
                table: "TempCovidExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COVID19TestResult",
                table: "CovidExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sequence",
                table: "CovidExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "COVID19TestResult",
                table: "TempCovidExtracts");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "TempCovidExtracts");

            migrationBuilder.DropColumn(
                name: "COVID19TestResult",
                table: "CovidExtracts");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "CovidExtracts");
        }
    }
}
