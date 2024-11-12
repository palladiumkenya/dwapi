using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ChildDefiledAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChildDefiled",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientHasChronicIllness",
                table: "TempAllergiesChronicIllnessExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChildDefiled",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientHasChronicIllness",
                table: "AllergiesChronicIllnessExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildDefiled",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "PatientHasChronicIllness",
                table: "TempAllergiesChronicIllnessExtracts");

            migrationBuilder.DropColumn(
                name: "ChildDefiled",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "PatientHasChronicIllness",
                table: "AllergiesChronicIllnessExtracts");
        }
    }
}
