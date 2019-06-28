using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddingErrorTypesToExtracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "TempPatientVisitExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "TempPatientStatusExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "TempPatientPharmacyExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "TempPatientLaboratoryExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "TempPatientExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "TempPatientBaselinesExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "TempPatientArtExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "TempPatientAdverseEventExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "TempHtsClientPartnerExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "TempHtsClientLinkageExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "TempHtsClientExtracts",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "TempPatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "TempPatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "TempPatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "TempPatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "TempHtsClientPartnerExtracts");

            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "TempHtsClientLinkageExtracts");

            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "TempHtsClientExtracts");
        }
    }
}
