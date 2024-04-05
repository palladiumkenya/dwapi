using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class UpdatedCancerScreening : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BiopsyCINIIandAbove",
                table: "TempCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "BiopsyCINIIandBelow",
                table: "TempCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "BiopsyNotAvailable",
                table: "TempCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "FollowUpDate",
                table: "TempCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "ReferredOut",
                table: "TempCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "ScreeningMethod",
                table: "TempCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "TreatmentToday",
                table: "TempCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "VIAScreeningResult",
                table: "TempCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "VIATreatmentOptions",
                table: "TempCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "BiopsyCINIIandAbove",
                table: "CancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "BiopsyCINIIandBelow",
                table: "CancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "BiopsyNotAvailable",
                table: "CancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "FollowUpDate",
                table: "CancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "ReferredOut",
                table: "CancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "ScreeningMethod",
                table: "CancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "TreatmentToday",
                table: "CancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "VIAScreeningResult",
                table: "CancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "VIATreatmentOptions",
                table: "CancerScreeningExtracts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BiopsyCINIIandAbove",
                table: "TempCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BiopsyCINIIandBelow",
                table: "TempCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BiopsyNotAvailable",
                table: "TempCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FollowUpDate",
                table: "TempCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferredOut",
                table: "TempCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScreeningMethod",
                table: "TempCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TreatmentToday",
                table: "TempCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VIAScreeningResult",
                table: "TempCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VIATreatmentOptions",
                table: "TempCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BiopsyCINIIandAbove",
                table: "CancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BiopsyCINIIandBelow",
                table: "CancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BiopsyNotAvailable",
                table: "CancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FollowUpDate",
                table: "CancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferredOut",
                table: "CancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScreeningMethod",
                table: "CancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TreatmentToday",
                table: "CancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VIAScreeningResult",
                table: "CancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VIATreatmentOptions",
                table: "CancerScreeningExtracts",
                nullable: true);
        }
    }
}
