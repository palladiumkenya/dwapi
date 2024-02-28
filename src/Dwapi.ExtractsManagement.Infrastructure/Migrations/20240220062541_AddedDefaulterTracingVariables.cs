using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddedDefaulterTracingVariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfMissedAppointment",
                table: "TempDefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePromisedToCome",
                table: "TempDefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonForMissedAppointment",
                table: "TempDefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Controlled",
                table: "TempAllergiesChronicIllnessExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfMissedAppointment",
                table: "DefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePromisedToCome",
                table: "DefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonForMissedAppointment",
                table: "DefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Controlled",
                table: "AllergiesChronicIllnessExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfMissedAppointment",
                table: "TempDefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "DatePromisedToCome",
                table: "TempDefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "ReasonForMissedAppointment",
                table: "TempDefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Controlled",
                table: "TempAllergiesChronicIllnessExtracts");

            migrationBuilder.DropColumn(
                name: "DateOfMissedAppointment",
                table: "DefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "DatePromisedToCome",
                table: "DefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "ReasonForMissedAppointment",
                table: "DefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Controlled",
                table: "AllergiesChronicIllnessExtracts");
        }
    }
}
