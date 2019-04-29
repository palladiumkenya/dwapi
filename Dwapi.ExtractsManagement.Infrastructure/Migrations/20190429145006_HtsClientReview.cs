using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsClientReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisabilityType",
                table: "TempHtsClientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyPopulationType",
                table: "TempHtsClientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientConsented",
                table: "TempHtsClientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientDisabled",
                table: "TempHtsClientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PopulationType",
                table: "TempHtsClientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisabilityType",
                table: "HtsClientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyPopulationType",
                table: "HtsClientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientConsented",
                table: "HtsClientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientDisabled",
                table: "HtsClientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PopulationType",
                table: "HtsClientExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisabilityType",
                table: "TempHtsClientExtracts");

            migrationBuilder.DropColumn(
                name: "KeyPopulationType",
                table: "TempHtsClientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientConsented",
                table: "TempHtsClientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientDisabled",
                table: "TempHtsClientExtracts");

            migrationBuilder.DropColumn(
                name: "PopulationType",
                table: "TempHtsClientExtracts");

            migrationBuilder.DropColumn(
                name: "DisabilityType",
                table: "HtsClientExtracts");

            migrationBuilder.DropColumn(
                name: "KeyPopulationType",
                table: "HtsClientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientConsented",
                table: "HtsClientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientDisabled",
                table: "HtsClientExtracts");

            migrationBuilder.DropColumn(
                name: "PopulationType",
                table: "HtsClientExtracts");
        }
    }
}
