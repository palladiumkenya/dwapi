using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CT_Additional_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DifferentiatedCare",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyPopulationType",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PopulationType",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StabilityAssessment",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inschool",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyPopulationType",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Orphan",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentCounty",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentLocation",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentSubCounty",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentSubLocation",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentVillage",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentWard",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientType",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PopulationType",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DifferentiatedCare",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyPopulationType",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PopulationType",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StabilityAssessment",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inschool",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyPopulationType",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Orphan",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentCounty",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentLocation",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentSubCounty",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentSubLocation",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentVillage",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientResidentWard",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientType",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PopulationType",
                table: "PatientExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifferentiatedCare",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "KeyPopulationType",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PopulationType",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "StabilityAssessment",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Inschool",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "KeyPopulationType",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "Orphan",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentCounty",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentLocation",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentSubCounty",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentSubLocation",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentVillage",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentWard",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientType",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "PopulationType",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "DifferentiatedCare",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "KeyPopulationType",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PopulationType",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "StabilityAssessment",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Inschool",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "KeyPopulationType",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "Orphan",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentCounty",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentLocation",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentSubCounty",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentSubLocation",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentVillage",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientResidentWard",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientType",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "PopulationType",
                table: "PatientExtracts");
            
        }
    }
}
