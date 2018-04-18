using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddValidationModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Validators",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Extract = table.Column<string>(nullable: true),
                    Field = table.Column<string>(nullable: true),
                    Logic = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValidationErrors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    FieldName = table.Column<string>(nullable: true),
                    ReferencedEntityId = table.Column<string>(nullable: true),
                    ValidatorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationErrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValidationErrors_Validators_ValidatorId",
                        column: x => x.ValidatorId,
                        principalTable: "Validators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ValidationErrors_ValidatorId",
                table: "ValidationErrors",
                column: "ValidatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValidationErrors");

            migrationBuilder.DropTable(
                name: "Validators");
        }
    }
}
