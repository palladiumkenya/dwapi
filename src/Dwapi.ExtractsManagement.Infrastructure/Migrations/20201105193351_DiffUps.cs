using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class DiffUps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempPatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempPatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempPatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempPatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempPatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempPatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempPatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempPatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempPatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempPatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempPatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempPatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "PatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "PatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "PatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "PatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "PatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "PatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "PatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "PatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiffLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Docket = table.Column<string>(nullable: true),
                    Extract = table.Column<string>(nullable: true),
                    LastCreated = table.Column<DateTime>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    MaxCreated = table.Column<DateTime>(nullable: true),
                    MaxModified = table.Column<DateTime>(nullable: true),
                    LastSent = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiffLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiffLogs");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempPatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempPatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempPatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempPatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempPatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempPatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempPatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempPatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "PatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "PatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "PatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "PatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "PatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "PatientAdverseEventExtracts");
        }
    }
}
