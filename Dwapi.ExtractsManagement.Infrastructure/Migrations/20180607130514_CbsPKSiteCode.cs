using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CbsPKSiteCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientPk",
                table: "TempMasterPatientIndices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteCode",
                table: "TempMasterPatientIndices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientPk",
                table: "MasterPatientIndices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SiteCode",
                table: "MasterPatientIndices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientPk",
                table: "TempMasterPatientIndices");

            migrationBuilder.DropColumn(
                name: "SiteCode",
                table: "TempMasterPatientIndices");

            migrationBuilder.DropColumn(
                name: "PatientPk",
                table: "MasterPatientIndices");

            migrationBuilder.DropColumn(
                name: "SiteCode",
                table: "MasterPatientIndices");
        }
    }
}
