using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AncVisitsAddedVariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HepatitisBScreening",
                table: "TempAncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiminumPackageOfCareReceived",
                table: "TempAncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiminumPackageOfCareServices",
                table: "TempAncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresumptiveTreatmentDose",
                table: "TempAncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresumptiveTreatmentGiven",
                table: "TempAncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TreatedHepatitisB",
                table: "TempAncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HepatitisBScreening",
                table: "AncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiminumPackageOfCareReceived",
                table: "AncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiminumPackageOfCareServices",
                table: "AncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresumptiveTreatmentDose",
                table: "AncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresumptiveTreatmentGiven",
                table: "AncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TreatedHepatitisB",
                table: "AncVisitExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HepatitisBScreening",
                table: "TempAncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "MiminumPackageOfCareReceived",
                table: "TempAncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "MiminumPackageOfCareServices",
                table: "TempAncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PresumptiveTreatmentDose",
                table: "TempAncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PresumptiveTreatmentGiven",
                table: "TempAncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "TreatedHepatitisB",
                table: "TempAncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "HepatitisBScreening",
                table: "AncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "MiminumPackageOfCareReceived",
                table: "AncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "MiminumPackageOfCareServices",
                table: "AncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PresumptiveTreatmentDose",
                table: "AncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PresumptiveTreatmentGiven",
                table: "AncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "TreatedHepatitisB",
                table: "AncVisitExtracts");
        }
    }
}
