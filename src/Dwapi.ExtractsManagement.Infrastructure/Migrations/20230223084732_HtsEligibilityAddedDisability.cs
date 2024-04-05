using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsEligibilityAddedDisability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Disability",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisabilityType",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HIVRiskCategory",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HTSEntryPoint",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HTSStrategy",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonNotReffered",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonRefferredForTesting",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Disability",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisabilityType",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HIVRiskCategory",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HTSEntryPoint",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HTSStrategy",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonNotReffered",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonRefferredForTesting",
                table: "HtsEligibilityExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disability",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "DisabilityType",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "HIVRiskCategory",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "HTSEntryPoint",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "HTSStrategy",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ReasonNotReffered",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ReasonRefferredForTesting",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "Disability",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "DisabilityType",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "HIVRiskCategory",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "HTSEntryPoint",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "HTSStrategy",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ReasonNotReffered",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ReasonRefferredForTesting",
                table: "HtsEligibilityExtracts");
        }
    }
}
