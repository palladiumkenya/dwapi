using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ExtractsInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.CreateTable(
                name: "Extracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Display = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extracts", x => x.Id);
                });
            */

            migrationBuilder.CreateTable(
                name: "PsmartStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "ExtractHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Accepted = table.Column<int>(nullable: true),
                    DateFound = table.Column<DateTime>(nullable: true),
                    DateLoaded = table.Column<DateTime>(nullable: true),
                    DateSent = table.Column<DateTime>(nullable: true),
                    DateValidated = table.Column<DateTime>(nullable: true),
                    ExtractId = table.Column<Guid>(nullable: false),
                    Found = table.Column<int>(nullable: true),
                    Loaded = table.Column<int>(nullable: true),
                    Queued = table.Column<int>(nullable: true),
                    Rejected = table.Column<int>(nullable: true),
                    Sent = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtractHistories_Extracts_ExtractId",
                        column: x => x.ExtractId,
                        principalTable: "Extracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtractHistories_ExtractId",
                table: "ExtractHistories",
                column: "ExtractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtractHistories");

            migrationBuilder.DropTable(
                name: "PsmartStages");
            /*
            migrationBuilder.DropTable(
                name: "Extracts");
            */
        }
    }
}
