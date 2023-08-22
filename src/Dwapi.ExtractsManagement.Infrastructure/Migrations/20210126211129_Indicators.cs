using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class Indicators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndicatorExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Indicator = table.Column<string>(nullable: true),
                    IndicatorValue = table.Column<string>(nullable: true),
                    IndicatorDate = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempIndicatorExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Indicator = table.Column<string>(nullable: true),
                    IndicatorValue = table.Column<string>(nullable: true),
                    IndicatorDate = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempIndicatorExtracts", x => x.Id);
                });

            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 0;");
                migrationBuilder.Sql(@"alter table TempIndicatorExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table IndicatorExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 1;");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndicatorExtracts");

            migrationBuilder.DropTable(
                name: "TempIndicatorExtracts");
        }
    }
}
