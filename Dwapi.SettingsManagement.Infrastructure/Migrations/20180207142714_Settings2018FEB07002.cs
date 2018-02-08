using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class Settings2018FEB07002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdvancedProperties",
                table: "DatabaseProtocols",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CentralRegistries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthToken = table.Column<string>(maxLength: 100, nullable: true),
                    Url = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralRegistries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentralRegistries");

            migrationBuilder.DropColumn(
                name: "AdvancedProperties",
                table: "DatabaseProtocols");
        }
    }
}
