using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsVisitIDTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VisitID",
                table: "TempHtsEligibilityExtracts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VisitID",
                table: "HtsEligibilityExtracts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HtsEligibilityExtracts_SiteCode_PatientPk",
                table: "HtsEligibilityExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.AddForeignKey(
                name: "FK_HtsEligibilityExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsEligibilityExtracts",
                columns: new[] { "SiteCode", "PatientPk" },
                principalTable: "HtsClientsExtracts",
                principalColumns: new[] { "SiteCode", "PatientPk" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HtsEligibilityExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropIndex(
                name: "IX_HtsEligibilityExtracts_SiteCode_PatientPk",
                table: "HtsEligibilityExtracts");

            migrationBuilder.AlterColumn<string>(
                name: "VisitID",
                table: "TempHtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "VisitID",
                table: "HtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
