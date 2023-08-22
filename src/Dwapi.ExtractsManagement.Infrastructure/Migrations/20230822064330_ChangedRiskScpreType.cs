using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ChangedRiskScpreType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HtsRiskScore",
                table: "TempHtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HtsRiskScore",
                table: "TempHtsClientTestsExtracts",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HtsRiskScore",
                table: "HtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HtsRiskScore",
                table: "HtsClientTestsExtracts",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HtsRiskScore",
                table: "TempHtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "HtsRiskScore",
                table: "TempHtsClientTestsExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "HtsRiskScore",
                table: "HtsEligibilityExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "HtsRiskScore",
                table: "HtsClientTestsExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
