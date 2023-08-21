using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class UpdatedIPTextract : Migration
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
        }
    }
}
