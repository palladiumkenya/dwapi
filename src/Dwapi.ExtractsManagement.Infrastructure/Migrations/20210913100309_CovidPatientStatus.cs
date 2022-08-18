using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CovidPatientStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeathDate",
                table: "TempPatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonForDeath",
                table: "TempPatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecificDeathReason",
                table: "TempPatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeathDate",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonForDeath",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecificDeathReason",
                table: "PatientStatusExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "ReasonForDeath",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "SpecificDeathReason",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "ReasonForDeath",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "SpecificDeathReason",
                table: "PatientStatusExtracts");
        }
    }
}
