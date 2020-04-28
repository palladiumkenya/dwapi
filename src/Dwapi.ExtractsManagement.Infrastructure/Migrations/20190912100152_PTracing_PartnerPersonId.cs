using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class PTracing_PartnerPersonId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartnerPersonId",
                table: "HtsPartnerTracingExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartnerPersonId",
                table: "HtsPartnerTracingExtracts");
        }
    }
}
