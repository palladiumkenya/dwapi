using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class RestResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    EndPoint = table.Column<string>(nullable: true),
                    RestProtocolId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_RestProtocols_RestProtocolId",
                        column: x => x.RestProtocolId,
                        principalTable: "RestProtocols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_RestProtocolId",
                table: "Resources",
                column: "RestProtocolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}
