using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class ExtractReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DatabaseProtocolId",
                table: "Extracts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Extracts_DatabaseProtocolId",
                table: "Extracts",
                column: "DatabaseProtocolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extracts_DatabaseProtocols_DatabaseProtocolId",
                table: "Extracts",
                column: "DatabaseProtocolId",
                principalTable: "DatabaseProtocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extracts_DatabaseProtocols_DatabaseProtocolId",
                table: "Extracts");

            migrationBuilder.DropIndex(
                name: "IX_Extracts_DatabaseProtocolId",
                table: "Extracts");

            migrationBuilder.DropColumn(
                name: "DatabaseProtocolId",
                table: "Extracts");
        }
    }
}
