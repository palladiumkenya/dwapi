using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class RemoveMotherBabyPairForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MotherBabyPairExtracts_PatientMnchExtracts_SiteCode_PatientPK",
                table: "MotherBabyPairExtracts");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
