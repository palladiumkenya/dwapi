using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class MetricStage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stage",
                table: "TempMetricMigrationExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stage",
                table: "MetricMigrationExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stage",
                table: "TempMetricMigrationExtracts");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "MetricMigrationExtracts");
        }
    }
}
