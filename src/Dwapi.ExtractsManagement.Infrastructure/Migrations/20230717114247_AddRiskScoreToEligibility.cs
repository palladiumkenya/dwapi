using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddRiskScoreToEligibility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HtsRiskScore",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherReferredServices",
                table: "TempHtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferredForServices",
                table: "TempHtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferredServices",
                table: "TempHtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HtsRiskScore",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherReferredServices",
                table: "HtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferredForServices",
                table: "HtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferredServices",
                table: "HtsClientTestsExtracts",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HtsRiskScore",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "OtherReferredServices",
                table: "TempHtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "ReferredForServices",
                table: "TempHtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "ReferredServices",
                table: "TempHtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "HtsRiskScore",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "OtherReferredServices",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "ReferredForServices",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "ReferredServices",
                table: "HtsClientTestsExtracts");

        }
    }
}
