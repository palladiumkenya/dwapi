using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class TempPatientExtractColumnChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateRegistrationAtTBClinic",
                table: "TempPatientExtracts",
                newName: "RegistrationDate");

            migrationBuilder.RenameColumn(
                name: "DateRegistrationAtPMTCT",
                table: "TempPatientExtracts",
                newName: "RegistrationAtTBClinic");

            migrationBuilder.RenameColumn(
                name: "DateRegistrationAtCCC",
                table: "TempPatientExtracts",
                newName: "RegistrationAtPMTCT");

            migrationBuilder.RenameColumn(
                name: "DateRegistered",
                table: "TempPatientExtracts",
                newName: "RegistrationAtCCC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "TempPatientExtracts",
                newName: "DateRegistrationAtTBClinic");

            migrationBuilder.RenameColumn(
                name: "RegistrationAtTBClinic",
                table: "TempPatientExtracts",
                newName: "DateRegistrationAtPMTCT");

            migrationBuilder.RenameColumn(
                name: "RegistrationAtPMTCT",
                table: "TempPatientExtracts",
                newName: "DateRegistrationAtCCC");

            migrationBuilder.RenameColumn(
                name: "RegistrationAtCCC",
                table: "TempPatientExtracts",
                newName: "DateRegistered");
        }
    }
}
