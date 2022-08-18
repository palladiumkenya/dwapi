using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class PatientExtractsAddedRefillDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<DateTime>(
                name: "RefillDate",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefillDate",
                table: "PatientVisitExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefillDate",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "RefillDate",
                table: "PatientVisitExtracts");
        }
    }
}
