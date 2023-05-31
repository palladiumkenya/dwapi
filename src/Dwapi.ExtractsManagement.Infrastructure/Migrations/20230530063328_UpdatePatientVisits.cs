using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class UpdatePatientVisits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ZScore",
                table: "TempPatientVisitExtracts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZScore",
                table: "PatientVisitExtracts",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZScore",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ZScore",
                table: "PatientVisitExtracts");
        }
    }
}
