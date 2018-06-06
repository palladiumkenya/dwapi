using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class MpiJaroViewScoreDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "JaroWinklerScore",
                table: "TempMasterPatientIndices",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "JaroWinklerScore",
                table: "MasterPatientIndices",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "JaroWinklerScore",
                table: "TempMasterPatientIndices",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "JaroWinklerScore",
                table: "MasterPatientIndices",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
