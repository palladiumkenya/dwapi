using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddedMissingColumnsToView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacilityName",
                table: "TempPncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacilityName",
                table: "TempMotherBabyPairExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacilityName",
                table: "TempCwcEnrolmentExtracts",
                nullable: true);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "FacilityName",
                table: "TempPncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "FacilityName",
                table: "TempMotherBabyPairExtracts");

            migrationBuilder.DropColumn(
                name: "FacilityName",
                table: "TempCwcEnrolmentExtracts");


        }
    }
}
