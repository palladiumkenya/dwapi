using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class AppMetrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"CREATE TABLE `AppMetrics` (
                    `Id` char(36) NOT NULL,
                    `Version` longtext NULL,
                    `Name` longtext NULL,
                    `LogDate` datetime NOT NULL,
                    `LogValue` longtext NULL,
                    `Status` int NOT NULL,
                    CONSTRAINT `PK_AppMetrics` PRIMARY KEY (`Id`)
                );");
            }
            else
            {
                migrationBuilder.CreateTable(
                name: "AppMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LogDate = table.Column<DateTime>(nullable: false),
                    LogValue = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMetrics", x => x.Id);
                });
            }
                
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppMetrics");
        }
    }
}
