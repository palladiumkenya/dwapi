using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class drophtskey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_HtsClientExtracts_Id",
                table: "HtsClientExtracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HtsClientExtracts",
                table: "HtsClientExtracts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HtsClientExtracts",
                table: "HtsClientExtracts",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HtsClientExtracts",
                table: "HtsClientExtracts");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_HtsClientExtracts_Id",
                table: "HtsClientExtracts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HtsClientExtracts",
                table: "HtsClientExtracts",
                columns: new[] { "SiteCode", "PatientPk", "EncounterId" });
        }
    }
}
