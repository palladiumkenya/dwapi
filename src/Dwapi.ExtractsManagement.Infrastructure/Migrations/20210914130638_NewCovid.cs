using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class NewCovid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CovidExtracts",
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    Covid19AssessmentDate = table.Column<DateTime>(nullable: true),
                    ReceivedCOVID19Vaccine = table.Column<string>(nullable: true),
                    DateGivenFirstDose = table.Column<DateTime>(nullable: true),
                    FirstDoseVaccineAdministered = table.Column<string>(nullable: true),
                    DateGivenSecondDose = table.Column<DateTime>(nullable: true),
                    SecondDoseVaccineAdministered = table.Column<string>(nullable: true),
                    VaccinationStatus = table.Column<string>(nullable: true),
                    VaccineVerification = table.Column<string>(nullable: true),
                    BoosterGiven = table.Column<string>(nullable: true),
                    BoosterDose = table.Column<string>(nullable: true),
                    BoosterDoseDate = table.Column<DateTime>(nullable: true),
                    EverCOVID19Positive = table.Column<string>(nullable: true),
                    COVID19TestDate = table.Column<DateTime>(nullable: true),
                    PatientStatus = table.Column<string>(nullable: true),
                    AdmissionStatus = table.Column<string>(nullable: true),
                    AdmissionUnit = table.Column<string>(nullable: true),
                    MissedAppointmentDueToCOVID19 = table.Column<string>(nullable: true),
                    COVID19PositiveSinceLasVisit = table.Column<string>(nullable: true),
                    COVID19TestDateSinceLastVisit = table.Column<DateTime>(nullable: true),
                    PatientStatusSinceLastVisit = table.Column<string>(nullable: true),
                    AdmissionStatusSinceLastVisit = table.Column<string>(nullable: true),
                    AdmissionStartDate = table.Column<DateTime>(nullable: true),
                    AdmissionEndDate = table.Column<DateTime>(nullable: true),
                    AdmissionUnitSinceLastVisit = table.Column<string>(nullable: true),
                    SupplementalOxygenReceived = table.Column<string>(nullable: true),
                    PatientVentilated = table.Column<string>(nullable: true),
                    TracingFinalOutcome = table.Column<string>(nullable: true),
                    CauseOfDeath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CovidExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CovidExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefaulterTracingExtracts",
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    EncounterId = table.Column<int>(nullable: true),
                    TracingType = table.Column<string>(nullable: true),
                    TracingOutcome = table.Column<string>(nullable: true),
                    AttemptNumber = table.Column<int>(nullable: true),
                    IsFinalTrace = table.Column<string>(nullable: true),
                    TrueStatus = table.Column<string>(nullable: true),
                    CauseOfDeath = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    BookingDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaulterTracingExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefaulterTracingExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempCovidExtracts",
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
                    Covid19AssessmentDate = table.Column<DateTime>(nullable: true),
                    ReceivedCOVID19Vaccine = table.Column<string>(nullable: true),
                    DateGivenFirstDose = table.Column<DateTime>(nullable: true),
                    FirstDoseVaccineAdministered = table.Column<string>(nullable: true),
                    DateGivenSecondDose = table.Column<DateTime>(nullable: true),
                    SecondDoseVaccineAdministered = table.Column<string>(nullable: true),
                    VaccinationStatus = table.Column<string>(nullable: true),
                    VaccineVerification = table.Column<string>(nullable: true),
                    BoosterGiven = table.Column<string>(nullable: true),
                    BoosterDose = table.Column<string>(nullable: true),
                    BoosterDoseDate = table.Column<DateTime>(nullable: true),
                    EverCOVID19Positive = table.Column<string>(nullable: true),
                    COVID19TestDate = table.Column<DateTime>(nullable: true),
                    PatientStatus = table.Column<string>(nullable: true),
                    AdmissionStatus = table.Column<string>(nullable: true),
                    AdmissionUnit = table.Column<string>(nullable: true),
                    MissedAppointmentDueToCOVID19 = table.Column<string>(nullable: true),
                    COVID19PositiveSinceLasVisit = table.Column<string>(nullable: true),
                    COVID19TestDateSinceLastVisit = table.Column<DateTime>(nullable: true),
                    PatientStatusSinceLastVisit = table.Column<string>(nullable: true),
                    AdmissionStatusSinceLastVisit = table.Column<string>(nullable: true),
                    AdmissionStartDate = table.Column<DateTime>(nullable: true),
                    AdmissionEndDate = table.Column<DateTime>(nullable: true),
                    AdmissionUnitSinceLastVisit = table.Column<string>(nullable: true),
                    SupplementalOxygenReceived = table.Column<string>(nullable: true),
                    PatientVentilated = table.Column<string>(nullable: true),
                    TracingFinalOutcome = table.Column<string>(nullable: true),
                    CauseOfDeath = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCovidExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempDefaulterTracingExtracts",
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
                    EncounterId = table.Column<int>(nullable: true),
                    TracingType = table.Column<string>(nullable: true),
                    TracingOutcome = table.Column<string>(nullable: true),
                    AttemptNumber = table.Column<int>(nullable: true),
                    IsFinalTrace = table.Column<string>(nullable: true),
                    TrueStatus = table.Column<string>(nullable: true),
                    CauseOfDeath = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    BookingDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempDefaulterTracingExtracts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CovidExtracts_SiteCode_PatientPK",
                table: "CovidExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_DefaulterTracingExtracts_SiteCode_PatientPK",
                table: "DefaulterTracingExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 0;");
                migrationBuilder.Sql(@"alter table TempCovidExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempDefaulterTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table CovidExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table DefaulterTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 1;");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CovidExtracts");

            migrationBuilder.DropTable(
                name: "DefaulterTracingExtracts");

            migrationBuilder.DropTable(
                name: "TempCovidExtracts");

            migrationBuilder.DropTable(
                name: "TempDefaulterTracingExtracts");
        }
    }
}
