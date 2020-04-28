using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class EmrMetric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmrName = table.Column<string>(nullable: true),
                    EmrVersion = table.Column<string>(nullable: true),
                    LastLoginDate = table.Column<DateTime>(nullable: true),
                    LastMoH731RunDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrMetrics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrMetrics");
        }
    }
}
