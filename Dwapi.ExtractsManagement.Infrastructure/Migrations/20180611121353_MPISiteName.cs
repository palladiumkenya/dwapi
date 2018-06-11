using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class MPISiteName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacilityName",
                table: "TempMasterPatientIndices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacilityName",
                table: "MasterPatientIndices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacilityName",
                table: "TempMasterPatientIndices");

            migrationBuilder.DropColumn(
                name: "FacilityName",
                table: "MasterPatientIndices");
        }
    }
}
