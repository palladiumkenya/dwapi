using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ExtractsInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtractHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    Stats = table.Column<int>(nullable: true),
                    StatusInfo = table.Column<string>(nullable: true),
                    ExtractId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterPatientIndices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPk = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    Serial = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FirstName_Normalized = table.Column<string>(nullable: true),
                    MiddleName_Normalized = table.Column<string>(nullable: true),
                    LastName_Normalized = table.Column<string>(nullable: true),
                    PatientPhoneNumber = table.Column<string>(nullable: true),
                    PatientAlternatePhoneNumber = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    PatientSource = table.Column<string>(nullable: true),
                    PatientCounty = table.Column<string>(nullable: true),
                    PatientSubCounty = table.Column<string>(nullable: true),
                    PatientVillage = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    National_ID = table.Column<string>(nullable: true),
                    NHIF_Number = table.Column<string>(nullable: true),
                    Birth_Certificate = table.Column<string>(nullable: true),
                    CCC_Number = table.Column<string>(nullable: true),
                    TB_Number = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactRelation = table.Column<string>(nullable: true),
                    ContactPhoneNumber = table.Column<string>(nullable: true),
                    ContactAddress = table.Column<string>(nullable: true),
                    DateConfirmedHIVPositive = table.Column<DateTime>(nullable: true),
                    StartARTDate = table.Column<DateTime>(nullable: true),
                    StartARTRegimenCode = table.Column<string>(nullable: true),
                    StartARTRegimenDesc = table.Column<string>(nullable: true),
                    dmFirstName = table.Column<string>(nullable: true),
                    dmLastName = table.Column<string>(nullable: true),
                    sxFirstName = table.Column<string>(nullable: true),
                    sxLastName = table.Column<string>(nullable: true),
                    sxPKValue = table.Column<string>(nullable: true),
                    dmPKValue = table.Column<string>(nullable: true),
                    sxdmPKValue = table.Column<string>(nullable: true),
                    sxMiddleName = table.Column<string>(nullable: true),
                    dmMiddleName = table.Column<string>(nullable: true),
                    sxPKValueDoB = table.Column<string>(nullable: true),
                    dmPKValueDoB = table.Column<string>(nullable: true),
                    sxdmPKValueDoB = table.Column<string>(nullable: true),
                    JaroWinklerScore = table.Column<double>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPatientIndices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientArtExtracts",
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
                    m6CD4Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientBaselinesExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientExtracts",
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
                    Gender = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
                    RegistrationAtCCC = table.Column<DateTime>(nullable: true),
                    RegistrationATPMTCT = table.Column<DateTime>(nullable: true),
                    RegistrationAtTBClinic = table.Column<DateTime>(nullable: true),
                    PatientSource = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    ContactRelation = table.Column<string>(nullable: true),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    EducationLevel = table.Column<string>(nullable: true),
                    DateConfirmedHIVPositive = table.Column<DateTime>(nullable: true),
                    PreviousARTExposure = table.Column<string>(nullable: true),
                    DatePreviousARTStart = table.Column<DateTime>(nullable: true),
                    StatusAtCCC = table.Column<string>(nullable: true),
                    StatusAtPMTCT = table.Column<string>(nullable: true),
                    StatusAtTBClinic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientLaboratoryExtracts",
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
                    SatelliteName = table.Column<string>(nullable: true),
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
                    VisitID = table.Column<int>(nullable: true),
                    Drug = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    DispenseDate = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true),
                    ExpectedReturn = table.Column<DateTime>(nullable: true),
                    TreatmentType = table.Column<string>(nullable: true),
                    RegimenLine = table.Column<string>(nullable: true),
                    PeriodTaken = table.Column<string>(nullable: true),
                    ProphylaxisType = table.Column<string>(nullable: true)
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
                name: "PsmartStage",
                columns: table => new
                {
                    EId = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: true),
                    Shr = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(maxLength: 100, nullable: true),
                    Status_Date = table.Column<DateTime>(nullable: true),
                    Uuid = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    DateStaged = table.Column<DateTime>(nullable: false),
                    DateSent = table.Column<DateTime>(nullable: true),
                    RequestId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsmartStage", x => x.EId);
                });

            migrationBuilder.CreateTable(
                name: "TempMasterPatientIndices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPk = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    Serial = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FirstName_Normalized = table.Column<string>(nullable: true),
                    MiddleName_Normalized = table.Column<string>(nullable: true),
                    LastName_Normalized = table.Column<string>(nullable: true),
                    PatientPhoneNumber = table.Column<string>(nullable: true),
                    PatientAlternatePhoneNumber = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    PatientSource = table.Column<string>(nullable: true),
                    PatientCounty = table.Column<string>(nullable: true),
                    PatientSubCounty = table.Column<string>(nullable: true),
                    PatientVillage = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    National_ID = table.Column<string>(nullable: true),
                    NHIF_Number = table.Column<string>(nullable: true),
                    Birth_Certificate = table.Column<string>(nullable: true),
                    CCC_Number = table.Column<string>(nullable: true),
                    TB_Number = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactRelation = table.Column<string>(nullable: true),
                    ContactPhoneNumber = table.Column<string>(nullable: true),
                    ContactAddress = table.Column<string>(nullable: true),
                    DateConfirmedHIVPositive = table.Column<DateTime>(nullable: true),
                    StartARTDate = table.Column<DateTime>(nullable: true),
                    StartARTRegimenCode = table.Column<string>(nullable: true),
                    StartARTRegimenDesc = table.Column<string>(nullable: true),
                    dmFirstName = table.Column<string>(nullable: true),
                    dmLastName = table.Column<string>(nullable: true),
                    sxFirstName = table.Column<string>(nullable: true),
                    sxLastName = table.Column<string>(nullable: true),
                    sxPKValue = table.Column<string>(nullable: true),
                    dmPKValue = table.Column<string>(nullable: true),
                    sxdmPKValue = table.Column<string>(nullable: true),
                    sxMiddleName = table.Column<string>(nullable: true),
                    dmMiddleName = table.Column<string>(nullable: true),
                    sxPKValueDoB = table.Column<string>(nullable: true),
                    dmPKValueDoB = table.Column<string>(nullable: true),
                    sxdmPKValueDoB = table.Column<string>(nullable: true),
                    JaroWinklerScore = table.Column<double>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMasterPatientIndices", x => x.Id);
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
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
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
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
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
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
                    RegistrationAtCCC = table.Column<DateTime>(nullable: true),
                    RegistrationAtPMTCT = table.Column<DateTime>(nullable: true),
                    RegistrationAtTBClinic = table.Column<DateTime>(nullable: true),
                    PatientSource = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    ContactRelation = table.Column<string>(nullable: true),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    EducationLevel = table.Column<string>(nullable: true),
                    DateConfirmedHIVPositive = table.Column<DateTime>(nullable: true),
                    PreviousARTExposure = table.Column<string>(nullable: true),
                    PreviousARTStartDate = table.Column<DateTime>(nullable: true),
                    StatusAtCCC = table.Column<string>(nullable: true),
                    StatusAtPMTCT = table.Column<string>(nullable: true),
                    StatusAtTBClinic = table.Column<string>(nullable: true),
                    SatelliteName = table.Column<string>(nullable: true)
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
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    SatelliteName = table.Column<string>(nullable: true),
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
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
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
                    ProphylaxisType = table.Column<string>(nullable: true)
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
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
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
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Validator",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Extract = table.Column<string>(nullable: true),
                    Field = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Logic = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValidationError",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ValidatorId = table.Column<Guid>(nullable: false),
                    RecordId = table.Column<Guid>(nullable: false),
                    DateGenerated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationError", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValidationError_Validator_ValidatorId",
                        column: x => x.ValidatorId,
                        principalTable: "Validator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ValidationError_ValidatorId",
                table: "ValidationError",
                column: "ValidatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtractHistory");

            migrationBuilder.DropTable(
                name: "MasterPatientIndices");

            migrationBuilder.DropTable(
                name: "PatientArtExtracts");

            migrationBuilder.DropTable(
                name: "PatientBaselinesExtracts");

            migrationBuilder.DropTable(
                name: "PatientExtracts");

            migrationBuilder.DropTable(
                name: "PatientLaboratoryExtracts");

            migrationBuilder.DropTable(
                name: "PatientPharmacyExtracts");

            migrationBuilder.DropTable(
                name: "PatientStatusExtracts");

            migrationBuilder.DropTable(
                name: "PatientVisitExtracts");

            migrationBuilder.DropTable(
                name: "PsmartStage");

            migrationBuilder.DropTable(
                name: "TempMasterPatientIndices");

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
                name: "ValidationError");

            migrationBuilder.DropTable(
                name: "Validator");
        }
    }
}
