using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ExtractsInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PsmartStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateStaged = table.Column<DateTime>(nullable: false),
                    Demographics = table.Column<string>(maxLength: 100, nullable: true),
                    Emr = table.Column<string>(maxLength: 50, nullable: true),
                    Encounters = table.Column<string>(maxLength: 100, nullable: true),
                    FacilityCode = table.Column<int>(nullable: true),
                    Serial = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsmartStages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PsmartStages");
        }
    }
}
