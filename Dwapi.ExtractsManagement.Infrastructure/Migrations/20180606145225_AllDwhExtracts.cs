using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AllDwhExtracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientArtExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    AgeEnrollment = table.Column<decimal>(nullable: true),
                    AgeARTStart = table.Column<decimal>(nullable: true),
                    AgeLastVisit = table.Column<decimal>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
                    PatientSource = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    StartARTDate = table.Column<DateTime>(nullable: true),
                    PreviousARTStartDate = table.Column<DateTime>(nullable: true),
                    PreviousARTRegimen = table.Column<string>(nullable: true),
                    StartARTAtThisFacility = table.Column<DateTime>(nullable: true),
                    StartRegimen = table.Column<string>(nullable: true),
                    StartRegimenLine = table.Column<string>(nullable: true),
                    LastARTDate = table.Column<DateTime>(nullable: true),
                    LastRegimen = table.Column<string>(nullable: true),
                    LastRegimenLine = table.Column<string>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true),
                    ExpectedReturn = table.Column<DateTime>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientArtExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientBaselinesExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    bCD4 = table.Column<int>(nullable: true),
                    bCD4Date = table.Column<DateTime>(nullable: true),
                    bWAB = table.Column<int>(nullable: true),
                    bWABDate = table.Column<DateTime>(nullable: true),
                    bWHO = table.Column<int>(nullable: true),
                    bWHODate = table.Column<DateTime>(nullable: true),
                    eWAB = table.Column<int>(nullable: true),
                    eWABDate = table.Column<DateTime>(nullable: true),
                    eCD4 = table.Column<int>(nullable: true),
                    eCD4Date = table.Column<DateTime>(nullable: true),
                    eWHO = table.Column<int>(nullable: true),
                    eWHODate = table.Column<DateTime>(nullable: true),
                    lastWHO = table.Column<int>(nullable: true),
                    lastWHODate = table.Column<DateTime>(nullable: true),
                    lastCD4 = table.Column<int>(nullable: true),
                    lastCD4Date = table.Column<DateTime>(nullable: true),
                    lastWAB = table.Column<int>(nullable: true),
                    lastWABDate = table.Column<DateTime>(nullable: true),
                    m12CD4 = table.Column<int>(nullable: true),
                    m12CD4Date = table.Column<DateTime>(nullable: true),
                    m6CD4 = table.Column<int>(nullable: true),
                    m6CD4Date = table.Column<DateTime>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientBaselinesExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientLaboratoryExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    SatelliteName = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    VisitId = table.Column<int>(nullable: true),
                    OrderedByDate = table.Column<DateTime>(nullable: true),
                    ReportedByDate = table.Column<DateTime>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    EnrollmentTest = table.Column<int>(nullable: true),
                    TestResult = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientLaboratoryExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientPharmacyExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    VisitID = table.Column<int>(nullable: true),
                    Drug = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    DispenseDate = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true),
                    ExpectedReturn = table.Column<DateTime>(nullable: true),
                    TreatmentType = table.Column<string>(nullable: true),
                    RegimenLine = table.Column<string>(nullable: true),
                    PeriodTaken = table.Column<string>(nullable: true),
                    ProphylaxisType = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPharmacyExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientStatusExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    ExitDescription = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientStatusExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientVisitExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    VisitId = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    Service = table.Column<string>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    WHOStage = table.Column<int>(nullable: true),
                    WABStage = table.Column<string>(nullable: true),
                    Pregnant = table.Column<string>(nullable: true),
                    LMP = table.Column<DateTime>(nullable: true),
                    EDD = table.Column<DateTime>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true),
                    BP = table.Column<string>(nullable: true),
                    OI = table.Column<string>(nullable: true),
                    OIDate = table.Column<DateTime>(nullable: true),
                    Adherence = table.Column<string>(nullable: true),
                    AdherenceCategory = table.Column<string>(nullable: true),
                    SubstitutionFirstlineRegimenDate = table.Column<DateTime>(nullable: true),
                    SubstitutionFirstlineRegimenReason = table.Column<string>(nullable: true),
                    SubstitutionSecondlineRegimenDate = table.Column<DateTime>(nullable: true),
                    SubstitutionSecondlineRegimenReason = table.Column<string>(nullable: true),
                    SecondlineRegimenChangeDate = table.Column<DateTime>(nullable: true),
                    SecondlineRegimenChangeReason = table.Column<string>(nullable: true),
                    FamilyPlanningMethod = table.Column<string>(nullable: true),
                    PwP = table.Column<string>(nullable: true),
                    GestationAge = table.Column<decimal>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVisitExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientArtExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    AgeEnrollment = table.Column<decimal>(nullable: true),
                    AgeARTStart = table.Column<decimal>(nullable: true),
                    AgeLastVisit = table.Column<decimal>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
                    PatientSource = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    StartARTDate = table.Column<DateTime>(nullable: true),
                    PreviousARTStartDate = table.Column<DateTime>(nullable: true),
                    PreviousARTRegimen = table.Column<string>(nullable: true),
                    StartARTAtThisFacility = table.Column<DateTime>(nullable: true),
                    StartRegimen = table.Column<string>(nullable: true),
                    StartRegimenLine = table.Column<string>(nullable: true),
                    LastARTDate = table.Column<DateTime>(nullable: true),
                    LastRegimen = table.Column<string>(nullable: true),
                    LastRegimenLine = table.Column<string>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true),
                    ExpectedReturn = table.Column<DateTime>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientArtExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientBaselinesExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    bCD4 = table.Column<int>(nullable: true),
                    bCD4Date = table.Column<DateTime>(nullable: true),
                    bWAB = table.Column<int>(nullable: true),
                    bWABDate = table.Column<DateTime>(nullable: true),
                    bWHO = table.Column<int>(nullable: true),
                    bWHODate = table.Column<DateTime>(nullable: true),
                    eWAB = table.Column<int>(nullable: true),
                    eWABDate = table.Column<DateTime>(nullable: true),
                    eCD4 = table.Column<int>(nullable: true),
                    eCD4Date = table.Column<DateTime>(nullable: true),
                    eWHO = table.Column<int>(nullable: true),
                    eWHODate = table.Column<DateTime>(nullable: true),
                    lastWHO = table.Column<int>(nullable: true),
                    lastWHODate = table.Column<DateTime>(nullable: true),
                    lastCD4 = table.Column<int>(nullable: true),
                    lastCD4Date = table.Column<DateTime>(nullable: true),
                    lastWAB = table.Column<int>(nullable: true),
                    lastWABDate = table.Column<DateTime>(nullable: true),
                    m12CD4 = table.Column<int>(nullable: true),
                    m12CD4Date = table.Column<DateTime>(nullable: true),
                    m6CD4 = table.Column<int>(nullable: true),
                    m6CD4Date = table.Column<DateTime>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientBaselinesExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientLaboratoryExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    SatelliteName = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    VisitId = table.Column<int>(nullable: true),
                    OrderedByDate = table.Column<DateTime>(nullable: true),
                    ReportedByDate = table.Column<DateTime>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    EnrollmentTest = table.Column<int>(nullable: true),
                    TestResult = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientLaboratoryExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientPharmacyExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    VisitID = table.Column<int>(nullable: true),
                    Drug = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    DispenseDate = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true),
                    ExpectedReturn = table.Column<DateTime>(nullable: true),
                    TreatmentType = table.Column<string>(nullable: true),
                    RegimenLine = table.Column<string>(nullable: true),
                    PeriodTaken = table.Column<string>(nullable: true),
                    ProphylaxisType = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientPharmacyExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientStatusExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    ExitDescription = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientStatusExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientVisitExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    VisitId = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    Service = table.Column<string>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    WHOStage = table.Column<int>(nullable: true),
                    WABStage = table.Column<string>(nullable: true),
                    Pregnant = table.Column<string>(nullable: true),
                    LMP = table.Column<DateTime>(nullable: true),
                    EDD = table.Column<DateTime>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true),
                    BP = table.Column<string>(nullable: true),
                    OI = table.Column<string>(nullable: true),
                    OIDate = table.Column<DateTime>(nullable: true),
                    Adherence = table.Column<string>(nullable: true),
                    AdherenceCategory = table.Column<string>(nullable: true),
                    SubstitutionFirstlineRegimenDate = table.Column<DateTime>(nullable: true),
                    SubstitutionFirstlineRegimenReason = table.Column<string>(nullable: true),
                    SubstitutionSecondlineRegimenDate = table.Column<DateTime>(nullable: true),
                    SubstitutionSecondlineRegimenReason = table.Column<string>(nullable: true),
                    SecondlineRegimenChangeDate = table.Column<DateTime>(nullable: true),
                    SecondlineRegimenChangeReason = table.Column<string>(nullable: true),
                    FamilyPlanningMethod = table.Column<string>(nullable: true),
                    PwP = table.Column<string>(nullable: true),
                    GestationAge = table.Column<decimal>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientVisitExtracts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientArtExtracts");

            migrationBuilder.DropTable(
                name: "PatientBaselinesExtracts");

            migrationBuilder.DropTable(
                name: "PatientLaboratoryExtracts");

            migrationBuilder.DropTable(
                name: "PatientPharmacyExtracts");

            migrationBuilder.DropTable(
                name: "PatientStatusExtracts");

            migrationBuilder.DropTable(
                name: "PatientVisitExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientArtExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientBaselinesExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientPharmacyExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientStatusExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientVisitExtracts");
        }
    }
}
