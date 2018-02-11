using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class Extracts2018FEB11000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateExtracted",
                table: "PsmartStages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateExtracted",
                table: "PsmartStages");
        }
    }
}
