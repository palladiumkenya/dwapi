using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CT_Patient_TransferInDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferInDate",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferInDate",
                table: "PatientExtracts",
                nullable: true);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransferInDate",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "TransferInDate",
                table: "PatientExtracts");
        }
    }
}
