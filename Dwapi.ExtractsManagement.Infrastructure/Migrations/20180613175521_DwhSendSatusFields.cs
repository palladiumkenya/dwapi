using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class DwhSendSatusFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Processed",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QueueId",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusDate",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Processed",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QueueId",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusDate",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Processed",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QueueId",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusDate",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Processed",
                table: "PatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QueueId",
                table: "PatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusDate",
                table: "PatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Processed",
                table: "PatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QueueId",
                table: "PatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusDate",
                table: "PatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Processed",
                table: "PatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QueueId",
                table: "PatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusDate",
                table: "PatientArtExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Processed",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "QueueId",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "StatusDate",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Processed",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "QueueId",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "StatusDate",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "Processed",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "QueueId",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "StatusDate",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "Processed",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "QueueId",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "StatusDate",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "Processed",
                table: "PatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "QueueId",
                table: "PatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "StatusDate",
                table: "PatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "Processed",
                table: "PatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "QueueId",
                table: "PatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "StatusDate",
                table: "PatientArtExtracts");
        }
    }
}
