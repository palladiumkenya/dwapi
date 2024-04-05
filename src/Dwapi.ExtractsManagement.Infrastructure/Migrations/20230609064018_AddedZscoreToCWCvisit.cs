using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddedZscoreToCWCvisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZScoreAbsolute",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZScore",
                table: "TempCwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZScoreAbsolute",
                table: "TempCwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZScoreAbsolute",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZScore",
                table: "CwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZScoreAbsolute",
                table: "CwcVisitExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZScoreAbsolute",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ZScore",
                table: "TempCwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ZScoreAbsolute",
                table: "TempCwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ZScoreAbsolute",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ZScore",
                table: "CwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ZScoreAbsolute",
                table: "CwcVisitExtracts");
        }
    }
}
