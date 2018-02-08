using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class Settings2018FEB07000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Version = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DatabaseProtocols",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Database = table.Column<string>(maxLength: 100, nullable: true),
                    DatabaseType = table.Column<int>(nullable: false),
                    EmrSystemId = table.Column<Guid>(nullable: false),
                    Host = table.Column<string>(maxLength: 100, nullable: true),
                    Password = table.Column<string>(maxLength: 100, nullable: true),
                    Port = table.Column<int>(nullable: true),
                    Username = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseProtocols", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatabaseProtocols_EmrSystems_EmrSystemId",
                        column: x => x.EmrSystemId,
                        principalTable: "EmrSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestProtocols",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthToken = table.Column<string>(maxLength: 100, nullable: true),
                    EmrSystemId = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestProtocols", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestProtocols_EmrSystems_EmrSystemId",
                        column: x => x.EmrSystemId,
                        principalTable: "EmrSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseProtocols_EmrSystemId",
                table: "DatabaseProtocols",
                column: "EmrSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_RestProtocols_EmrSystemId",
                table: "RestProtocols",
                column: "EmrSystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatabaseProtocols");

            migrationBuilder.DropTable(
                name: "RestProtocols");

            migrationBuilder.DropTable(
                name: "EmrSystems");
        }
    }
}
