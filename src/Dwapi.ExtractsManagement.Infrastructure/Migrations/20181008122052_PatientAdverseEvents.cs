using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class PatientAdverseEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientAdverseEventExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    AdverseEvent = table.Column<string>(nullable: true),
                    AdverseEventStartDate = table.Column<DateTime>(nullable: false),
                    AdverseEventEndDate = table.Column<DateTime>(nullable: false),
                    Severity = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAdverseEventExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientAdverseEventExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientAdverseEventExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    AdverseEvent = table.Column<string>(nullable: true),
                    AdverseEventStartDate = table.Column<DateTime>(nullable: false),
                    AdverseEventEndDate = table.Column<DateTime>(nullable: false),
                    Severity = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientAdverseEventExtracts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientAdverseEventExtracts_SiteCode_PatientPK",
                table: "PatientAdverseEventExtracts",
                columns: new[] { "SiteCode", "PatientPK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientAdverseEventExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientAdverseEventExtracts");
        }
    }
}
