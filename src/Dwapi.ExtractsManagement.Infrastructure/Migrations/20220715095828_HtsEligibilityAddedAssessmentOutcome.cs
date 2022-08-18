using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsEligibilityAddedAssessmentOutcome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssessmentOutcome",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForcedSex",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceivedServices",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeGBV",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssessmentOutcome",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForcedSex",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceivedServices",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeGBV",
                table: "HtsEligibilityExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssessmentOutcome",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ForcedSex",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ReceivedServices",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "TypeGBV",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "AssessmentOutcome",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ForcedSex",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ReceivedServices",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "TypeGBV",
                table: "HtsEligibilityExtracts");
        }
    }
}
