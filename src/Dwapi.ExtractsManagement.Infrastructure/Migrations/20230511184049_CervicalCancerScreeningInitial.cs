using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CervicalCancerScreeningInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CervicalCancerScreeningExtracts",
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
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    ScreeningMethod = table.Column<string>(nullable: true),
                    TreatmentToday = table.Column<string>(nullable: true),
                    ReferredOut = table.Column<string>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    ScreeningType = table.Column<string>(nullable: true),
                    ScreeningResult = table.Column<string>(nullable: true),
                    PostTreatmentComplicationCause = table.Column<string>(nullable: true),
                    OtherPostTreatmentComplication = table.Column<string>(nullable: true),
                    ReferralReason = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CervicalCancerScreeningExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CervicalCancerScreeningExtracts_PatientExtracts_SiteCode_Pat~",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempCervicalCancerScreeningExtracts",
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
                    ErrorType = table.Column<int>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    ScreeningMethod = table.Column<string>(nullable: true),
                    TreatmentToday = table.Column<string>(nullable: true),
                    ReferredOut = table.Column<string>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    ScreeningType = table.Column<string>(nullable: true),
                    ScreeningResult = table.Column<string>(nullable: true),
                    PostTreatmentComplicationCause = table.Column<string>(nullable: true),
                    OtherPostTreatmentComplication = table.Column<string>(nullable: true),
                    ReferralReason = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCervicalCancerScreeningExtracts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CervicalCancerScreeningExtracts_SiteCode_PatientPK",
                table: "CervicalCancerScreeningExtracts",
                columns: new[] { "SiteCode", "PatientPK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CervicalCancerScreeningExtracts");

            migrationBuilder.DropTable(
                name: "TempCervicalCancerScreeningExtracts");
        }
    }
}
