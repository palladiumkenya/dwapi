using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class Settings2018FEB10002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dockets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dockets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Extracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Display = table.Column<string>(maxLength: 100, nullable: true),
                    DocketId = table.Column<Guid>(nullable: false),
                    ExtractSql = table.Column<string>(maxLength: 8000, nullable: true),
                    IsPriority = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Rank = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extracts_Dockets_DocketId",
                        column: x => x.DocketId,
                        principalTable: "Dockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtractDestinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtractId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Rank = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractDestinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtractDestinations_Extracts_ExtractId",
                        column: x => x.ExtractId,
                        principalTable: "Extracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtractDestinations_ExtractId",
                table: "ExtractDestinations",
                column: "ExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_Extracts_DocketId",
                table: "Extracts",
                column: "DocketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtractDestinations");

            migrationBuilder.DropTable(
                name: "Extracts");

            migrationBuilder.DropTable(
                name: "Dockets");
        }
    }
}
