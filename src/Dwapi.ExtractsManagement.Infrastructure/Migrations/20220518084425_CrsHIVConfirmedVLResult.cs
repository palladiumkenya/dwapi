using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CrsHIVConfirmedVLResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfHIVdiagnosis",
                table: "TempClientRegistryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastViralLoadResult",
                table: "TempClientRegistryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfHIVdiagnosis",
                table: "ClientRegistryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastViralLoadResult",
                table: "ClientRegistryExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfHIVdiagnosis",
                table: "TempClientRegistryExtracts");

            migrationBuilder.DropColumn(
                name: "LastViralLoadResult",
                table: "TempClientRegistryExtracts");

            migrationBuilder.DropColumn(
                name: "DateOfHIVdiagnosis",
                table: "ClientRegistryExtracts");

            migrationBuilder.DropColumn(
                name: "LastViralLoadResult",
                table: "ClientRegistryExtracts");
        }
    }
}
