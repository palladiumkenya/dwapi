using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class SettingsInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentralRegistries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthToken = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    SubscriberId = table.Column<string>(maxLength: 50, nullable: true),
                    Url = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralRegistries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dockets",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dockets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmrSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsMiddleware = table.Column<bool>(nullable: false),
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
                    AdvancedProperties = table.Column<string>(maxLength: 100, nullable: true),
                    DatabaseName = table.Column<string>(maxLength: 100, nullable: true),
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
                name: "Extracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Destination = table.Column<string>(nullable: true),
                    Display = table.Column<string>(maxLength: 100, nullable: true),
                    DocketId = table.Column<string>(nullable: true),
                    EmrSystemId = table.Column<Guid>(nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Extracts_EmrSystems_EmrSystemId",
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
                name: "IX_Extracts_DocketId",
                table: "Extracts",
                column: "DocketId");

            migrationBuilder.CreateIndex(
                name: "IX_Extracts_EmrSystemId",
                table: "Extracts",
                column: "EmrSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_RestProtocols_EmrSystemId",
                table: "RestProtocols",
                column: "EmrSystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentralRegistries");

            migrationBuilder.DropTable(
                name: "DatabaseProtocols");

            migrationBuilder.DropTable(
                name: "Extracts");

            migrationBuilder.DropTable(
                name: "RestProtocols");

            migrationBuilder.DropTable(
                name: "Dockets");

            migrationBuilder.DropTable(
                name: "EmrSystems");
        }
    }
}
