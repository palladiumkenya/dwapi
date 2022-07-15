using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsEligibilityAddedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTested",
                table: "TempHtsEligibilityExtracts",
                newName: "DateTestedSelf");

            migrationBuilder.RenameColumn(
                name: "DateTested",
                table: "HtsEligibilityExtracts",
                newName: "DateTestedSelf");

            migrationBuilder.AddColumn<string>(
                name: "Cough",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTestedProvider",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmotionalViolence",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fever",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MothersStatus",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NightSweats",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferredForTesting",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResultOfHIVSelf",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScreenedTB",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TBStatus",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeightLoss",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cough",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTestedProvider",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmotionalViolence",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fever",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MothersStatus",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NightSweats",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferredForTesting",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResultOfHIVSelf",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScreenedTB",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TBStatus",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeightLoss",
                table: "HtsEligibilityExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cough",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "DateTestedProvider",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "EmotionalViolence",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "Fever",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "MothersStatus",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "NightSweats",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ReferredForTesting",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ResultOfHIVSelf",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ScreenedTB",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "TBStatus",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "WeightLoss",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "Cough",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "DateTestedProvider",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "EmotionalViolence",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "Fever",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "MothersStatus",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "NightSweats",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ReferredForTesting",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ResultOfHIVSelf",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "ScreenedTB",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "TBStatus",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "WeightLoss",
                table: "HtsEligibilityExtracts");

            migrationBuilder.RenameColumn(
                name: "DateTestedSelf",
                table: "TempHtsEligibilityExtracts",
                newName: "DateTested");

            migrationBuilder.RenameColumn(
                name: "DateTestedSelf",
                table: "HtsEligibilityExtracts",
                newName: "DateTested");
        }
    }
}
