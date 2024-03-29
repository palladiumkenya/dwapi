﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    public partial class PreUploadChecks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntegrityChecks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    LogicType = table.Column<int>(nullable: false),
                    Logic = table.Column<string>(nullable: true),
                    Stage = table.Column<int>(nullable: false),
                    Docket = table.Column<string>(nullable: true),
                    EmrSystemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrityChecks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrityCheckRuns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IntegrityCheckId = table.Column<Guid>(nullable: false),
                    RunDate = table.Column<DateTime>(nullable: false),
                    RunStatus = table.Column<int>(nullable: false),
                    Finding = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrityCheckRuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegrityCheckRuns_IntegrityChecks_IntegrityCheckId",
                        column: x => x.IntegrityCheckId,
                        principalTable: "IntegrityChecks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntegrityCheckRuns_IntegrityCheckId",
                table: "IntegrityCheckRuns",
                column: "IntegrityCheckId");

            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 0;");
                migrationBuilder.Sql(@"alter table IntegrityCheckRuns convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table IntegrityChecks convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 1;");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrityCheckRuns");

            migrationBuilder.DropTable(
                name: "IntegrityChecks");
        }
    }
}
