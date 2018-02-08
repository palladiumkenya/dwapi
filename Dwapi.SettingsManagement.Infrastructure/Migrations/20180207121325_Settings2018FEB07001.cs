using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class Settings2018FEB07001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Database",
                table: "DatabaseProtocols",
                newName: "DatabaseName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DatabaseName",
                table: "DatabaseProtocols",
                newName: "Database");
        }
    }
}
