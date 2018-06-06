using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class MpiJaroWinklerScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "JaroWinklerScore",
                table: "TempMasterPatientIndices",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "JaroWinklerScore",
                table: "MasterPatientIndices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JaroWinklerScore",
                table: "TempMasterPatientIndices");

            migrationBuilder.DropColumn(
                name: "JaroWinklerScore",
                table: "MasterPatientIndices");
        }
    }
}
