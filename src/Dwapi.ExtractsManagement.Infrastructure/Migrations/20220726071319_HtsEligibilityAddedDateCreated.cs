using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsEligibilityAddedDateCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentlyHasTB",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "EmotionalViolence",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "CurrentlyHasTB",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "EmotionalViolence",
                table: "HtsEligibilityExtracts");

            migrationBuilder.RenameColumn(
                name: "PartnerHivStatus",
                table: "TempHtsEligibilityExtracts",
                newName: "PartnerHIVStatus");

            migrationBuilder.RenameColumn(
                name: "SexualViolence",
                table: "TempHtsEligibilityExtracts",
                newName: "Lethargy");

            migrationBuilder.RenameColumn(
                name: "PhysicalViolence",
                table: "TempHtsEligibilityExtracts",
                newName: "ContactWithTBCase");

            migrationBuilder.RenameColumn(
                name: "PartnerHivStatus",
                table: "HtsEligibilityExtracts",
                newName: "PartnerHIVStatus");

            migrationBuilder.RenameColumn(
                name: "SexualViolence",
                table: "HtsEligibilityExtracts",
                newName: "Lethargy");

            migrationBuilder.RenameColumn(
                name: "PhysicalViolence",
                table: "HtsEligibilityExtracts",
                newName: "ContactWithTBCase");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModified",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModified",
                table: "HtsEligibilityExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "HtsEligibilityExtracts");

            migrationBuilder.RenameColumn(
                name: "PartnerHIVStatus",
                table: "TempHtsEligibilityExtracts",
                newName: "PartnerHivStatus");

            migrationBuilder.RenameColumn(
                name: "Lethargy",
                table: "TempHtsEligibilityExtracts",
                newName: "SexualViolence");

            migrationBuilder.RenameColumn(
                name: "ContactWithTBCase",
                table: "TempHtsEligibilityExtracts",
                newName: "PhysicalViolence");

            migrationBuilder.RenameColumn(
                name: "PartnerHIVStatus",
                table: "HtsEligibilityExtracts",
                newName: "PartnerHivStatus");

            migrationBuilder.RenameColumn(
                name: "Lethargy",
                table: "HtsEligibilityExtracts",
                newName: "SexualViolence");

            migrationBuilder.RenameColumn(
                name: "ContactWithTBCase",
                table: "HtsEligibilityExtracts",
                newName: "PhysicalViolence");

            migrationBuilder.AddColumn<string>(
                name: "CurrentlyHasTB",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmotionalViolence",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentlyHasTB",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmotionalViolence",
                table: "HtsEligibilityExtracts",
                nullable: true);
        }
    }
}
