using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CtContactPatientPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactPatientPK",
                table: "TempContactListingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContactPatientPK",
                table: "ContactListingExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactPatientPK",
                table: "TempContactListingExtracts");

            migrationBuilder.DropColumn(
                name: "ContactPatientPK",
                table: "ContactListingExtracts");

        }
    }
}
