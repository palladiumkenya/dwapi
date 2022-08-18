using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class TransportLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransportLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Docket = table.Column<string>(nullable: true),
                    Extract = table.Column<string>(nullable: true),
                    ManifestId = table.Column<Guid>(nullable: false),
                    JobId = table.Column<string>(nullable: true),
                    JobStart = table.Column<DateTime>(nullable: false),
                    JobEnd = table.Column<DateTime>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportLogs");
        }
    }
}
