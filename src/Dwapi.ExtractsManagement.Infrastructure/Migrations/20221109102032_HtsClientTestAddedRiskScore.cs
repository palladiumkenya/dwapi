using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsClientTestAddedRiskScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "HtsRiskCategory",
                table: "TempHtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HtsRiskScore",
                table: "TempHtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientHeiId",
                table: "TempHeiExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HtsRiskCategory",
                table: "HtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HtsRiskScore",
                table: "HtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientHeiId",
                table: "HeiExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "HtsRiskCategory",
                table: "TempHtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "HtsRiskScore",
                table: "TempHtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "PatientHeiId",
                table: "TempHeiExtracts");

            migrationBuilder.DropColumn(
                name: "HtsRiskCategory",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "HtsRiskScore",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "PatientHeiId",
                table: "HeiExtracts");
        }
    }
}
