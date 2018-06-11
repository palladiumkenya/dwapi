using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class RegistryConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocketId",
                table: "CentralRegistries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CentralRegistries_DocketId",
                table: "CentralRegistries",
                column: "DocketId");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralRegistries_Dockets_DocketId",
                table: "CentralRegistries",
                column: "DocketId",
                principalTable: "Dockets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralRegistries_Dockets_DocketId",
                table: "CentralRegistries");

            migrationBuilder.DropIndex(
                name: "IX_CentralRegistries_DocketId",
                table: "CentralRegistries");

            migrationBuilder.DropColumn(
                name: "DocketId",
                table: "CentralRegistries");
        }
    }
}
