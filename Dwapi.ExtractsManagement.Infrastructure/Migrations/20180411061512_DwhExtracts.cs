using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class DwhExtracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContactRelation = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    DateConfirmedHIVPositive = table.Column<DateTime>(nullable: true),
                    DateLastVisit = table.Column<DateTime>(nullable: true),
                    DatePreviousARTStart = table.Column<DateTime>(nullable: true),
                    DateRegistered = table.Column<DateTime>(nullable: true),
                    DateRegistrationAtCCC = table.Column<DateTime>(nullable: true),
                    DateRegistrationAtPMTCT = table.Column<DateTime>(nullable: true),
                    DateRegistrationAtTBClinic = table.Column<DateTime>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    EducationLevel = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: false),
                    PatientSource = table.Column<string>(nullable: true),
                    PreviousARTExposure = table.Column<string>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusAtCCC = table.Column<string>(nullable: true),
                    StatusAtPMTCT = table.Column<string>(nullable: true),
                    StatusAtTBClinic = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    Village = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientExtract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientArtExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AgeARTStart = table.Column<decimal>(nullable: true),
                    AgeEnrollment = table.Column<decimal>(nullable: true),
                    AgeLastVisit = table.Column<decimal>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<decimal>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    ExpectedReturn = table.Column<DateTime>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    LastARTDate = table.Column<DateTime>(nullable: true),
                    LastRegimen = table.Column<string>(nullable: true),
                    LastRegimenLine = table.Column<string>(nullable: true),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientSource = table.Column<string>(nullable: true),
                    PreviousARTRegimen = table.Column<string>(nullable: true),
                    PreviousARTStartDate = table.Column<DateTime>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    StartARTAtThisFacility = table.Column<DateTime>(nullable: true),
                    StartARTDate = table.Column<DateTime>(nullable: true),
                    StartRegimen = table.Column<string>(nullable: true),
                    StartRegimenLine = table.Column<string>(nullable: true)
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
                    CheckError = table.Column<bool>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    bCD4 = table.Column<int>(nullable: true),
                    bCD4Date = table.Column<DateTime>(nullable: true),
                    bWAB = table.Column<int>(nullable: true),
                    bWABDate = table.Column<DateTime>(nullable: true),
                    bWHO = table.Column<int>(nullable: true),
                    bWHODate = table.Column<DateTime>(nullable: true),
                    eCD4 = table.Column<int>(nullable: true),
                    eCD4Date = table.Column<DateTime>(nullable: true),
                    eWAB = table.Column<int>(nullable: true),
                    eWABDate = table.Column<DateTime>(nullable: true),
                    eWHO = table.Column<int>(nullable: true),
                    eWHODate = table.Column<DateTime>(nullable: true),
                    lastCD4 = table.Column<int>(nullable: true),
                    lastCD4Date = table.Column<DateTime>(nullable: true),
                    lastWAB = table.Column<int>(nullable: true),
                    lastWABDate = table.Column<DateTime>(nullable: true),
                    lastWHO = table.Column<int>(nullable: true),
                    lastWHODate = table.Column<DateTime>(nullable: true),
                    m12CD4 = table.Column<int>(nullable: true),
                    m12CD4Date = table.Column<DateTime>(nullable: true),
                    m6CD4 = table.Column<int>(nullable: true),
                    m6CD4Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientBaselinesExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    ContactRelation = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    DateConfirmedHIVPositive = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    DateLastVisit = table.Column<DateTime>(nullable: true),
                    DatePreviousARTStart = table.Column<DateTime>(nullable: true),
                    DateRegistered = table.Column<DateTime>(nullable: true),
                    DateRegistrationAtCCC = table.Column<DateTime>(nullable: true),
                    DateRegistrationAtPMTCT = table.Column<DateTime>(nullable: true),
                    DateRegistrationAtTBClinic = table.Column<DateTime>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    EducationLevel = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientSource = table.Column<string>(nullable: true),
                    PreviousARTExposure = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    StatusAtCCC = table.Column<string>(nullable: true),
                    StatusAtPMTCT = table.Column<string>(nullable: true),
                    StatusAtTBClinic = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientLaboratoryExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    EnrollmentTest = table.Column<int>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    OrderedByDate = table.Column<DateTime>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    ReportedByDate = table.Column<DateTime>(nullable: true),
                    SatelliteName = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    VisitId = table.Column<int>(nullable: true)
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
                    CheckError = table.Column<bool>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    DispenseDate = table.Column<DateTime>(nullable: true),
                    Drug = table.Column<string>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    ExpectedReturn = table.Column<DateTime>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: true),
                    PeriodTaken = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    ProphylaxisType = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    RegimenLine = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    TreatmentType = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true)
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
                    CheckError = table.Column<bool>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    ExitDescription = table.Column<string>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true)
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
                    Adherence = table.Column<string>(nullable: true),
                    AdherenceCategory = table.Column<string>(nullable: true),
                    BP = table.Column<string>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    EDD = table.Column<DateTime>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    FamilyPlanningMethod = table.Column<string>(nullable: true),
                    GestationAge = table.Column<decimal>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    LMP = table.Column<DateTime>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    OI = table.Column<string>(nullable: true),
                    OIDate = table.Column<DateTime>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: true),
                    Pregnant = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    PwP = table.Column<string>(nullable: true),
                    SecondlineRegimenChangeDate = table.Column<DateTime>(nullable: true),
                    SecondlineRegimenChangeReason = table.Column<string>(nullable: true),
                    Service = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    SubstitutionFirstlineRegimenDate = table.Column<DateTime>(nullable: true),
                    SubstitutionFirstlineRegimenReason = table.Column<string>(nullable: true),
                    SubstitutionSecondlineRegimenDate = table.Column<DateTime>(nullable: true),
                    SubstitutionSecondlineRegimenReason = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    VisitId = table.Column<int>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    WABStage = table.Column<string>(nullable: true),
                    WHOStage = table.Column<int>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientVisitExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientArtExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AgeARTStart = table.Column<decimal>(nullable: true),
                    AgeEnrollment = table.Column<decimal>(nullable: true),
                    AgeLastVisit = table.Column<decimal>(nullable: true),
                    ClientPatientExtractId = table.Column<Guid>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    ExpectedReturn = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    LastARTDate = table.Column<DateTime>(nullable: true),
                    LastRegimen = table.Column<string>(nullable: true),
                    LastRegimenLine = table.Column<string>(nullable: true),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: false),
                    PatientSource = table.Column<string>(nullable: true),
                    PreviousARTRegimen = table.Column<string>(nullable: true),
                    PreviousARTStartDate = table.Column<DateTime>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    StartARTAtThisFacility = table.Column<DateTime>(nullable: true),
                    StartARTDate = table.Column<DateTime>(nullable: true),
                    StartRegimen = table.Column<string>(nullable: true),
                    StartRegimenLine = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientArtExtract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientArtExtract_PatientExtract_ClientPatientExtractId",
                        column: x => x.ClientPatientExtractId,
                        principalTable: "PatientExtract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientBaselinesExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientPatientExtractId = table.Column<Guid>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: false),
                    Processed = table.Column<bool>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    bCD4 = table.Column<int>(nullable: true),
                    bCD4Date = table.Column<DateTime>(nullable: true),
                    bWAB = table.Column<int>(nullable: true),
                    bWABDate = table.Column<DateTime>(nullable: true),
                    bWHO = table.Column<int>(nullable: true),
                    bWHODate = table.Column<DateTime>(nullable: true),
                    eCD4 = table.Column<int>(nullable: true),
                    eCD4Date = table.Column<DateTime>(nullable: true),
                    eWAB = table.Column<int>(nullable: true),
                    eWABDate = table.Column<DateTime>(nullable: true),
                    eWHO = table.Column<int>(nullable: true),
                    eWHODate = table.Column<DateTime>(nullable: true),
                    lastCD4 = table.Column<int>(nullable: true),
                    lastCD4Date = table.Column<DateTime>(nullable: true),
                    lastWAB = table.Column<int>(nullable: true),
                    lastWABDate = table.Column<DateTime>(nullable: true),
                    lastWHO = table.Column<int>(nullable: true),
                    lastWHODate = table.Column<DateTime>(nullable: true),
                    m12CD4 = table.Column<int>(nullable: true),
                    m12CD4Date = table.Column<DateTime>(nullable: true),
                    m6CD4 = table.Column<int>(nullable: true),
                    m6CD4Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientBaselinesExtract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientBaselinesExtract_PatientExtract_ClientPatientExtractId",
                        column: x => x.ClientPatientExtractId,
                        principalTable: "PatientExtract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientLaboratoryExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientPatientExtractId = table.Column<Guid>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    EnrollmentTest = table.Column<int>(nullable: true),
                    OrderedByDate = table.Column<DateTime>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: false),
                    Processed = table.Column<bool>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    ReportedByDate = table.Column<DateTime>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    VisitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientLaboratoryExtract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientLaboratoryExtract_PatientExtract_ClientPatientExtractId",
                        column: x => x.ClientPatientExtractId,
                        principalTable: "PatientExtract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientPharmacyExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientPatientExtractId = table.Column<Guid>(nullable: true),
                    DispenseDate = table.Column<DateTime>(nullable: true),
                    Drug = table.Column<string>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    ExpectedReturn = table.Column<DateTime>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: false),
                    PeriodTaken = table.Column<string>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    ProphylaxisType = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    RegimenLine = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    TreatmentType = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPharmacyExtract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPharmacyExtract_PatientExtract_ClientPatientExtractId",
                        column: x => x.ClientPatientExtractId,
                        principalTable: "PatientExtract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientStatusExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientPatientExtractId = table.Column<Guid>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    ExitDescription = table.Column<string>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: false),
                    Processed = table.Column<bool>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientStatusExtract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientStatusExtract_PatientExtract_ClientPatientExtractId",
                        column: x => x.ClientPatientExtractId,
                        principalTable: "PatientExtract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientVisitExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Adherence = table.Column<string>(nullable: true),
                    AdherenceCategory = table.Column<string>(nullable: true),
                    BP = table.Column<string>(nullable: true),
                    ClientPatientExtractId = table.Column<Guid>(nullable: true),
                    EDD = table.Column<DateTime>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    FamilyPlanningMethod = table.Column<string>(nullable: true),
                    GestationAge = table.Column<decimal>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    LMP = table.Column<DateTime>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    OI = table.Column<string>(nullable: true),
                    OIDate = table.Column<DateTime>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: false),
                    Pregnant = table.Column<string>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    PwP = table.Column<string>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    SecondlineRegimenChangeDate = table.Column<DateTime>(nullable: true),
                    SecondlineRegimenChangeReason = table.Column<string>(nullable: true),
                    Service = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    SubstitutionFirstlineRegimenDate = table.Column<DateTime>(nullable: true),
                    SubstitutionFirstlineRegimenReason = table.Column<string>(nullable: true),
                    SubstitutionSecondlineRegimenDate = table.Column<DateTime>(nullable: true),
                    SubstitutionSecondlineRegimenReason = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    VisitId = table.Column<int>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    WABStage = table.Column<string>(nullable: true),
                    WHOStage = table.Column<int>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVisitExtract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientVisitExtract_PatientExtract_ClientPatientExtractId",
                        column: x => x.ClientPatientExtractId,
                        principalTable: "PatientExtract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EMR",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConnectionKey = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EMR_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtractSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Destination = table.Column<string>(nullable: true),
                    Display = table.Column<string>(nullable: true),
                    EmrId = table.Column<Guid>(nullable: false),
                    ExtractCsv = table.Column<string>(nullable: true),
                    ExtractSql = table.Column<string>(maxLength: 8000, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsPriority = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Rank = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtractSettings_EMR_EmrId",
                        column: x => x.EmrId,
                        principalTable: "EMR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Display = table.Column<string>(nullable: true),
                    ExtractSettingId = table.Column<Guid>(nullable: false),
                    Found = table.Column<int>(nullable: true),
                    FoundDate = table.Column<DateTime>(nullable: true),
                    FoundStatus = table.Column<string>(nullable: true),
                    ImportDate = table.Column<DateTime>(nullable: true),
                    ImportStatus = table.Column<string>(nullable: true),
                    Imported = table.Column<int>(nullable: true),
                    IsFoundSuccess = table.Column<bool>(nullable: true),
                    IsImportSuccess = table.Column<bool>(nullable: true),
                    IsLoadSuccess = table.Column<bool>(nullable: true),
                    IsSendSuccess = table.Column<bool>(nullable: true),
                    LoadDate = table.Column<DateTime>(nullable: true),
                    LoadStatus = table.Column<string>(nullable: true),
                    Loaded = table.Column<int>(nullable: true),
                    NotImported = table.Column<int>(nullable: true),
                    NotSent = table.Column<int>(nullable: true),
                    Rejected = table.Column<int>(nullable: true),
                    SendDate = table.Column<DateTime>(nullable: true),
                    SendStatus = table.Column<string>(nullable: true),
                    Sent = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventHistory_ExtractSettings_ExtractSettingId",
                        column: x => x.ExtractSettingId,
                        principalTable: "ExtractSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMR_ProjectId",
                table: "EMR",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EventHistory_ExtractSettingId",
                table: "EventHistory",
                column: "ExtractSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractSettings_EmrId",
                table: "ExtractSettings",
                column: "EmrId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientArtExtract_ClientPatientExtractId",
                table: "PatientArtExtract",
                column: "ClientPatientExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientBaselinesExtract_ClientPatientExtractId",
                table: "PatientBaselinesExtract",
                column: "ClientPatientExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientLaboratoryExtract_ClientPatientExtractId",
                table: "PatientLaboratoryExtract",
                column: "ClientPatientExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPharmacyExtract_ClientPatientExtractId",
                table: "PatientPharmacyExtract",
                column: "ClientPatientExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientStatusExtract_ClientPatientExtractId",
                table: "PatientStatusExtract",
                column: "ClientPatientExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVisitExtract_ClientPatientExtractId",
                table: "PatientVisitExtract",
                column: "ClientPatientExtractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventHistory");

            migrationBuilder.DropTable(
                name: "PatientArtExtract");

            migrationBuilder.DropTable(
                name: "PatientBaselinesExtract");

            migrationBuilder.DropTable(
                name: "PatientLaboratoryExtract");

            migrationBuilder.DropTable(
                name: "PatientPharmacyExtract");

            migrationBuilder.DropTable(
                name: "PatientStatusExtract");

            migrationBuilder.DropTable(
                name: "PatientVisitExtract");

            migrationBuilder.DropTable(
                name: "TempPatientArtExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientBaselinesExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientPharmacyExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientStatusExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientVisitExtracts");

            migrationBuilder.DropTable(
                name: "ExtractSettings");

            migrationBuilder.DropTable(
                name: "PatientExtract");

            migrationBuilder.DropTable(
                name: "EMR");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
