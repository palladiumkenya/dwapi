using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsRiskScoresInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HtsRiskScoresExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    PatientPk = table.Column<int>(nullable: false),
                    HtsNumber = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    SourceSysUUID = table.Column<string>(nullable: true),
                    RiskScore = table.Column<decimal>(nullable: true),
                    RiskFactors = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EvaluationDate = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsRiskScoresExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsRiskScoresExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                        columns: x => new { x.SiteCode, x.PatientPk },
                        principalTable: "HtsClientsExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPk" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsRiskScoresExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    PatientPk = table.Column<int>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    ErrorType = table.Column<int>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    SourceSysUUID = table.Column<string>(nullable: true),
                    RiskScore = table.Column<decimal>(nullable: true),
                    RiskFactors = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EvaluationDate = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsRiskScoresExtracts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HtsRiskScoresExtracts_SiteCode_PatientPk",
                table: "HtsRiskScoresExtracts",
                columns: new[] { "SiteCode", "PatientPk" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HtsRiskScoresExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsRiskScoresExtracts");
        }
    }
}
