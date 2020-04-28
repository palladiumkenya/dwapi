using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class MgsModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetricMigrationExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    MetricId = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    HtsNumber = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    Dataset = table.Column<string>(nullable: true),
                    Metric = table.Column<string>(nullable: true),
                    MetricValue = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricMigrationExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempMetricMigrationExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    MetricId = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    ErrorType = table.Column<int>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Dataset = table.Column<string>(nullable: true),
                    Metric = table.Column<string>(nullable: true),
                    MetricValue = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMetricMigrationExtracts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetricMigrationExtracts");

            migrationBuilder.DropTable(
                name: "TempMetricMigrationExtracts");
        }
    }
}
