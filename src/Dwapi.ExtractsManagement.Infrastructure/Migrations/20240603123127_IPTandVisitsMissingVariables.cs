using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class IPTandVisitsMissingVariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppointmentReminderWillingness",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WantsToGetPregnant",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adherence",
                table: "TempIptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hepatoxicity",
                table: "TempIptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PeripheralNeuropathy",
                table: "TempIptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rash",
                table: "TempIptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppointmentReminderWillingness",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WantsToGetPregnant",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adherence",
                table: "IptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hepatoxicity",
                table: "IptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PeripheralNeuropathy",
                table: "IptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rash",
                table: "IptExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentReminderWillingness",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "WantsToGetPregnant",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Adherence",
                table: "TempIptExtracts");

            migrationBuilder.DropColumn(
                name: "Hepatoxicity",
                table: "TempIptExtracts");

            migrationBuilder.DropColumn(
                name: "PeripheralNeuropathy",
                table: "TempIptExtracts");

            migrationBuilder.DropColumn(
                name: "Rash",
                table: "TempIptExtracts");

            migrationBuilder.DropColumn(
                name: "AppointmentReminderWillingness",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "WantsToGetPregnant",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Adherence",
                table: "IptExtracts");

            migrationBuilder.DropColumn(
                name: "Hepatoxicity",
                table: "IptExtracts");

            migrationBuilder.DropColumn(
                name: "PeripheralNeuropathy",
                table: "IptExtracts");

            migrationBuilder.DropColumn(
                name: "Rash",
                table: "IptExtracts");
        }
    }
}
