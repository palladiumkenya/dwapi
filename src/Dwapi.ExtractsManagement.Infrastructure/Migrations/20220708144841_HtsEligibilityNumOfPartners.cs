using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsEligibilityNumOfPartners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberPartners",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.RenameColumn(
                name: "NumberPartners",
                table: "HtsEligibilityExtracts",
                newName: "NumberOfPartners");

            migrationBuilder.AlterColumn<int>(
                name: "VisitID",
                table: "TempHtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SpecificReasonForIneligibility",
                table: "TempHtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPartners",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VisitID",
                table: "HtsEligibilityExtracts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
            

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HtsEligibilityExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropIndex(
                name: "IX_HtsEligibilityExtracts_SiteCode_PatientPk",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "NumberOfPartners",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.RenameColumn(
                name: "NumberOfPartners",
                table: "HtsEligibilityExtracts",
                newName: "NumberPartners");

            migrationBuilder.AlterColumn<string>(
                name: "VisitID",
                table: "TempHtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SpecificReasonForIneligibility",
                table: "TempHtsEligibilityExtracts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberPartners",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VisitID",
                table: "HtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
