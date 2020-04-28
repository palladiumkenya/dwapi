using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AdverseEventsUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdverseEventActionTaken",
                table: "TempPatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdverseEventClinicalOutcome",
                table: "TempPatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AdverseEventIsPregnant",
                table: "TempPatientAdverseEventExtracts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AdverseEventActionTaken",
                table: "PatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdverseEventClinicalOutcome",
                table: "PatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AdverseEventIsPregnant",
                table: "PatientAdverseEventExtracts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdverseEventActionTaken",
                table: "TempPatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "AdverseEventClinicalOutcome",
                table: "TempPatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "AdverseEventIsPregnant",
                table: "TempPatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "AdverseEventActionTaken",
                table: "PatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "AdverseEventClinicalOutcome",
                table: "PatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "AdverseEventIsPregnant",
                table: "PatientAdverseEventExtracts");
        }
    }
}
