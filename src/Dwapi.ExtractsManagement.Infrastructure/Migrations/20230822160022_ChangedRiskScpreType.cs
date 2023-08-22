using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ChangedRiskScpreType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDiscontinuation",
                table: "TempIptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IPTDiscontinuation",
                table: "TempIptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TPTInitiationDate",
                table: "TempIptExtracts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HtsRiskScore",
                table: "TempHtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HtsRiskScore",
                table: "TempHtsClientTestsExtracts",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDiscontinuation",
                table: "IptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IPTDiscontinuation",
                table: "IptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TPTInitiationDate",
                table: "IptExtracts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HtsRiskScore",
                table: "HtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HtsRiskScore",
                table: "HtsClientTestsExtracts",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "DateOfDiscontinuation",
                table: "TempIptExtracts");

            migrationBuilder.DropColumn(
                name: "IPTDiscontinuation",
                table: "TempIptExtracts");

            migrationBuilder.DropColumn(
                name: "TPTInitiationDate",
                table: "TempIptExtracts");

            migrationBuilder.DropColumn(
                name: "DateOfDiscontinuation",
                table: "IptExtracts");

            migrationBuilder.DropColumn(
                name: "IPTDiscontinuation",
                table: "IptExtracts");

            migrationBuilder.DropColumn(
                name: "TPTInitiationDate",
                table: "IptExtracts");

            migrationBuilder.AlterColumn<decimal>(
                name: "HtsRiskScore",
                table: "vTempHtsClientTestsExtractErrorSummary",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "HtsRiskScore",
                table: "vTempHtsClientTestsError",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "HtsRiskScore",
                table: "TempHtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "HtsRiskScore",
                table: "TempHtsClientTestsExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "HtsRiskScore",
                table: "HtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "HtsRiskScore",
                table: "HtsClientTestsExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
