using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class NewCTQReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abdomen",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNS",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CVS",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chest",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ENT",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Eyes",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeneralExamination",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genitourinary",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Skin",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemExamination",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastUsed",
                table: "TempPatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousARTPurpose",
                table: "TempPatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousARTUse",
                table: "TempPatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abdomen",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNS",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CVS",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chest",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ENT",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Eyes",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeneralExamination",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genitourinary",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Skin",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemExamination",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastUsed",
                table: "PatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousARTPurpose",
                table: "PatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousARTUse",
                table: "PatientArtExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abdomen",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "CNS",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "CVS",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Chest",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ENT",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Eyes",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "GeneralExamination",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Genitourinary",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Skin",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "SystemExamination",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "DateLastUsed",
                table: "TempPatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "PreviousARTPurpose",
                table: "TempPatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "PreviousARTUse",
                table: "TempPatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "Abdomen",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "CNS",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "CVS",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Chest",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ENT",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Eyes",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "GeneralExamination",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Genitourinary",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Skin",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "SystemExamination",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "DateLastUsed",
                table: "PatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "PreviousARTPurpose",
                table: "PatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "PreviousARTUse",
                table: "PatientArtExtracts");
        }
    }
}
