using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class UpdatedMNCHandPatientVisits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaedsDisclosure",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ZScore",
                table: "TempPatientVisitExtracts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "BP",
                table: "TempAncVisitExtracts",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaedsDisclosure",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ZScore",
                table: "PatientVisitExtracts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "BP",
                table: "AncVisitExtracts",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaedsDisclosure",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ZScore",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PaedsDisclosure",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ZScore",
                table: "PatientVisitExtracts");

            migrationBuilder.AlterColumn<int>(
                name: "BP",
                table: "TempAncVisitExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BP",
                table: "AncVisitExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
