using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class LabReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "TempPatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "PatientLaboratoryExtracts",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "PatientLaboratoryExtracts");
        }
    }
}
