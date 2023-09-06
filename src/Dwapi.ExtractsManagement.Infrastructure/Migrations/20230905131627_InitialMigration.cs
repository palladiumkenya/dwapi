using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientRegistryExtracts",
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
                    CCCNumber = table.Column<string>(nullable: true),
                    NationalId = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    HudumaNumber = table.Column<string>(nullable: true),
                    BirthCertificateNumber = table.Column<string>(nullable: true),
                    AlienIdNo = table.Column<string>(nullable: true),
                    DrivingLicenseNumber = table.Column<string>(nullable: true),
                    PatientClinicNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    HighestLevelOfEducation = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    AlternativePhoneNumber = table.Column<string>(nullable: true),
                    SpousePhoneNumber = table.Column<string>(nullable: true),
                    NameOfNextOfKin = table.Column<string>(nullable: true),
                    NextOfKinRelationship = table.Column<string>(nullable: true),
                    NextOfKinTelNo = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    SubCounty = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    Landmark = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    MFLCode = table.Column<string>(nullable: true),
                    DateOfInitiation = table.Column<DateTime>(nullable: true),
                    TreatmentOutcome = table.Column<string>(nullable: true),
                    DateOfLastEncounter = table.Column<DateTime>(nullable: true),
                    DateOfLastViralLoad = table.Column<DateTime>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    LastRegimen = table.Column<string>(nullable: true),
                    LastRegimenLine = table.Column<string>(nullable: true),
                    CurrentOnART = table.Column<string>(nullable: true),
                    DateOfHIVdiagnosis = table.Column<DateTime>(nullable: true),
                    LastViralLoadResult = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRegistryExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiffLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Docket = table.Column<string>(nullable: true),
                    Extract = table.Column<string>(nullable: true),
                    LastCreated = table.Column<DateTime>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    MaxCreated = table.Column<DateTime>(nullable: true),
                    MaxModified = table.Column<DateTime>(nullable: true),
                    LastSent = table.Column<DateTime>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    ChangesLoaded = table.Column<bool>(nullable: false),
                    ExtractsSent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiffLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmrMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmrName = table.Column<string>(nullable: true),
                    EmrVersion = table.Column<string>(nullable: true),
                    LastLoginDate = table.Column<DateTime>(nullable: true),
                    LastMoH731RunDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrMetrics", x => x.Id);
                });

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
                name: "HtsClientExtracts",
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
                    EncounterId = table.Column<int>(nullable: false),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    Dob = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    KeyPop = table.Column<string>(nullable: true),
                    TestedBefore = table.Column<string>(nullable: true),
                    MonthsLastTested = table.Column<int>(nullable: true),
                    ClientTestedAs = table.Column<string>(nullable: true),
                    StrategyHTS = table.Column<string>(nullable: true),
                    TestKitName1 = table.Column<string>(nullable: true),
                    TestKitLotNumber1 = table.Column<string>(nullable: true),
                    TestKitExpiryDate1 = table.Column<DateTime>(nullable: true),
                    TestResultsHTS1 = table.Column<string>(nullable: true),
                    TestKitName2 = table.Column<string>(nullable: true),
                    TestKitLotNumber2 = table.Column<string>(nullable: true),
                    TestKitExpiryDate2 = table.Column<string>(nullable: true),
                    TestResultsHTS2 = table.Column<string>(nullable: true),
                    FinalResultHTS = table.Column<string>(nullable: true),
                    FinalResultsGiven = table.Column<string>(nullable: true),
                    TBScreeningHTS = table.Column<string>(nullable: true),
                    ClientSelfTested = table.Column<string>(nullable: true),
                    CoupleDiscordant = table.Column<string>(nullable: true),
                    TestType = table.Column<string>(nullable: true),
                    KeyPopulationType = table.Column<string>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    PatientDisabled = table.Column<string>(nullable: true),
                    DisabilityType = table.Column<string>(nullable: true),
                    PatientConsented = table.Column<string>(nullable: true),
                    NUPI = table.Column<string>(nullable: true),
                    Pkv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HtsClientLinkageExtracts",
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
                    PhoneTracingDate = table.Column<DateTime>(nullable: true),
                    PhysicalTracingDate = table.Column<DateTime>(nullable: true),
                    TracingOutcome = table.Column<string>(nullable: true),
                    CccNumber = table.Column<string>(nullable: true),
                    EnrolledFacilityName = table.Column<string>(nullable: true),
                    ReferralDate = table.Column<DateTime>(nullable: false),
                    DateEnrolled = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientLinkageExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HtsClientPartnerExtracts",
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
                    PartnerPatientPk = table.Column<int>(nullable: true),
                    PartnerPersonId = table.Column<int>(nullable: true),
                    RelationshipToIndexClient = table.Column<string>(nullable: true),
                    ScreenedForIpv = table.Column<string>(nullable: true),
                    IpvScreeningOutcome = table.Column<string>(nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(nullable: true),
                    PnsApproach = table.Column<string>(nullable: true),
                    Trace1Outcome = table.Column<string>(nullable: true),
                    Trace1Type = table.Column<string>(nullable: true),
                    Trace1Date = table.Column<DateTime>(nullable: true),
                    Trace2Outcome = table.Column<string>(nullable: true),
                    Trace2Type = table.Column<string>(nullable: true),
                    Trace2Date = table.Column<DateTime>(nullable: true),
                    Trace3Outcome = table.Column<string>(nullable: true),
                    Trace3Type = table.Column<string>(nullable: true),
                    Trace3Date = table.Column<DateTime>(nullable: true),
                    PnsConsent = table.Column<string>(nullable: true),
                    Linked = table.Column<string>(nullable: true),
                    LinkDateLinkedToCare = table.Column<DateTime>(nullable: true),
                    CccNumber = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Sex = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientPartnerExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HtsClientsExtracts",
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
                    DoB = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    KeyPopulationType = table.Column<string>(nullable: true),
                    PatientDisabled = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    SubCounty = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true),
                    NUPI = table.Column<string>(nullable: true),
                    Pkv = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    PriorityPopulationType = table.Column<string>(nullable: true),
                    HtsRecencyId = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientsExtracts", x => new { x.SiteCode, x.PatientPk });
                    table.UniqueConstraint("AK_HtsClientsExtracts_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Indicator = table.Column<string>(nullable: true),
                    IndicatorValue = table.Column<string>(nullable: true),
                    IndicatorDate = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorExtracts", x => x.Id);
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
                name: "MetricMigrationExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    MetricId = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    HtsNumber = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    Dataset = table.Column<string>(nullable: true),
                    Metric = table.Column<string>(nullable: true),
                    MetricValue = table.Column<string>(nullable: true),
                    Stage = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricMigrationExtracts", x => x.Id);
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
                    StatusAtTBClinic = table.Column<string>(nullable: true),
                    Orphan = table.Column<string>(nullable: true),
                    Inschool = table.Column<string>(nullable: true),
                    PatientType = table.Column<string>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    KeyPopulationType = table.Column<string>(nullable: true),
                    PatientResidentCounty = table.Column<string>(nullable: true),
                    PatientResidentSubCounty = table.Column<string>(nullable: true),
                    PatientResidentLocation = table.Column<string>(nullable: true),
                    PatientResidentSubLocation = table.Column<string>(nullable: true),
                    PatientResidentWard = table.Column<string>(nullable: true),
                    PatientResidentVillage = table.Column<string>(nullable: true),
                    TransferInDate = table.Column<DateTime>(nullable: true),
                    Pkv = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    NUPI = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientExtracts", x => new { x.SiteCode, x.PatientPK });
                    table.UniqueConstraint("AK_PatientExtracts_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientMnchExtracts",
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
                    Pkv = table.Column<string>(nullable: true),
                    PatientMnchID = table.Column<string>(nullable: true),
                    PatientHeiID = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    FirstEnrollmentAtMnch = table.Column<DateTime>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    EducationLevel = table.Column<string>(nullable: true),
                    PatientResidentCounty = table.Column<string>(nullable: true),
                    PatientResidentSubCounty = table.Column<string>(nullable: true),
                    PatientResidentWard = table.Column<string>(nullable: true),
                    InSchool = table.Column<string>(nullable: true),
                    NUPI = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientMnchExtracts", x => new { x.SiteCode, x.PatientPK });
                    table.UniqueConstraint("AK_PatientMnchExtracts_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientPrepExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    PrepEnrollmentDate = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: true),
                    CountyofBirth = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    SubCounty = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    LandMark = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true),
                    ClientType = table.Column<string>(nullable: true),
                    ReferralPoint = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Inschool = table.Column<string>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    KeyPopulationType = table.Column<string>(nullable: true),
                    Refferedfrom = table.Column<string>(nullable: true),
                    TransferIn = table.Column<string>(nullable: true),
                    TransferInDate = table.Column<DateTime>(nullable: true),
                    TransferFromFacility = table.Column<string>(nullable: true),
                    DatefirstinitiatedinPrepCare = table.Column<DateTime>(nullable: true),
                    DateStartedPrEPattransferringfacility = table.Column<DateTime>(nullable: true),
                    ClientPreviouslyonPrep = table.Column<string>(nullable: true),
                    PrevPrepReg = table.Column<string>(nullable: true),
                    DateLastUsedPrev = table.Column<DateTime>(nullable: true),
                    NUPI = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPrepExtracts", x => new { x.SiteCode, x.PatientPK });
                    table.UniqueConstraint("AK_PatientPrepExtracts_Id", x => x.Id);
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
                name: "TempAllergiesChronicIllnessExtracts",
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
                    ChronicIllness = table.Column<string>(nullable: true),
                    ChronicOnsetDate = table.Column<DateTime>(nullable: true),
                    knownAllergies = table.Column<string>(nullable: true),
                    AllergyCausativeAgent = table.Column<string>(nullable: true),
                    AllergicReaction = table.Column<string>(nullable: true),
                    AllergySeverity = table.Column<string>(nullable: true),
                    AllergyOnsetDate = table.Column<DateTime>(nullable: true),
                    Skin = table.Column<string>(nullable: true),
                    Eyes = table.Column<string>(nullable: true),
                    ENT = table.Column<string>(nullable: true),
                    Chest = table.Column<string>(nullable: true),
                    CVS = table.Column<string>(nullable: true),
                    Abdomen = table.Column<string>(nullable: true),
                    CNS = table.Column<string>(nullable: true),
                    Genitourinary = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempAllergiesChronicIllnessExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempAncVisitExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    ANCClinicNumber = table.Column<string>(nullable: true),
                    ANCVisitNo = table.Column<int>(nullable: true),
                    GestationWeeks = table.Column<int>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true),
                    Temp = table.Column<decimal>(nullable: true),
                    PulseRate = table.Column<int>(nullable: true),
                    RespiratoryRate = table.Column<int>(nullable: true),
                    OxygenSaturation = table.Column<decimal>(nullable: true),
                    MUAC = table.Column<int>(nullable: true),
                    BP = table.Column<string>(nullable: true),
                    BreastExam = table.Column<string>(nullable: true),
                    AntenatalExercises = table.Column<string>(nullable: true),
                    FGM = table.Column<string>(nullable: true),
                    FGMComplications = table.Column<string>(nullable: true),
                    Haemoglobin = table.Column<decimal>(nullable: true),
                    DiabetesTest = table.Column<string>(nullable: true),
                    TBScreening = table.Column<string>(nullable: true),
                    CACxScreen = table.Column<string>(nullable: true),
                    CACxScreenMethod = table.Column<string>(nullable: true),
                    WHOStaging = table.Column<int>(nullable: true),
                    VLSampleTaken = table.Column<string>(nullable: true),
                    VLDate = table.Column<DateTime>(nullable: true),
                    VLResult = table.Column<string>(nullable: true),
                    SyphilisTreatment = table.Column<string>(nullable: true),
                    HIVStatusBeforeANC = table.Column<string>(nullable: true),
                    HIVTestingDone = table.Column<string>(nullable: true),
                    HIVTestType = table.Column<string>(nullable: true),
                    HIVTest1 = table.Column<string>(nullable: true),
                    HIVTest1Result = table.Column<string>(nullable: true),
                    HIVTest2 = table.Column<string>(nullable: true),
                    HIVTest2Result = table.Column<string>(nullable: true),
                    HIVTestFinalResult = table.Column<string>(nullable: true),
                    SyphilisTestDone = table.Column<string>(nullable: true),
                    SyphilisTestType = table.Column<string>(nullable: true),
                    SyphilisTestResults = table.Column<string>(nullable: true),
                    SyphilisTreated = table.Column<string>(nullable: true),
                    MotherProphylaxisGiven = table.Column<string>(nullable: true),
                    MotherGivenHAART = table.Column<DateTime>(nullable: true),
                    AZTBabyDispense = table.Column<string>(nullable: true),
                    NVPBabyDispense = table.Column<string>(nullable: true),
                    ChronicIllness = table.Column<string>(nullable: true),
                    CounselledOn = table.Column<string>(nullable: true),
                    PartnerHIVTestingANC = table.Column<string>(nullable: true),
                    PartnerHIVStatusANC = table.Column<string>(nullable: true),
                    PostParturmFP = table.Column<string>(nullable: true),
                    Deworming = table.Column<string>(nullable: true),
                    MalariaProphylaxis = table.Column<string>(nullable: true),
                    TetanusDose = table.Column<string>(nullable: true),
                    IronSupplementsGiven = table.Column<string>(nullable: true),
                    ReceivedMosquitoNet = table.Column<string>(nullable: true),
                    PreventiveServices = table.Column<string>(nullable: true),
                    UrinalysisVariables = table.Column<string>(nullable: true),
                    ReferredFrom = table.Column<string>(nullable: true),
                    ReferredTo = table.Column<string>(nullable: true),
                    ReferralReasons = table.Column<string>(nullable: true),
                    NextAppointmentANC = table.Column<DateTime>(nullable: true),
                    ClinicalNotes = table.Column<string>(nullable: true),
                    HepatitisBScreening = table.Column<string>(nullable: true),
                    TreatedHepatitisB = table.Column<string>(nullable: true),
                    PresumptiveTreatmentGiven = table.Column<string>(nullable: true),
                    PresumptiveTreatmentDose = table.Column<string>(nullable: true),
                    MiminumPackageOfCareReceived = table.Column<string>(nullable: true),
                    MiminumPackageOfCareServices = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempAncVisitExtracts", x => x.Id);
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
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCervicalCancerScreeningExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempClientRegistryExtracts",
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
                    CCCNumber = table.Column<string>(nullable: true),
                    NationalId = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    HudumaNumber = table.Column<string>(nullable: true),
                    BirthCertificateNumber = table.Column<string>(nullable: true),
                    AlienIdNo = table.Column<string>(nullable: true),
                    DrivingLicenseNumber = table.Column<string>(nullable: true),
                    PatientClinicNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    HighestLevelOfEducation = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    AlternativePhoneNumber = table.Column<string>(nullable: true),
                    SpousePhoneNumber = table.Column<string>(nullable: true),
                    NameOfNextOfKin = table.Column<string>(nullable: true),
                    NextOfKinRelationship = table.Column<string>(nullable: true),
                    NextOfKinTelNo = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    SubCounty = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    Landmark = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    MFLCode = table.Column<string>(nullable: true),
                    DateOfInitiation = table.Column<DateTime>(nullable: true),
                    TreatmentOutcome = table.Column<string>(nullable: true),
                    DateOfLastEncounter = table.Column<DateTime>(nullable: true),
                    DateOfLastViralLoad = table.Column<DateTime>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    LastRegimen = table.Column<string>(nullable: true),
                    LastRegimenLine = table.Column<string>(nullable: true),
                    CurrentOnART = table.Column<string>(nullable: true),
                    DateOfHIVdiagnosis = table.Column<DateTime>(nullable: true),
                    LastViralLoadResult = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempClientRegistryExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempContactListingExtracts",
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
                    PartnerPersonID = table.Column<int>(nullable: true),
                    ContactAge = table.Column<string>(nullable: true),
                    ContactSex = table.Column<string>(nullable: true),
                    ContactMaritalStatus = table.Column<string>(nullable: true),
                    RelationshipWithPatient = table.Column<string>(nullable: true),
                    ScreenedForIpv = table.Column<string>(nullable: true),
                    IpvScreening = table.Column<string>(nullable: true),
                    IPVScreeningOutcome = table.Column<string>(nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(nullable: true),
                    PnsApproach = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    ContactPatientPK = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempContactListingExtracts", x => x.Id);
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
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    COVID19TestResult = table.Column<string>(nullable: true),
                    Sequence = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCovidExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempCwcEnrolmentExtracts",
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
                    Pkv = table.Column<string>(nullable: true),
                    PatientIDCWC = table.Column<string>(nullable: true),
                    HEIID = table.Column<string>(nullable: true),
                    MothersPkv = table.Column<string>(nullable: true),
                    RegistrationAtCWC = table.Column<DateTime>(nullable: true),
                    RegistrationAtHEI = table.Column<DateTime>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    Gestation = table.Column<DateTime>(nullable: true),
                    BirthWeight = table.Column<string>(nullable: true),
                    BirthLength = table.Column<decimal>(nullable: true),
                    BirthOrder = table.Column<int>(nullable: true),
                    BirthType = table.Column<string>(nullable: true),
                    PlaceOfDelivery = table.Column<string>(nullable: true),
                    ModeOfDelivery = table.Column<string>(nullable: true),
                    SpecialNeeds = table.Column<string>(nullable: true),
                    SpecialCare = table.Column<string>(nullable: true),
                    HEI = table.Column<string>(nullable: true),
                    MotherAlive = table.Column<string>(nullable: true),
                    MothersCCCNo = table.Column<string>(nullable: true),
                    TransferIn = table.Column<string>(nullable: true),
                    TransferInDate = table.Column<string>(nullable: true),
                    TransferredFrom = table.Column<string>(nullable: true),
                    HEIDate = table.Column<string>(nullable: true),
                    NVP = table.Column<string>(nullable: true),
                    BreastFeeding = table.Column<string>(nullable: true),
                    ReferredFrom = table.Column<string>(nullable: true),
                    ARTMother = table.Column<string>(nullable: true),
                    ARTRegimenMother = table.Column<string>(nullable: true),
                    ARTStartDateMother = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCwcEnrolmentExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempCwcVisitExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true),
                    Temp = table.Column<decimal>(nullable: true),
                    PulseRate = table.Column<int>(nullable: true),
                    RespiratoryRate = table.Column<int>(nullable: true),
                    OxygenSaturation = table.Column<decimal>(nullable: true),
                    MUAC = table.Column<int>(nullable: true),
                    WeightCategory = table.Column<string>(nullable: true),
                    Stunted = table.Column<string>(nullable: true),
                    InfantFeeding = table.Column<string>(nullable: true),
                    MedicationGiven = table.Column<string>(nullable: true),
                    TBAssessment = table.Column<string>(nullable: true),
                    MNPsSupplementation = table.Column<string>(nullable: true),
                    Immunization = table.Column<string>(nullable: true),
                    DangerSigns = table.Column<string>(nullable: true),
                    Milestones = table.Column<string>(nullable: true),
                    VitaminA = table.Column<string>(nullable: true),
                    Disability = table.Column<string>(nullable: true),
                    ReceivedMosquitoNet = table.Column<string>(nullable: true),
                    Dewormed = table.Column<string>(nullable: true),
                    ReferredFrom = table.Column<string>(nullable: true),
                    ReferredTo = table.Column<string>(nullable: true),
                    ReferralReasons = table.Column<string>(nullable: true),
                    FollowUP = table.Column<string>(nullable: true),
                    NextAppointment = table.Column<DateTime>(nullable: true),
                    RevisitThisYear = table.Column<string>(nullable: true),
                    Refferred = table.Column<string>(nullable: true),
                    HeightLength = table.Column<decimal>(nullable: true),
                    ZScore = table.Column<string>(nullable: true),
                    ZScoreAbsolute = table.Column<int>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCwcVisitExtracts", x => x.Id);
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
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempDefaulterTracingExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempDepressionScreeningExtracts",
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
                    PHQ9_1 = table.Column<string>(nullable: true),
                    PHQ9_2 = table.Column<string>(nullable: true),
                    PHQ9_3 = table.Column<string>(nullable: true),
                    PHQ9_4 = table.Column<string>(nullable: true),
                    PHQ9_5 = table.Column<string>(nullable: true),
                    PHQ9_6 = table.Column<string>(nullable: true),
                    PHQ9_7 = table.Column<string>(nullable: true),
                    PHQ9_8 = table.Column<string>(nullable: true),
                    PHQ9_9 = table.Column<string>(nullable: true),
                    PHQ_9_rating = table.Column<string>(nullable: true),
                    DepressionAssesmentScore = table.Column<int>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempDepressionScreeningExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempDrugAlcoholScreeningExtracts",
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
                    DrinkingAlcohol = table.Column<string>(nullable: true),
                    Smoking = table.Column<string>(nullable: true),
                    DrugUse = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempDrugAlcoholScreeningExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempEnhancedAdherenceCounsellingExtracts",
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
                    SessionNumber = table.Column<int>(nullable: true),
                    DateOfFirstSession = table.Column<DateTime>(nullable: true),
                    PillCountAdherence = table.Column<int>(nullable: true),
                    MMAS4_1 = table.Column<string>(nullable: true),
                    MMAS4_2 = table.Column<string>(nullable: true),
                    MMAS4_3 = table.Column<string>(nullable: true),
                    MMAS4_4 = table.Column<string>(nullable: true),
                    MMSA8_1 = table.Column<string>(nullable: true),
                    MMSA8_2 = table.Column<string>(nullable: true),
                    MMSA8_3 = table.Column<string>(nullable: true),
                    MMSA8_4 = table.Column<string>(nullable: true),
                    MMSAScore = table.Column<string>(nullable: true),
                    EACRecievedVL = table.Column<string>(nullable: true),
                    EACVL = table.Column<string>(nullable: true),
                    EACVLConcerns = table.Column<string>(nullable: true),
                    EACVLThoughts = table.Column<string>(nullable: true),
                    EACWayForward = table.Column<string>(nullable: true),
                    EACCognitiveBarrier = table.Column<string>(nullable: true),
                    EACBehaviouralBarrier_1 = table.Column<string>(nullable: true),
                    EACBehaviouralBarrier_2 = table.Column<string>(nullable: true),
                    EACBehaviouralBarrier_3 = table.Column<string>(nullable: true),
                    EACBehaviouralBarrier_4 = table.Column<string>(nullable: true),
                    EACBehaviouralBarrier_5 = table.Column<string>(nullable: true),
                    EACEmotionalBarriers_1 = table.Column<string>(nullable: true),
                    EACEmotionalBarriers_2 = table.Column<string>(nullable: true),
                    EACEconBarrier_1 = table.Column<string>(nullable: true),
                    EACEconBarrier_2 = table.Column<string>(nullable: true),
                    EACEconBarrier_3 = table.Column<string>(nullable: true),
                    EACEconBarrier_4 = table.Column<string>(nullable: true),
                    EACEconBarrier_5 = table.Column<string>(nullable: true),
                    EACEconBarrier_6 = table.Column<string>(nullable: true),
                    EACEconBarrier_7 = table.Column<string>(nullable: true),
                    EACEconBarrier_8 = table.Column<string>(nullable: true),
                    EACReviewImprovement = table.Column<string>(nullable: true),
                    EACReviewMissedDoses = table.Column<string>(nullable: true),
                    EACReviewStrategy = table.Column<string>(nullable: true),
                    EACReferral = table.Column<string>(nullable: true),
                    EACReferralApp = table.Column<string>(nullable: true),
                    EACReferralExperience = table.Column<string>(nullable: true),
                    EACHomevisit = table.Column<string>(nullable: true),
                    EACAdherencePlan = table.Column<string>(nullable: true),
                    EACFollowupDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempEnhancedAdherenceCounsellingExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempGbvScreeningExtracts",
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
                    IPV = table.Column<string>(nullable: true),
                    PhysicalIPV = table.Column<string>(nullable: true),
                    EmotionalIPV = table.Column<string>(nullable: true),
                    SexualIPV = table.Column<string>(nullable: true),
                    IPVRelationship = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempGbvScreeningExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHeiExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    DNAPCR1Date = table.Column<DateTime>(nullable: true),
                    DNAPCR2Date = table.Column<DateTime>(nullable: true),
                    DNAPCR3Date = table.Column<DateTime>(nullable: true),
                    ConfirmatoryPCRDate = table.Column<DateTime>(nullable: true),
                    BasellineVLDate = table.Column<DateTime>(nullable: true),
                    FinalyAntibodyDate = table.Column<DateTime>(nullable: true),
                    DNAPCR1 = table.Column<string>(nullable: true),
                    DNAPCR2 = table.Column<string>(nullable: true),
                    DNAPCR3 = table.Column<string>(nullable: true),
                    ConfirmatoryPCR = table.Column<string>(nullable: true),
                    BasellineVL = table.Column<string>(nullable: true),
                    FinalyAntibody = table.Column<string>(nullable: true),
                    HEIExitDate = table.Column<DateTime>(nullable: true),
                    HEIHIVStatus = table.Column<string>(nullable: true),
                    HEIExitCritearia = table.Column<string>(nullable: true),
                    PatientHeiId = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHeiExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsClientExtracts",
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
                    EncounterId = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    Dob = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    KeyPop = table.Column<string>(nullable: true),
                    TestedBefore = table.Column<string>(nullable: true),
                    MonthsLastTested = table.Column<int>(nullable: true),
                    ClientTestedAs = table.Column<string>(nullable: true),
                    StrategyHTS = table.Column<string>(nullable: true),
                    TestKitName1 = table.Column<string>(nullable: true),
                    TestKitLotNumber1 = table.Column<string>(nullable: true),
                    TestKitExpiryDate1 = table.Column<DateTime>(nullable: true),
                    TestResultsHTS1 = table.Column<string>(nullable: true),
                    TestKitName2 = table.Column<string>(nullable: true),
                    TestKitLotNumber2 = table.Column<string>(nullable: true),
                    TestKitExpiryDate2 = table.Column<string>(nullable: true),
                    TestResultsHTS2 = table.Column<string>(nullable: true),
                    FinalResultHTS = table.Column<string>(nullable: true),
                    FinalResultsGiven = table.Column<string>(nullable: true),
                    TBScreeningHTS = table.Column<string>(nullable: true),
                    ClientSelfTested = table.Column<string>(nullable: true),
                    CoupleDiscordant = table.Column<string>(nullable: true),
                    TestType = table.Column<string>(nullable: true),
                    KeyPopulationType = table.Column<string>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    PatientDisabled = table.Column<string>(nullable: true),
                    DisabilityType = table.Column<string>(nullable: true),
                    PatientConsented = table.Column<string>(nullable: true),
                    NUPI = table.Column<string>(nullable: true),
                    Pkv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsClientExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsClientLinkageExtracts",
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
                    PhoneTracingDate = table.Column<DateTime>(nullable: true),
                    PhysicalTracingDate = table.Column<DateTime>(nullable: true),
                    TracingOutcome = table.Column<string>(nullable: true),
                    CccNumber = table.Column<string>(nullable: true),
                    ReferralDate = table.Column<DateTime>(nullable: true),
                    DateEnrolled = table.Column<DateTime>(nullable: true),
                    EnrolledFacilityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsClientLinkageExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsClientPartnerExtracts",
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
                    PartnerPatientPk = table.Column<int>(nullable: true),
                    PartnerPersonId = table.Column<int>(nullable: true),
                    RelationshipToIndexClient = table.Column<string>(nullable: true),
                    ScreenedForIpv = table.Column<string>(nullable: true),
                    IpvScreeningOutcome = table.Column<string>(nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(nullable: true),
                    PnsApproach = table.Column<string>(nullable: true),
                    Trace1Outcome = table.Column<string>(nullable: true),
                    Trace1Type = table.Column<string>(nullable: true),
                    Trace1Date = table.Column<DateTime>(nullable: true),
                    Trace2Outcome = table.Column<string>(nullable: true),
                    Trace2Type = table.Column<string>(nullable: true),
                    Trace2Date = table.Column<DateTime>(nullable: true),
                    Trace3Outcome = table.Column<string>(nullable: true),
                    Trace3Type = table.Column<string>(nullable: true),
                    Trace3Date = table.Column<DateTime>(nullable: true),
                    PnsConsent = table.Column<string>(nullable: true),
                    Linked = table.Column<string>(nullable: true),
                    LinkDateLinkedToCare = table.Column<DateTime>(nullable: true),
                    CccNumber = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: true),
                    Sex = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsClientPartnerExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsClientsExtracts",
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
                    Dob = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    KeyPopulationType = table.Column<string>(nullable: true),
                    PatientDisabled = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    SubCounty = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true),
                    NUPI = table.Column<string>(nullable: true),
                    Pkv = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    PriorityPopulationType = table.Column<string>(nullable: true),
                    HtsRecencyId = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsClientsExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsClientsLinkageExtracts",
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
                    DatePrefferedToBeEnrolled = table.Column<DateTime>(nullable: true),
                    FacilityReferredTo = table.Column<string>(nullable: true),
                    HandedOverTo = table.Column<string>(nullable: true),
                    HandedOverToCadre = table.Column<string>(nullable: true),
                    EnrolledFacilityName = table.Column<string>(nullable: true),
                    ReferralDate = table.Column<DateTime>(nullable: true),
                    DateEnrolled = table.Column<DateTime>(nullable: true),
                    ReportedCCCNumber = table.Column<string>(nullable: true),
                    ReportedStartARTDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsClientsLinkageExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsClientTestsExtracts",
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
                    EncounterId = table.Column<int>(nullable: true),
                    TestDate = table.Column<DateTime>(nullable: true),
                    EverTestedForHiv = table.Column<string>(nullable: true),
                    MonthsSinceLastTest = table.Column<int>(nullable: true),
                    ClientTestedAs = table.Column<string>(nullable: true),
                    EntryPoint = table.Column<string>(nullable: true),
                    TestStrategy = table.Column<string>(nullable: true),
                    TestResult1 = table.Column<string>(nullable: true),
                    TestResult2 = table.Column<string>(nullable: true),
                    FinalTestResult = table.Column<string>(nullable: true),
                    PatientGivenResult = table.Column<string>(nullable: true),
                    TbScreening = table.Column<string>(nullable: true),
                    ClientSelfTested = table.Column<string>(nullable: true),
                    CoupleDiscordant = table.Column<string>(nullable: true),
                    TestType = table.Column<string>(nullable: true),
                    Consent = table.Column<string>(nullable: true),
                    Setting = table.Column<string>(nullable: true),
                    Approach = table.Column<string>(nullable: true),
                    HtsRiskCategory = table.Column<string>(nullable: true),
                    HtsRiskScore = table.Column<string>(nullable: true),
                    ReferredForServices = table.Column<string>(nullable: true),
                    ReferredServices = table.Column<string>(nullable: true),
                    OtherReferredServices = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsClientTestsExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsClientTracingExtracts",
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
                    TracingType = table.Column<string>(nullable: true),
                    TracingDate = table.Column<DateTime>(nullable: true),
                    TracingOutcome = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsClientTracingExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsEligibilityExtracts",
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
                    EncounterId = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    KeyPopulation = table.Column<string>(nullable: true),
                    PriorityPopulation = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    PatientType = table.Column<string>(nullable: true),
                    IsHealthWorker = table.Column<string>(nullable: true),
                    RelationshipWithContact = table.Column<string>(nullable: true),
                    TestedHIVBefore = table.Column<string>(nullable: true),
                    WhoPerformedTest = table.Column<string>(nullable: true),
                    ResultOfHIV = table.Column<string>(nullable: true),
                    StartedOnART = table.Column<string>(nullable: true),
                    CCCNumber = table.Column<string>(nullable: true),
                    EverHadSex = table.Column<string>(nullable: true),
                    SexuallyActive = table.Column<string>(nullable: true),
                    NewPartner = table.Column<string>(nullable: true),
                    PartnerHIVStatus = table.Column<string>(nullable: true),
                    CoupleDiscordant = table.Column<string>(nullable: true),
                    MultiplePartners = table.Column<string>(nullable: true),
                    NumberOfPartners = table.Column<int>(nullable: true),
                    AlcoholSex = table.Column<string>(nullable: true),
                    MoneySex = table.Column<string>(nullable: true),
                    CondomBurst = table.Column<string>(nullable: true),
                    UnknownStatusPartner = table.Column<string>(nullable: true),
                    KnownStatusPartner = table.Column<string>(nullable: true),
                    Pregnant = table.Column<string>(nullable: true),
                    BreastfeedingMother = table.Column<string>(nullable: true),
                    ExperiencedGBV = table.Column<string>(nullable: true),
                    EverOnPrep = table.Column<string>(nullable: true),
                    CurrentlyOnPrep = table.Column<string>(nullable: true),
                    EverOnPep = table.Column<string>(nullable: true),
                    CurrentlyOnPep = table.Column<string>(nullable: true),
                    EverHadSTI = table.Column<string>(nullable: true),
                    CurrentlyHasSTI = table.Column<string>(nullable: true),
                    EverHadTB = table.Column<string>(nullable: true),
                    SharedNeedle = table.Column<string>(nullable: true),
                    NeedleStickInjuries = table.Column<string>(nullable: true),
                    TraditionalProcedures = table.Column<string>(nullable: true),
                    ChildReasonsForIneligibility = table.Column<string>(nullable: true),
                    EligibleForTest = table.Column<string>(nullable: true),
                    ReasonsForIneligibility = table.Column<string>(nullable: true),
                    SpecificReasonForIneligibility = table.Column<int>(nullable: true),
                    MothersStatus = table.Column<string>(nullable: true),
                    DateTestedSelf = table.Column<DateTime>(nullable: true),
                    ResultOfHIVSelf = table.Column<string>(nullable: true),
                    DateTestedProvider = table.Column<DateTime>(nullable: true),
                    ScreenedTB = table.Column<string>(nullable: true),
                    Cough = table.Column<string>(nullable: true),
                    Fever = table.Column<string>(nullable: true),
                    WeightLoss = table.Column<string>(nullable: true),
                    NightSweats = table.Column<string>(nullable: true),
                    Lethargy = table.Column<string>(nullable: true),
                    TBStatus = table.Column<string>(nullable: true),
                    ReferredForTesting = table.Column<string>(nullable: true),
                    AssessmentOutcome = table.Column<string>(nullable: true),
                    TypeGBV = table.Column<string>(nullable: true),
                    ForcedSex = table.Column<string>(nullable: true),
                    ReceivedServices = table.Column<string>(nullable: true),
                    ContactWithTBCase = table.Column<string>(nullable: true),
                    Disability = table.Column<string>(nullable: true),
                    DisabilityType = table.Column<string>(nullable: true),
                    HTSStrategy = table.Column<string>(nullable: true),
                    HTSEntryPoint = table.Column<string>(nullable: true),
                    HIVRiskCategory = table.Column<string>(nullable: true),
                    ReasonRefferredForTesting = table.Column<string>(nullable: true),
                    ReasonNotReffered = table.Column<string>(nullable: true),
                    HtsRiskScore = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsEligibilityExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsPartnerNotificationServicesExtracts",
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
                    PartnerPatientPk = table.Column<int>(nullable: true),
                    PartnerPersonID = table.Column<int>(nullable: true),
                    Age = table.Column<int>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    RelationsipToIndexClient = table.Column<string>(nullable: true),
                    ScreenedForIpv = table.Column<string>(nullable: true),
                    IpvScreeningOutcome = table.Column<string>(nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(nullable: true),
                    PnsApproach = table.Column<string>(nullable: true),
                    PnsConsent = table.Column<string>(nullable: true),
                    LinkedToCare = table.Column<string>(nullable: true),
                    LinkDateLinkedToCare = table.Column<DateTime>(nullable: true),
                    CccNumber = table.Column<string>(nullable: true),
                    FacilityLinkedTo = table.Column<string>(nullable: true),
                    Dob = table.Column<DateTime>(nullable: true),
                    DateElicited = table.Column<DateTime>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsPartnerNotificationServicesExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsPartnerTracingExtracts",
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
                    TraceType = table.Column<string>(nullable: true),
                    PartnerPersonId = table.Column<int>(nullable: true),
                    TraceDate = table.Column<DateTime>(nullable: true),
                    TraceOutcome = table.Column<string>(nullable: true),
                    BookingDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsPartnerTracingExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempHtsTestKitsExtracts",
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
                    EncounterId = table.Column<int>(nullable: true),
                    TestKitName1 = table.Column<string>(nullable: true),
                    TestKitLotNumber1 = table.Column<string>(nullable: true),
                    TestKitExpiry1 = table.Column<string>(nullable: true),
                    TestResult1 = table.Column<string>(nullable: true),
                    TestKitName2 = table.Column<string>(nullable: true),
                    TestKitLotNumber2 = table.Column<string>(nullable: true),
                    TestKitExpiry2 = table.Column<string>(nullable: true),
                    TestResult2 = table.Column<string>(nullable: true),
                    SyphilisResult = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsTestKitsExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempIITRiskScoresExtracts",
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
                    SourceSysUUID = table.Column<string>(nullable: true),
                    RiskScore = table.Column<string>(nullable: true),
                    RiskFactors = table.Column<string>(nullable: true),
                    RiskDescription = table.Column<string>(nullable: true),
                    RiskEvaluationDate = table.Column<DateTime>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempIITRiskScoresExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempIndicatorExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Indicator = table.Column<string>(nullable: true),
                    IndicatorValue = table.Column<string>(nullable: true),
                    IndicatorDate = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempIndicatorExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempIptExtracts",
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
                    OnTBDrugs = table.Column<string>(nullable: true),
                    OnIPT = table.Column<string>(nullable: true),
                    EverOnIPT = table.Column<string>(nullable: true),
                    Cough = table.Column<string>(nullable: true),
                    Fever = table.Column<string>(nullable: true),
                    NoticeableWeightLoss = table.Column<string>(nullable: true),
                    NightSweats = table.Column<string>(nullable: true),
                    Lethargy = table.Column<string>(nullable: true),
                    ICFActionTaken = table.Column<string>(nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    TBClinicalDiagnosis = table.Column<string>(nullable: true),
                    ContactsInvited = table.Column<string>(nullable: true),
                    EvaluatedForIPT = table.Column<string>(nullable: true),
                    StartAntiTBs = table.Column<string>(nullable: true),
                    TBRxStartDate = table.Column<DateTime>(nullable: true),
                    TBScreening = table.Column<string>(nullable: true),
                    IPTClientWorkUp = table.Column<string>(nullable: true),
                    StartIPT = table.Column<string>(nullable: true),
                    IndicationForIPT = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    TPTInitiationDate = table.Column<DateTime>(nullable: true),
                    IPTDiscontinuation = table.Column<string>(nullable: true),
                    DateOfDiscontinuation = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempIptExtracts", x => x.Id);
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
                name: "TempMatVisitExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    AdmissionNumber = table.Column<string>(nullable: true),
                    ANCVisits = table.Column<int>(nullable: true),
                    DateOfDelivery = table.Column<DateTime>(nullable: true),
                    DurationOfDelivery = table.Column<int>(nullable: true),
                    GestationAtBirth = table.Column<int>(nullable: true),
                    ModeOfDelivery = table.Column<string>(nullable: true),
                    PlacentaComplete = table.Column<string>(nullable: true),
                    UterotonicGiven = table.Column<string>(nullable: true),
                    VaginalExamination = table.Column<string>(nullable: true),
                    BloodLoss = table.Column<int>(nullable: true),
                    BloodLossVisual = table.Column<string>(nullable: true),
                    ConditonAfterDelivery = table.Column<string>(nullable: true),
                    MaternalDeath = table.Column<DateTime>(nullable: true),
                    DeliveryComplications = table.Column<string>(nullable: true),
                    NoBabiesDelivered = table.Column<int>(nullable: true),
                    BabyBirthNumber = table.Column<int>(nullable: true),
                    SexBaby = table.Column<string>(nullable: true),
                    BirthWeight = table.Column<string>(nullable: true),
                    BirthOutcome = table.Column<string>(nullable: true),
                    BirthWithDeformity = table.Column<string>(nullable: true),
                    TetracyclineGiven = table.Column<string>(nullable: true),
                    InitiatedBF = table.Column<string>(nullable: true),
                    ApgarScore1 = table.Column<int>(nullable: true),
                    ApgarScore5 = table.Column<int>(nullable: true),
                    ApgarScore10 = table.Column<int>(nullable: true),
                    KangarooCare = table.Column<string>(nullable: true),
                    ChlorhexidineApplied = table.Column<string>(nullable: true),
                    VitaminKGiven = table.Column<string>(nullable: true),
                    StatusBabyDischarge = table.Column<string>(nullable: true),
                    MotherDischargeDate = table.Column<string>(nullable: true),
                    SyphilisTestResults = table.Column<string>(nullable: true),
                    HIVStatusLastANC = table.Column<string>(nullable: true),
                    HIVTestingDone = table.Column<string>(nullable: true),
                    HIVTest1 = table.Column<string>(nullable: true),
                    HIV1Results = table.Column<string>(nullable: true),
                    HIVTest2 = table.Column<string>(nullable: true),
                    HIV2Results = table.Column<string>(nullable: true),
                    HIVTestFinalResult = table.Column<string>(nullable: true),
                    OnARTANC = table.Column<string>(nullable: true),
                    BabyGivenProphylaxis = table.Column<string>(nullable: true),
                    MotherGivenCTX = table.Column<string>(nullable: true),
                    PartnerHIVTestingMAT = table.Column<string>(nullable: true),
                    PartnerHIVStatusMAT = table.Column<string>(nullable: true),
                    CounselledOn = table.Column<string>(nullable: true),
                    ReferredFrom = table.Column<string>(nullable: true),
                    ReferredTo = table.Column<string>(nullable: true),
                    ClinicalNotes = table.Column<string>(nullable: true),
                    LMP = table.Column<DateTime>(nullable: true),
                    EDD = table.Column<DateTime>(nullable: true),
                    MaternalDeathAudited = table.Column<string>(nullable: true),
                    ReferralReason = table.Column<string>(nullable: true),
                    OnARTMat = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMatVisitExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempMetricMigrationExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    MetricId = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    ErrorType = table.Column<int>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Dataset = table.Column<string>(nullable: true),
                    Metric = table.Column<string>(nullable: true),
                    MetricValue = table.Column<string>(nullable: true),
                    Stage = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMetricMigrationExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempMnchArtExtracts",
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
                    Pkv = table.Column<string>(nullable: true),
                    PatientMnchID = table.Column<string>(nullable: true),
                    PatientHeiID = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    RegistrationAtCCC = table.Column<DateTime>(nullable: true),
                    StartARTDate = table.Column<DateTime>(nullable: true),
                    StartRegimen = table.Column<string>(nullable: true),
                    StartRegimenLine = table.Column<string>(nullable: true),
                    StatusAtCCC = table.Column<string>(nullable: true),
                    LastARTDate = table.Column<DateTime>(nullable: true),
                    LastRegimen = table.Column<string>(nullable: true),
                    LastRegimenLine = table.Column<string>(nullable: true),
                    FacilityReceivingARTCare = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMnchArtExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempMnchEnrolmentExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    ServiceType = table.Column<string>(nullable: true),
                    EnrollmentDateAtMnch = table.Column<DateTime>(nullable: true),
                    MnchNumber = table.Column<DateTime>(nullable: true),
                    FirstVisitAnc = table.Column<DateTime>(nullable: true),
                    Parity = table.Column<string>(nullable: true),
                    Gravidae = table.Column<int>(nullable: false),
                    LMP = table.Column<DateTime>(nullable: true),
                    EDDFromLMP = table.Column<DateTime>(nullable: true),
                    HIVStatusBeforeANC = table.Column<string>(nullable: true),
                    HIVTestDate = table.Column<DateTime>(nullable: true),
                    PartnerHIVStatus = table.Column<string>(nullable: true),
                    PartnerHIVTestDate = table.Column<DateTime>(nullable: true),
                    BloodGroup = table.Column<string>(nullable: true),
                    StatusAtMnch = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMnchEnrolmentExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempMnchImmunizationExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    BCG = table.Column<DateTime>(nullable: true),
                    OPVatBirth = table.Column<DateTime>(nullable: true),
                    OPV1 = table.Column<DateTime>(nullable: true),
                    OPV2 = table.Column<DateTime>(nullable: true),
                    OPV3 = table.Column<DateTime>(nullable: true),
                    IPV = table.Column<DateTime>(nullable: true),
                    DPTHepBHIB1 = table.Column<DateTime>(nullable: true),
                    DPTHepBHIB2 = table.Column<DateTime>(nullable: true),
                    DPTHepBHIB3 = table.Column<DateTime>(nullable: true),
                    PCV101 = table.Column<DateTime>(nullable: true),
                    PCV102 = table.Column<DateTime>(nullable: true),
                    PCV103 = table.Column<DateTime>(nullable: true),
                    ROTA1 = table.Column<DateTime>(nullable: true),
                    MeaslesReubella1 = table.Column<DateTime>(nullable: true),
                    YellowFever = table.Column<DateTime>(nullable: true),
                    MeaslesReubella2 = table.Column<DateTime>(nullable: true),
                    MeaslesAt6Months = table.Column<DateTime>(nullable: true),
                    ROTA2 = table.Column<DateTime>(nullable: true),
                    DateOfNextVisit = table.Column<DateTime>(nullable: true),
                    BCGScarChecked = table.Column<string>(nullable: true),
                    DateChecked = table.Column<DateTime>(nullable: true),
                    DateBCGrepeated = table.Column<DateTime>(nullable: true),
                    VitaminAAt6Months = table.Column<DateTime>(nullable: true),
                    VitaminAAt1Yr = table.Column<DateTime>(nullable: true),
                    VitaminAAt18Months = table.Column<DateTime>(nullable: true),
                    VitaminAAt2Years = table.Column<DateTime>(nullable: true),
                    VitaminAAt2To5Years = table.Column<DateTime>(nullable: true),
                    FullyImmunizedChild = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMnchImmunizationExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempMnchLabExtracts",
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
                    PatientMNCH_ID = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    SatelliteName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    OrderedbyDate = table.Column<DateTime>(nullable: true),
                    ReportedbyDate = table.Column<DateTime>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    LabReason = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMnchLabExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempMotherBabyPairExtracts",
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
                    BabyPatientPK = table.Column<int>(nullable: false),
                    MotherPatientPK = table.Column<int>(nullable: false),
                    BabyPatientMncHeiID = table.Column<string>(nullable: true),
                    MotherPatientMncHeiID = table.Column<string>(nullable: true),
                    PatientIDCCC = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMotherBabyPairExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempOtzExtracts",
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
                    OTZEnrollmentDate = table.Column<DateTime>(nullable: true),
                    TransferInStatus = table.Column<string>(nullable: true),
                    ModulesPreviouslyCovered = table.Column<string>(nullable: true),
                    ModulesCompletedToday = table.Column<string>(nullable: true),
                    SupportGroupInvolvement = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    TransitionAttritionReason = table.Column<string>(nullable: true),
                    OutcomeDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOtzExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempOvcExtracts",
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
                    OVCEnrollmentDate = table.Column<DateTime>(nullable: true),
                    RelationshipToClient = table.Column<string>(nullable: true),
                    EnrolledinCPIMS = table.Column<string>(nullable: true),
                    CPIMSUniqueIdentifier = table.Column<string>(nullable: true),
                    PartnerOfferingOVCServices = table.Column<string>(nullable: true),
                    OVCExitReason = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOvcExtracts", x => x.Id);
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
                    ErrorType = table.Column<int>(nullable: false),
                    AdverseEvent = table.Column<string>(nullable: true),
                    AdverseEventStartDate = table.Column<DateTime>(nullable: true),
                    AdverseEventEndDate = table.Column<DateTime>(nullable: true),
                    Severity = table.Column<string>(nullable: true),
                    AdverseEventRegimen = table.Column<string>(nullable: true),
                    AdverseEventCause = table.Column<string>(nullable: true),
                    AdverseEventClinicalOutcome = table.Column<string>(nullable: true),
                    AdverseEventActionTaken = table.Column<string>(nullable: true),
                    AdverseEventIsPregnant = table.Column<bool>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientAdverseEventExtracts", x => x.Id);
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
                    ErrorType = table.Column<int>(nullable: false),
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
                    ExitDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    PreviousARTUse = table.Column<string>(nullable: true),
                    PreviousARTPurpose = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    DateLastUsed = table.Column<DateTime>(nullable: true)
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
                    ErrorType = table.Column<int>(nullable: false),
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
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
                    ErrorType = table.Column<int>(nullable: false),
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
                    SatelliteName = table.Column<string>(nullable: true),
                    Orphan = table.Column<string>(nullable: true),
                    Inschool = table.Column<string>(nullable: true),
                    PatientType = table.Column<string>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    KeyPopulationType = table.Column<string>(nullable: true),
                    PatientResidentCounty = table.Column<string>(nullable: true),
                    PatientResidentSubCounty = table.Column<string>(nullable: true),
                    PatientResidentLocation = table.Column<string>(nullable: true),
                    PatientResidentSubLocation = table.Column<string>(nullable: true),
                    PatientResidentWard = table.Column<string>(nullable: true),
                    PatientResidentVillage = table.Column<string>(nullable: true),
                    TransferInDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Pkv = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    NUPI = table.Column<string>(nullable: true)
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
                    ErrorType = table.Column<int>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    SatelliteName = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    VisitId = table.Column<int>(nullable: true),
                    OrderedByDate = table.Column<DateTime>(nullable: true),
                    ReportedByDate = table.Column<DateTime>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    EnrollmentTest = table.Column<int>(nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    DateSampleTaken = table.Column<DateTime>(nullable: true),
                    SampleType = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientLaboratoryExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientMnchExtracts",
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
                    Pkv = table.Column<string>(nullable: true),
                    PatientMnchID = table.Column<string>(nullable: true),
                    PatientHeiID = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    FirstEnrollmentAtMnch = table.Column<DateTime>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    EducationLevel = table.Column<string>(nullable: true),
                    PatientResidentCounty = table.Column<string>(nullable: true),
                    PatientResidentSubCounty = table.Column<string>(nullable: true),
                    PatientResidentWard = table.Column<string>(nullable: true),
                    InSchool = table.Column<string>(nullable: true),
                    NUPI = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientMnchExtracts", x => x.Id);
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
                    ErrorType = table.Column<int>(nullable: false),
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RegimenChangedSwitched = table.Column<string>(nullable: true),
                    RegimenChangeSwitchReason = table.Column<string>(nullable: true),
                    StopRegimenReason = table.Column<string>(nullable: true),
                    StopRegimenDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientPharmacyExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPatientPrepExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    PrepEnrollmentDate = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: true),
                    CountyofBirth = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    SubCounty = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    LandMark = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true),
                    ClientType = table.Column<string>(nullable: true),
                    ReferralPoint = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Inschool = table.Column<string>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    KeyPopulationType = table.Column<string>(nullable: true),
                    Refferedfrom = table.Column<string>(nullable: true),
                    TransferIn = table.Column<string>(nullable: true),
                    TransferInDate = table.Column<DateTime>(nullable: true),
                    TransferFromFacility = table.Column<string>(nullable: true),
                    DatefirstinitiatedinPrepCare = table.Column<DateTime>(nullable: true),
                    DateStartedPrEPattransferringfacility = table.Column<DateTime>(nullable: true),
                    ClientPreviouslyonPrep = table.Column<string>(nullable: true),
                    PrevPrepReg = table.Column<string>(nullable: true),
                    DateLastUsedPrev = table.Column<DateTime>(nullable: true),
                    NUPI = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientPrepExtracts", x => x.Id);
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
                    ErrorType = table.Column<int>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    ExitDescription = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    TOVerified = table.Column<string>(nullable: true),
                    TOVerifiedDate = table.Column<DateTime>(nullable: true),
                    ReEnrollmentDate = table.Column<DateTime>(nullable: true),
                    ReasonForDeath = table.Column<string>(nullable: true),
                    SpecificDeathReason = table.Column<string>(nullable: true),
                    DeathDate = table.Column<DateTime>(nullable: true),
                    EffectiveDiscontinuationDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
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
                    ErrorType = table.Column<int>(nullable: false),
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
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    StabilityAssessment = table.Column<string>(nullable: true),
                    DifferentiatedCare = table.Column<string>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    KeyPopulationType = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    VisitBy = table.Column<string>(nullable: true),
                    Temp = table.Column<decimal>(nullable: true),
                    PulseRate = table.Column<int>(nullable: true),
                    RespiratoryRate = table.Column<int>(nullable: true),
                    OxygenSaturation = table.Column<decimal>(nullable: true),
                    Muac = table.Column<int>(nullable: true),
                    NutritionalStatus = table.Column<string>(nullable: true),
                    EverHadMenses = table.Column<string>(nullable: true),
                    Breastfeeding = table.Column<string>(nullable: true),
                    Menopausal = table.Column<string>(nullable: true),
                    NoFPReason = table.Column<string>(nullable: true),
                    ProphylaxisUsed = table.Column<string>(nullable: true),
                    CTXAdherence = table.Column<string>(nullable: true),
                    CurrentRegimen = table.Column<string>(nullable: true),
                    HCWConcern = table.Column<string>(nullable: true),
                    TCAReason = table.Column<string>(nullable: true),
                    ClinicalNotes = table.Column<string>(nullable: true),
                    GeneralExamination = table.Column<string>(nullable: true),
                    SystemExamination = table.Column<string>(nullable: true),
                    Skin = table.Column<string>(nullable: true),
                    Eyes = table.Column<string>(nullable: true),
                    ENT = table.Column<string>(nullable: true),
                    Chest = table.Column<string>(nullable: true),
                    CVS = table.Column<string>(nullable: true),
                    Abdomen = table.Column<string>(nullable: true),
                    CNS = table.Column<string>(nullable: true),
                    Genitourinary = table.Column<string>(nullable: true),
                    RefillDate = table.Column<DateTime>(nullable: true),
                    ZScore = table.Column<string>(nullable: true),
                    PaedsDisclosure = table.Column<string>(nullable: true),
                    ZScoreAbsolute = table.Column<int>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientVisitExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPncVisitExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    PNCRegisterNumber = table.Column<string>(nullable: true),
                    PNCVisitNo = table.Column<int>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: true),
                    ModeOfDelivery = table.Column<string>(nullable: true),
                    PlaceOfDelivery = table.Column<string>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true),
                    Temp = table.Column<decimal>(nullable: true),
                    PulseRate = table.Column<int>(nullable: true),
                    RespiratoryRate = table.Column<int>(nullable: true),
                    OxygenSaturation = table.Column<decimal>(nullable: true),
                    MUAC = table.Column<int>(nullable: true),
                    BP = table.Column<int>(nullable: true),
                    BreastExam = table.Column<string>(nullable: true),
                    GeneralCondition = table.Column<string>(nullable: true),
                    HasPallor = table.Column<string>(nullable: true),
                    Pallor = table.Column<string>(nullable: true),
                    Breast = table.Column<string>(nullable: true),
                    PPH = table.Column<string>(nullable: true),
                    CSScar = table.Column<string>(nullable: true),
                    UterusInvolution = table.Column<string>(nullable: true),
                    Episiotomy = table.Column<string>(nullable: true),
                    Lochia = table.Column<string>(nullable: true),
                    Fistula = table.Column<string>(nullable: true),
                    MaternalComplications = table.Column<string>(nullable: true),
                    TBScreening = table.Column<string>(nullable: true),
                    ClientScreenedCACx = table.Column<string>(nullable: true),
                    CACxScreenMethod = table.Column<string>(nullable: true),
                    CACxScreenResults = table.Column<string>(nullable: true),
                    PriorHIVStatus = table.Column<string>(nullable: true),
                    HIVTestingDone = table.Column<string>(nullable: true),
                    HIVTest1 = table.Column<string>(nullable: true),
                    HIVTest1Result = table.Column<string>(nullable: true),
                    HIVTest2 = table.Column<string>(nullable: true),
                    HIVTest2Result = table.Column<string>(nullable: true),
                    HIVTestFinalResult = table.Column<string>(nullable: true),
                    InfantProphylaxisGiven = table.Column<string>(nullable: true),
                    MotherProphylaxisGiven = table.Column<string>(nullable: true),
                    CoupleCounselled = table.Column<string>(nullable: true),
                    PartnerHIVTestingPNC = table.Column<string>(nullable: true),
                    PartnerHIVResultPNC = table.Column<string>(nullable: true),
                    CounselledOnFP = table.Column<string>(nullable: true),
                    ReceivedFP = table.Column<string>(nullable: true),
                    HaematinicsGiven = table.Column<string>(nullable: true),
                    DeliveryOutcome = table.Column<string>(nullable: true),
                    BabyConditon = table.Column<string>(nullable: true),
                    BabyFeeding = table.Column<string>(nullable: true),
                    UmbilicalCord = table.Column<string>(nullable: true),
                    Immunization = table.Column<string>(nullable: true),
                    InfantFeeding = table.Column<string>(nullable: true),
                    PreventiveServices = table.Column<string>(nullable: true),
                    ReferredFrom = table.Column<string>(nullable: true),
                    ReferredTo = table.Column<string>(nullable: true),
                    NextAppointmentPNC = table.Column<DateTime>(nullable: true),
                    ClinicalNotes = table.Column<string>(nullable: true),
                    VisitTimingMother = table.Column<string>(nullable: true),
                    VisitTimingBaby = table.Column<string>(nullable: true),
                    MotherCameForHIVTest = table.Column<string>(nullable: true),
                    InfactCameForHAART = table.Column<string>(nullable: true),
                    MotherGivenHAART = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPncVisitExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPrepAdverseEventExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    AdverseEvent = table.Column<string>(nullable: true),
                    AdverseEventStartDate = table.Column<DateTime>(nullable: true),
                    AdverseEventEndDate = table.Column<DateTime>(nullable: true),
                    Severity = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    AdverseEventActionTaken = table.Column<string>(nullable: true),
                    AdverseEventClinicalOutcome = table.Column<string>(nullable: true),
                    AdverseEventIsPregnant = table.Column<string>(nullable: true),
                    AdverseEventCause = table.Column<string>(nullable: true),
                    AdverseEventRegimen = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPrepAdverseEventExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPrepBehaviourRiskExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    SexPartnerHIVStatus = table.Column<string>(nullable: true),
                    IsHIVPositivePartnerCurrentonART = table.Column<string>(nullable: true),
                    IsPartnerHighrisk = table.Column<string>(nullable: true),
                    PartnerARTRisk = table.Column<string>(nullable: true),
                    ClientAssessments = table.Column<string>(nullable: true),
                    ClientRisk = table.Column<string>(nullable: true),
                    ClientWillingToTakePrep = table.Column<string>(nullable: true),
                    PrEPDeclineReason = table.Column<string>(nullable: true),
                    RiskReductionEducationOffered = table.Column<string>(nullable: true),
                    ReferralToOtherPrevServices = table.Column<string>(nullable: true),
                    FirstEstablishPartnerStatus = table.Column<DateTime>(nullable: true),
                    PartnerEnrolledtoCCC = table.Column<DateTime>(nullable: true),
                    HIVPartnerCCCnumber = table.Column<string>(nullable: true),
                    HIVPartnerARTStartDate = table.Column<DateTime>(nullable: true),
                    MonthsknownHIVSerodiscordant = table.Column<string>(nullable: true),
                    SexWithoutCondom = table.Column<string>(nullable: true),
                    NumberofchildrenWithPartner = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPrepBehaviourRiskExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPrepCareTerminationExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    DateOfLastPrepDose = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPrepCareTerminationExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPrepLabExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    SampleDate = table.Column<DateTime>(nullable: true),
                    TestResultDate = table.Column<DateTime>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPrepLabExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPrepPharmacyExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    RegimenPrescribed = table.Column<string>(nullable: true),
                    DispenseDate = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPrepPharmacyExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPrepVisitExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    EncounterId = table.Column<string>(nullable: true),
                    VisitID = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    BloodPressure = table.Column<string>(nullable: true),
                    Temperature = table.Column<string>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    BMI = table.Column<decimal>(nullable: true),
                    STIScreening = table.Column<string>(nullable: true),
                    STISymptoms = table.Column<string>(nullable: true),
                    STITreated = table.Column<string>(nullable: true),
                    Circumcised = table.Column<string>(nullable: true),
                    VMMCReferral = table.Column<string>(nullable: true),
                    LMP = table.Column<DateTime>(nullable: true),
                    MenopausalStatus = table.Column<string>(nullable: true),
                    PregnantAtThisVisit = table.Column<string>(nullable: true),
                    EDD = table.Column<DateTime>(nullable: true),
                    PlanningToGetPregnant = table.Column<string>(nullable: true),
                    PregnancyPlanned = table.Column<string>(nullable: true),
                    PregnancyEnded = table.Column<string>(nullable: true),
                    PregnancyEndDate = table.Column<DateTime>(nullable: true),
                    PregnancyOutcome = table.Column<string>(nullable: true),
                    BirthDefects = table.Column<string>(nullable: true),
                    Breastfeeding = table.Column<string>(nullable: true),
                    FamilyPlanningStatus = table.Column<string>(nullable: true),
                    FPMethods = table.Column<string>(nullable: true),
                    AdherenceDone = table.Column<string>(nullable: true),
                    AdherenceOutcome = table.Column<string>(nullable: true),
                    AdherenceReasons = table.Column<string>(nullable: true),
                    SymptomsAcuteHIV = table.Column<string>(nullable: true),
                    ContraindicationsPrep = table.Column<string>(nullable: true),
                    PrepTreatmentPlan = table.Column<string>(nullable: true),
                    PrepPrescribed = table.Column<string>(nullable: true),
                    RegimenPrescribed = table.Column<string>(nullable: true),
                    MonthsPrescribed = table.Column<string>(nullable: true),
                    CondomsIssued = table.Column<string>(nullable: true),
                    Tobegivennextappointment = table.Column<string>(nullable: true),
                    Reasonfornotgivingnextappointment = table.Column<string>(nullable: true),
                    HepatitisBPositiveResult = table.Column<string>(nullable: true),
                    HepatitisCPositiveResult = table.Column<string>(nullable: true),
                    VaccinationForHepBStarted = table.Column<string>(nullable: true),
                    TreatedForHepB = table.Column<string>(nullable: true),
                    VaccinationForHepCStarted = table.Column<string>(nullable: true),
                    TreatedForHepC = table.Column<string>(nullable: true),
                    NextAppointment = table.Column<DateTime>(nullable: true),
                    ClinicalNotes = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPrepVisitExtracts", x => x.Id);
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
                name: "HtsClientsLinkageExtracts",
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
                    DatePrefferedToBeEnrolled = table.Column<DateTime>(nullable: true),
                    FacilityReferredTo = table.Column<string>(nullable: true),
                    HandedOverTo = table.Column<string>(nullable: true),
                    HandedOverToCadre = table.Column<string>(nullable: true),
                    EnrolledFacilityName = table.Column<string>(nullable: true),
                    ReferralDate = table.Column<DateTime>(nullable: true),
                    DateEnrolled = table.Column<DateTime>(nullable: true),
                    ReportedCCCNumber = table.Column<string>(nullable: true),
                    ReportedStartARTDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientsLinkageExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsClientsLinkageExtracts_HtsClientsExtracts_SiteCode_Patien~",
                        columns: x => new { x.SiteCode, x.PatientPk },
                        principalTable: "HtsClientsExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPk" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsClientTestsExtracts",
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
                    EncounterId = table.Column<int>(nullable: true),
                    TestDate = table.Column<DateTime>(nullable: true),
                    EverTestedForHiv = table.Column<string>(nullable: true),
                    MonthsSinceLastTest = table.Column<int>(nullable: true),
                    ClientTestedAs = table.Column<string>(nullable: true),
                    EntryPoint = table.Column<string>(nullable: true),
                    TestStrategy = table.Column<string>(nullable: true),
                    TestResult1 = table.Column<string>(nullable: true),
                    TestResult2 = table.Column<string>(nullable: true),
                    FinalTestResult = table.Column<string>(nullable: true),
                    PatientGivenResult = table.Column<string>(nullable: true),
                    TbScreening = table.Column<string>(nullable: true),
                    ClientSelfTested = table.Column<string>(nullable: true),
                    CoupleDiscordant = table.Column<string>(nullable: true),
                    TestType = table.Column<string>(nullable: true),
                    Consent = table.Column<string>(nullable: true),
                    Setting = table.Column<string>(nullable: true),
                    Approach = table.Column<string>(nullable: true),
                    HtsRiskCategory = table.Column<string>(nullable: true),
                    HtsRiskScore = table.Column<string>(nullable: true),
                    ReferredForServices = table.Column<string>(nullable: true),
                    ReferredServices = table.Column<string>(nullable: true),
                    OtherReferredServices = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientTestsExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsClientTestsExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                        columns: x => new { x.SiteCode, x.PatientPk },
                        principalTable: "HtsClientsExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPk" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsClientTracingExtracts",
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
                    TracingType = table.Column<string>(nullable: true),
                    TracingDate = table.Column<DateTime>(nullable: true),
                    TracingOutcome = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientTracingExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsClientTracingExtracts_HtsClientsExtracts_SiteCode_Patient~",
                        columns: x => new { x.SiteCode, x.PatientPk },
                        principalTable: "HtsClientsExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPk" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsEligibilityExtracts",
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
                    EncounterId = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    KeyPopulation = table.Column<string>(nullable: true),
                    PriorityPopulation = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    PatientType = table.Column<string>(nullable: true),
                    IsHealthWorker = table.Column<string>(nullable: true),
                    RelationshipWithContact = table.Column<string>(nullable: true),
                    TestedHIVBefore = table.Column<string>(nullable: true),
                    WhoPerformedTest = table.Column<string>(nullable: true),
                    ResultOfHIV = table.Column<string>(nullable: true),
                    StartedOnART = table.Column<string>(nullable: true),
                    CCCNumber = table.Column<string>(nullable: true),
                    EverHadSex = table.Column<string>(nullable: true),
                    SexuallyActive = table.Column<string>(nullable: true),
                    NewPartner = table.Column<string>(nullable: true),
                    PartnerHIVStatus = table.Column<string>(nullable: true),
                    CoupleDiscordant = table.Column<string>(nullable: true),
                    MultiplePartners = table.Column<string>(nullable: true),
                    NumberOfPartners = table.Column<int>(nullable: true),
                    AlcoholSex = table.Column<string>(nullable: true),
                    MoneySex = table.Column<string>(nullable: true),
                    CondomBurst = table.Column<string>(nullable: true),
                    UnknownStatusPartner = table.Column<string>(nullable: true),
                    KnownStatusPartner = table.Column<string>(nullable: true),
                    Pregnant = table.Column<string>(nullable: true),
                    BreastfeedingMother = table.Column<string>(nullable: true),
                    ExperiencedGBV = table.Column<string>(nullable: true),
                    EverOnPrep = table.Column<string>(nullable: true),
                    CurrentlyOnPrep = table.Column<string>(nullable: true),
                    EverOnPep = table.Column<string>(nullable: true),
                    CurrentlyOnPep = table.Column<string>(nullable: true),
                    EverHadSTI = table.Column<string>(nullable: true),
                    CurrentlyHasSTI = table.Column<string>(nullable: true),
                    EverHadTB = table.Column<string>(nullable: true),
                    SharedNeedle = table.Column<string>(nullable: true),
                    NeedleStickInjuries = table.Column<string>(nullable: true),
                    TraditionalProcedures = table.Column<string>(nullable: true),
                    ChildReasonsForIneligibility = table.Column<string>(nullable: true),
                    EligibleForTest = table.Column<string>(nullable: true),
                    ReasonsForIneligibility = table.Column<string>(nullable: true),
                    SpecificReasonForIneligibility = table.Column<int>(nullable: true),
                    MothersStatus = table.Column<string>(nullable: true),
                    DateTestedSelf = table.Column<DateTime>(nullable: true),
                    ResultOfHIVSelf = table.Column<string>(nullable: true),
                    DateTestedProvider = table.Column<DateTime>(nullable: true),
                    ScreenedTB = table.Column<string>(nullable: true),
                    Cough = table.Column<string>(nullable: true),
                    Fever = table.Column<string>(nullable: true),
                    WeightLoss = table.Column<string>(nullable: true),
                    NightSweats = table.Column<string>(nullable: true),
                    Lethargy = table.Column<string>(nullable: true),
                    TBStatus = table.Column<string>(nullable: true),
                    ReferredForTesting = table.Column<string>(nullable: true),
                    AssessmentOutcome = table.Column<string>(nullable: true),
                    TypeGBV = table.Column<string>(nullable: true),
                    ForcedSex = table.Column<string>(nullable: true),
                    ReceivedServices = table.Column<string>(nullable: true),
                    ContactWithTBCase = table.Column<string>(nullable: true),
                    Disability = table.Column<string>(nullable: true),
                    DisabilityType = table.Column<string>(nullable: true),
                    HTSStrategy = table.Column<string>(nullable: true),
                    HTSEntryPoint = table.Column<string>(nullable: true),
                    HIVRiskCategory = table.Column<string>(nullable: true),
                    ReasonRefferredForTesting = table.Column<string>(nullable: true),
                    ReasonNotReffered = table.Column<string>(nullable: true),
                    HtsRiskScore = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsEligibilityExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsEligibilityExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                        columns: x => new { x.SiteCode, x.PatientPk },
                        principalTable: "HtsClientsExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPk" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsPartnerNotificationServicesExtracts",
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
                    PartnerPatientPk = table.Column<int>(nullable: true),
                    PartnerPersonID = table.Column<int>(nullable: true),
                    Age = table.Column<int>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    RelationsipToIndexClient = table.Column<string>(nullable: true),
                    ScreenedForIpv = table.Column<string>(nullable: true),
                    IpvScreeningOutcome = table.Column<string>(nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(nullable: true),
                    PnsApproach = table.Column<string>(nullable: true),
                    PnsConsent = table.Column<string>(nullable: true),
                    LinkedToCare = table.Column<string>(nullable: true),
                    LinkDateLinkedToCare = table.Column<DateTime>(nullable: true),
                    CccNumber = table.Column<string>(nullable: true),
                    FacilityLinkedTo = table.Column<string>(nullable: true),
                    Dob = table.Column<DateTime>(nullable: true),
                    DateElicited = table.Column<DateTime>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsPartnerNotificationServicesExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsPartnerNotificationServicesExtracts_HtsClientsExtracts_Si~",
                        columns: x => new { x.SiteCode, x.PatientPk },
                        principalTable: "HtsClientsExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPk" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsPartnerTracingExtracts",
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
                    TraceType = table.Column<string>(nullable: true),
                    TraceDate = table.Column<DateTime>(nullable: true),
                    PartnerPersonId = table.Column<int>(nullable: true),
                    TraceOutcome = table.Column<string>(nullable: true),
                    BookingDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsPartnerTracingExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsPartnerTracingExtracts_HtsClientsExtracts_SiteCode_Patien~",
                        columns: x => new { x.SiteCode, x.PatientPk },
                        principalTable: "HtsClientsExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPk" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsTestKitsExtracts",
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
                    EncounterId = table.Column<int>(nullable: true),
                    TestKitName1 = table.Column<string>(nullable: true),
                    TestKitLotNumber1 = table.Column<string>(nullable: true),
                    TestKitExpiry1 = table.Column<string>(nullable: true),
                    TestResult1 = table.Column<string>(nullable: true),
                    TestKitName2 = table.Column<string>(nullable: true),
                    TestKitLotNumber2 = table.Column<string>(nullable: true),
                    TestKitExpiry2 = table.Column<string>(nullable: true),
                    TestResult2 = table.Column<string>(nullable: true),
                    SyphilisResult = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsTestKitsExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsTestKitsExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                        columns: x => new { x.SiteCode, x.PatientPk },
                        principalTable: "HtsClientsExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPk" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllergiesChronicIllnessExtracts",
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
                    ChronicIllness = table.Column<string>(nullable: true),
                    ChronicOnsetDate = table.Column<DateTime>(nullable: true),
                    knownAllergies = table.Column<string>(nullable: true),
                    AllergyCausativeAgent = table.Column<string>(nullable: true),
                    AllergicReaction = table.Column<string>(nullable: true),
                    AllergySeverity = table.Column<string>(nullable: true),
                    AllergyOnsetDate = table.Column<DateTime>(nullable: true),
                    Skin = table.Column<string>(nullable: true),
                    Eyes = table.Column<string>(nullable: true),
                    ENT = table.Column<string>(nullable: true),
                    Chest = table.Column<string>(nullable: true),
                    CVS = table.Column<string>(nullable: true),
                    Abdomen = table.Column<string>(nullable: true),
                    CNS = table.Column<string>(nullable: true),
                    Genitourinary = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergiesChronicIllnessExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllergiesChronicIllnessExtracts_PatientExtracts_SiteCode_Pat~",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

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
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
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
                name: "ContactListingExtracts",
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
                    PartnerPersonID = table.Column<int>(nullable: true),
                    ContactAge = table.Column<string>(nullable: true),
                    ContactSex = table.Column<string>(nullable: true),
                    ContactMaritalStatus = table.Column<string>(nullable: true),
                    RelationshipWithPatient = table.Column<string>(nullable: true),
                    ScreenedForIpv = table.Column<string>(nullable: true),
                    IpvScreening = table.Column<string>(nullable: true),
                    IPVScreeningOutcome = table.Column<string>(nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(nullable: true),
                    PnsApproach = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    ContactPatientPK = table.Column<int>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactListingExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactListingExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

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
                    COVID19TestResult = table.Column<string>(nullable: true),
                    Sequence = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
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
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
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
                name: "DepressionScreeningExtracts",
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
                    PHQ9_1 = table.Column<string>(nullable: true),
                    PHQ9_2 = table.Column<string>(nullable: true),
                    PHQ9_3 = table.Column<string>(nullable: true),
                    PHQ9_4 = table.Column<string>(nullable: true),
                    PHQ9_5 = table.Column<string>(nullable: true),
                    PHQ9_6 = table.Column<string>(nullable: true),
                    PHQ9_7 = table.Column<string>(nullable: true),
                    PHQ9_8 = table.Column<string>(nullable: true),
                    PHQ9_9 = table.Column<string>(nullable: true),
                    PHQ_9_rating = table.Column<string>(nullable: true),
                    DepressionAssesmentScore = table.Column<int>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepressionScreeningExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepressionScreeningExtracts_PatientExtracts_SiteCode_Patient~",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugAlcoholScreeningExtracts",
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
                    DrinkingAlcohol = table.Column<string>(nullable: true),
                    Smoking = table.Column<string>(nullable: true),
                    DrugUse = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugAlcoholScreeningExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugAlcoholScreeningExtracts_PatientExtracts_SiteCode_Patien~",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnhancedAdherenceCounsellingExtracts",
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
                    SessionNumber = table.Column<int>(nullable: true),
                    DateOfFirstSession = table.Column<DateTime>(nullable: true),
                    PillCountAdherence = table.Column<int>(nullable: true),
                    MMAS4_1 = table.Column<string>(nullable: true),
                    MMAS4_2 = table.Column<string>(nullable: true),
                    MMAS4_3 = table.Column<string>(nullable: true),
                    MMAS4_4 = table.Column<string>(nullable: true),
                    MMSA8_1 = table.Column<string>(nullable: true),
                    MMSA8_2 = table.Column<string>(nullable: true),
                    MMSA8_3 = table.Column<string>(nullable: true),
                    MMSA8_4 = table.Column<string>(nullable: true),
                    MMSAScore = table.Column<string>(nullable: true),
                    EACRecievedVL = table.Column<string>(nullable: true),
                    EACVL = table.Column<string>(nullable: true),
                    EACVLConcerns = table.Column<string>(nullable: true),
                    EACVLThoughts = table.Column<string>(nullable: true),
                    EACWayForward = table.Column<string>(nullable: true),
                    EACCognitiveBarrier = table.Column<string>(nullable: true),
                    EACBehaviouralBarrier_1 = table.Column<string>(nullable: true),
                    EACBehaviouralBarrier_2 = table.Column<string>(nullable: true),
                    EACBehaviouralBarrier_3 = table.Column<string>(nullable: true),
                    EACBehaviouralBarrier_4 = table.Column<string>(nullable: true),
                    EACBehaviouralBarrier_5 = table.Column<string>(nullable: true),
                    EACEmotionalBarriers_1 = table.Column<string>(nullable: true),
                    EACEmotionalBarriers_2 = table.Column<string>(nullable: true),
                    EACEconBarrier_1 = table.Column<string>(nullable: true),
                    EACEconBarrier_2 = table.Column<string>(nullable: true),
                    EACEconBarrier_3 = table.Column<string>(nullable: true),
                    EACEconBarrier_4 = table.Column<string>(nullable: true),
                    EACEconBarrier_5 = table.Column<string>(nullable: true),
                    EACEconBarrier_6 = table.Column<string>(nullable: true),
                    EACEconBarrier_7 = table.Column<string>(nullable: true),
                    EACEconBarrier_8 = table.Column<string>(nullable: true),
                    EACReviewImprovement = table.Column<string>(nullable: true),
                    EACReviewMissedDoses = table.Column<string>(nullable: true),
                    EACReviewStrategy = table.Column<string>(nullable: true),
                    EACReferral = table.Column<string>(nullable: true),
                    EACReferralApp = table.Column<string>(nullable: true),
                    EACReferralExperience = table.Column<string>(nullable: true),
                    EACHomevisit = table.Column<string>(nullable: true),
                    EACAdherencePlan = table.Column<string>(nullable: true),
                    EACFollowupDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnhancedAdherenceCounsellingExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnhancedAdherenceCounsellingExtracts_PatientExtracts_SiteCod~",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GbvScreeningExtracts",
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
                    IPV = table.Column<string>(nullable: true),
                    PhysicalIPV = table.Column<string>(nullable: true),
                    EmotionalIPV = table.Column<string>(nullable: true),
                    SexualIPV = table.Column<string>(nullable: true),
                    IPVRelationship = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GbvScreeningExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GbvScreeningExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IITRiskScoresExtracts",
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
                    SourceSysUUID = table.Column<string>(nullable: true),
                    RiskScore = table.Column<string>(nullable: true),
                    RiskFactors = table.Column<string>(nullable: true),
                    RiskDescription = table.Column<string>(nullable: true),
                    RiskEvaluationDate = table.Column<DateTime>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IITRiskScoresExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IITRiskScoresExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IptExtracts",
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
                    OnTBDrugs = table.Column<string>(nullable: true),
                    OnIPT = table.Column<string>(nullable: true),
                    EverOnIPT = table.Column<string>(nullable: true),
                    Cough = table.Column<string>(nullable: true),
                    Fever = table.Column<string>(nullable: true),
                    NoticeableWeightLoss = table.Column<string>(nullable: true),
                    NightSweats = table.Column<string>(nullable: true),
                    Lethargy = table.Column<string>(nullable: true),
                    ICFActionTaken = table.Column<string>(nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    TBClinicalDiagnosis = table.Column<string>(nullable: true),
                    ContactsInvited = table.Column<string>(nullable: true),
                    EvaluatedForIPT = table.Column<string>(nullable: true),
                    StartAntiTBs = table.Column<string>(nullable: true),
                    TBRxStartDate = table.Column<DateTime>(nullable: true),
                    TBScreening = table.Column<string>(nullable: true),
                    IPTClientWorkUp = table.Column<string>(nullable: true),
                    StartIPT = table.Column<string>(nullable: true),
                    IndicationForIPT = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    TPTInitiationDate = table.Column<DateTime>(nullable: true),
                    IPTDiscontinuation = table.Column<string>(nullable: true),
                    DateOfDiscontinuation = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IptExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IptExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtzExtracts",
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
                    OTZEnrollmentDate = table.Column<DateTime>(nullable: true),
                    TransferInStatus = table.Column<string>(nullable: true),
                    ModulesPreviouslyCovered = table.Column<string>(nullable: true),
                    ModulesCompletedToday = table.Column<string>(nullable: true),
                    SupportGroupInvolvement = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    TransitionAttritionReason = table.Column<string>(nullable: true),
                    OutcomeDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtzExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtzExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OvcExtracts",
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
                    OVCEnrollmentDate = table.Column<DateTime>(nullable: true),
                    RelationshipToClient = table.Column<string>(nullable: true),
                    EnrolledinCPIMS = table.Column<string>(nullable: true),
                    CPIMSUniqueIdentifier = table.Column<string>(nullable: true),
                    PartnerOfferingOVCServices = table.Column<string>(nullable: true),
                    OVCExitReason = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvcExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OvcExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

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
                    AdverseEventStartDate = table.Column<DateTime>(nullable: true),
                    AdverseEventEndDate = table.Column<DateTime>(nullable: true),
                    Severity = table.Column<string>(nullable: true),
                    AdverseEventRegimen = table.Column<string>(nullable: true),
                    AdverseEventCause = table.Column<string>(nullable: true),
                    AdverseEventClinicalOutcome = table.Column<string>(nullable: true),
                    AdverseEventActionTaken = table.Column<string>(nullable: true),
                    AdverseEventIsPregnant = table.Column<bool>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAdverseEventExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientAdverseEventExtracts_PatientExtracts_SiteCode_Patient~",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
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
                    ExitDate = table.Column<DateTime>(nullable: true),
                    PreviousARTUse = table.Column<string>(nullable: true),
                    PreviousARTPurpose = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    DateLastUsed = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientArtExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientArtExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
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
                    m6CD4Date = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientBaselinesExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientBaselinesExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
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
                    Reason = table.Column<string>(nullable: true),
                    VisitId = table.Column<int>(nullable: true),
                    OrderedByDate = table.Column<DateTime>(nullable: true),
                    ReportedByDate = table.Column<DateTime>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    EnrollmentTest = table.Column<int>(nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    DateSampleTaken = table.Column<DateTime>(nullable: true),
                    SampleType = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientLaboratoryExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientLaboratoryExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
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
                    ProphylaxisType = table.Column<string>(nullable: true),
                    RegimenChangedSwitched = table.Column<string>(nullable: true),
                    RegimenChangeSwitchReason = table.Column<string>(nullable: true),
                    StopRegimenReason = table.Column<string>(nullable: true),
                    StopRegimenDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPharmacyExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPharmacyExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
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
                    ExitReason = table.Column<string>(nullable: true),
                    TOVerified = table.Column<string>(nullable: true),
                    TOVerifiedDate = table.Column<DateTime>(nullable: true),
                    ReEnrollmentDate = table.Column<DateTime>(nullable: true),
                    ReasonForDeath = table.Column<string>(nullable: true),
                    SpecificDeathReason = table.Column<string>(nullable: true),
                    DeathDate = table.Column<DateTime>(nullable: true),
                    EffectiveDiscontinuationDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientStatusExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientStatusExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
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
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    StabilityAssessment = table.Column<string>(nullable: true),
                    DifferentiatedCare = table.Column<string>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    KeyPopulationType = table.Column<string>(nullable: true),
                    VisitBy = table.Column<string>(nullable: true),
                    Temp = table.Column<decimal>(nullable: true),
                    PulseRate = table.Column<int>(nullable: true),
                    RespiratoryRate = table.Column<int>(nullable: true),
                    OxygenSaturation = table.Column<decimal>(nullable: true),
                    Muac = table.Column<int>(nullable: true),
                    NutritionalStatus = table.Column<string>(nullable: true),
                    EverHadMenses = table.Column<string>(nullable: true),
                    Breastfeeding = table.Column<string>(nullable: true),
                    Menopausal = table.Column<string>(nullable: true),
                    NoFPReason = table.Column<string>(nullable: true),
                    ProphylaxisUsed = table.Column<string>(nullable: true),
                    CTXAdherence = table.Column<string>(nullable: true),
                    CurrentRegimen = table.Column<string>(nullable: true),
                    HCWConcern = table.Column<string>(nullable: true),
                    TCAReason = table.Column<string>(nullable: true),
                    ClinicalNotes = table.Column<string>(nullable: true),
                    GeneralExamination = table.Column<string>(nullable: true),
                    SystemExamination = table.Column<string>(nullable: true),
                    Skin = table.Column<string>(nullable: true),
                    Eyes = table.Column<string>(nullable: true),
                    ENT = table.Column<string>(nullable: true),
                    Chest = table.Column<string>(nullable: true),
                    CVS = table.Column<string>(nullable: true),
                    Abdomen = table.Column<string>(nullable: true),
                    CNS = table.Column<string>(nullable: true),
                    Genitourinary = table.Column<string>(nullable: true),
                    RefillDate = table.Column<DateTime>(nullable: true),
                    ZScore = table.Column<string>(nullable: true),
                    PaedsDisclosure = table.Column<string>(nullable: true),
                    ZScoreAbsolute = table.Column<int>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVisitExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientVisitExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AncVisitExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    ANCClinicNumber = table.Column<string>(nullable: true),
                    ANCVisitNo = table.Column<int>(nullable: true),
                    GestationWeeks = table.Column<int>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true),
                    Temp = table.Column<decimal>(nullable: true),
                    PulseRate = table.Column<int>(nullable: true),
                    RespiratoryRate = table.Column<int>(nullable: true),
                    OxygenSaturation = table.Column<decimal>(nullable: true),
                    MUAC = table.Column<int>(nullable: true),
                    BP = table.Column<string>(nullable: true),
                    BreastExam = table.Column<string>(nullable: true),
                    AntenatalExercises = table.Column<string>(nullable: true),
                    FGM = table.Column<string>(nullable: true),
                    FGMComplications = table.Column<string>(nullable: true),
                    Haemoglobin = table.Column<decimal>(nullable: true),
                    DiabetesTest = table.Column<string>(nullable: true),
                    TBScreening = table.Column<string>(nullable: true),
                    CACxScreen = table.Column<string>(nullable: true),
                    CACxScreenMethod = table.Column<string>(nullable: true),
                    WHOStaging = table.Column<int>(nullable: true),
                    VLSampleTaken = table.Column<string>(nullable: true),
                    VLDate = table.Column<DateTime>(nullable: true),
                    VLResult = table.Column<string>(nullable: true),
                    SyphilisTreatment = table.Column<string>(nullable: true),
                    HIVStatusBeforeANC = table.Column<string>(nullable: true),
                    HIVTestingDone = table.Column<string>(nullable: true),
                    HIVTestType = table.Column<string>(nullable: true),
                    HIVTest1 = table.Column<string>(nullable: true),
                    HIVTest1Result = table.Column<string>(nullable: true),
                    HIVTest2 = table.Column<string>(nullable: true),
                    HIVTest2Result = table.Column<string>(nullable: true),
                    HIVTestFinalResult = table.Column<string>(nullable: true),
                    SyphilisTestDone = table.Column<string>(nullable: true),
                    SyphilisTestType = table.Column<string>(nullable: true),
                    SyphilisTestResults = table.Column<string>(nullable: true),
                    SyphilisTreated = table.Column<string>(nullable: true),
                    MotherProphylaxisGiven = table.Column<string>(nullable: true),
                    MotherGivenHAART = table.Column<DateTime>(nullable: true),
                    AZTBabyDispense = table.Column<string>(nullable: true),
                    NVPBabyDispense = table.Column<string>(nullable: true),
                    ChronicIllness = table.Column<string>(nullable: true),
                    CounselledOn = table.Column<string>(nullable: true),
                    PartnerHIVTestingANC = table.Column<string>(nullable: true),
                    PartnerHIVStatusANC = table.Column<string>(nullable: true),
                    PostParturmFP = table.Column<string>(nullable: true),
                    Deworming = table.Column<string>(nullable: true),
                    MalariaProphylaxis = table.Column<string>(nullable: true),
                    TetanusDose = table.Column<string>(nullable: true),
                    IronSupplementsGiven = table.Column<string>(nullable: true),
                    ReceivedMosquitoNet = table.Column<string>(nullable: true),
                    PreventiveServices = table.Column<string>(nullable: true),
                    UrinalysisVariables = table.Column<string>(nullable: true),
                    ReferredFrom = table.Column<string>(nullable: true),
                    ReferredTo = table.Column<string>(nullable: true),
                    ReferralReasons = table.Column<string>(nullable: true),
                    NextAppointmentANC = table.Column<DateTime>(nullable: true),
                    ClinicalNotes = table.Column<string>(nullable: true),
                    HepatitisBScreening = table.Column<string>(nullable: true),
                    TreatedHepatitisB = table.Column<string>(nullable: true),
                    PresumptiveTreatmentGiven = table.Column<string>(nullable: true),
                    PresumptiveTreatmentDose = table.Column<string>(nullable: true),
                    MiminumPackageOfCareReceived = table.Column<string>(nullable: true),
                    MiminumPackageOfCareServices = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AncVisitExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AncVisitExtracts_PatientMnchExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientMnchExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CwcEnrolmentExtracts",
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
                    Pkv = table.Column<string>(nullable: true),
                    PatientIDCWC = table.Column<string>(nullable: true),
                    HEIID = table.Column<string>(nullable: true),
                    MothersPkv = table.Column<string>(nullable: true),
                    RegistrationAtCWC = table.Column<DateTime>(nullable: true),
                    RegistrationAtHEI = table.Column<DateTime>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    Gestation = table.Column<DateTime>(nullable: true),
                    BirthWeight = table.Column<string>(nullable: true),
                    BirthLength = table.Column<decimal>(nullable: true),
                    BirthOrder = table.Column<int>(nullable: true),
                    BirthType = table.Column<string>(nullable: true),
                    PlaceOfDelivery = table.Column<string>(nullable: true),
                    ModeOfDelivery = table.Column<string>(nullable: true),
                    SpecialNeeds = table.Column<string>(nullable: true),
                    SpecialCare = table.Column<string>(nullable: true),
                    HEI = table.Column<string>(nullable: true),
                    MotherAlive = table.Column<string>(nullable: true),
                    MothersCCCNo = table.Column<string>(nullable: true),
                    TransferIn = table.Column<string>(nullable: true),
                    TransferInDate = table.Column<string>(nullable: true),
                    TransferredFrom = table.Column<string>(nullable: true),
                    HEIDate = table.Column<string>(nullable: true),
                    NVP = table.Column<string>(nullable: true),
                    BreastFeeding = table.Column<string>(nullable: true),
                    ReferredFrom = table.Column<string>(nullable: true),
                    ARTMother = table.Column<string>(nullable: true),
                    ARTRegimenMother = table.Column<string>(nullable: true),
                    ARTStartDateMother = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CwcEnrolmentExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CwcEnrolmentExtracts_PatientMnchExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientMnchExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CwcVisitExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true),
                    Temp = table.Column<decimal>(nullable: true),
                    PulseRate = table.Column<int>(nullable: true),
                    RespiratoryRate = table.Column<int>(nullable: true),
                    OxygenSaturation = table.Column<decimal>(nullable: true),
                    MUAC = table.Column<int>(nullable: true),
                    WeightCategory = table.Column<string>(nullable: true),
                    Stunted = table.Column<string>(nullable: true),
                    InfantFeeding = table.Column<string>(nullable: true),
                    MedicationGiven = table.Column<string>(nullable: true),
                    TBAssessment = table.Column<string>(nullable: true),
                    MNPsSupplementation = table.Column<string>(nullable: true),
                    Immunization = table.Column<string>(nullable: true),
                    DangerSigns = table.Column<string>(nullable: true),
                    Milestones = table.Column<string>(nullable: true),
                    VitaminA = table.Column<string>(nullable: true),
                    Disability = table.Column<string>(nullable: true),
                    ReceivedMosquitoNet = table.Column<string>(nullable: true),
                    Dewormed = table.Column<string>(nullable: true),
                    ReferredFrom = table.Column<string>(nullable: true),
                    ReferredTo = table.Column<string>(nullable: true),
                    ReferralReasons = table.Column<string>(nullable: true),
                    FollowUP = table.Column<string>(nullable: true),
                    NextAppointment = table.Column<DateTime>(nullable: true),
                    RevisitThisYear = table.Column<string>(nullable: true),
                    Refferred = table.Column<string>(nullable: true),
                    HeightLength = table.Column<decimal>(nullable: true),
                    ZScore = table.Column<string>(nullable: true),
                    ZScoreAbsolute = table.Column<int>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CwcVisitExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CwcVisitExtracts_PatientMnchExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientMnchExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeiExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    DNAPCR1Date = table.Column<DateTime>(nullable: true),
                    DNAPCR2Date = table.Column<DateTime>(nullable: true),
                    DNAPCR3Date = table.Column<DateTime>(nullable: true),
                    ConfirmatoryPCRDate = table.Column<DateTime>(nullable: true),
                    BasellineVLDate = table.Column<DateTime>(nullable: true),
                    FinalyAntibodyDate = table.Column<DateTime>(nullable: true),
                    DNAPCR1 = table.Column<string>(nullable: true),
                    DNAPCR2 = table.Column<string>(nullable: true),
                    DNAPCR3 = table.Column<string>(nullable: true),
                    ConfirmatoryPCR = table.Column<string>(nullable: true),
                    BasellineVL = table.Column<string>(nullable: true),
                    FinalyAntibody = table.Column<string>(nullable: true),
                    HEIExitDate = table.Column<DateTime>(nullable: true),
                    HEIHIVStatus = table.Column<string>(nullable: true),
                    HEIExitCritearia = table.Column<string>(nullable: true),
                    PatientHeiId = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeiExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeiExtracts_PatientMnchExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientMnchExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatVisitExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    AdmissionNumber = table.Column<string>(nullable: true),
                    ANCVisits = table.Column<int>(nullable: true),
                    DateOfDelivery = table.Column<DateTime>(nullable: true),
                    DurationOfDelivery = table.Column<int>(nullable: true),
                    GestationAtBirth = table.Column<int>(nullable: true),
                    ModeOfDelivery = table.Column<string>(nullable: true),
                    PlacentaComplete = table.Column<string>(nullable: true),
                    UterotonicGiven = table.Column<string>(nullable: true),
                    VaginalExamination = table.Column<string>(nullable: true),
                    BloodLoss = table.Column<int>(nullable: true),
                    BloodLossVisual = table.Column<string>(nullable: true),
                    ConditonAfterDelivery = table.Column<string>(nullable: true),
                    MaternalDeath = table.Column<DateTime>(nullable: true),
                    DeliveryComplications = table.Column<string>(nullable: true),
                    NoBabiesDelivered = table.Column<int>(nullable: true),
                    BabyBirthNumber = table.Column<int>(nullable: true),
                    SexBaby = table.Column<string>(nullable: true),
                    BirthWeight = table.Column<string>(nullable: true),
                    BirthOutcome = table.Column<string>(nullable: true),
                    BirthWithDeformity = table.Column<string>(nullable: true),
                    TetracyclineGiven = table.Column<string>(nullable: true),
                    InitiatedBF = table.Column<string>(nullable: true),
                    ApgarScore1 = table.Column<int>(nullable: true),
                    ApgarScore5 = table.Column<int>(nullable: true),
                    ApgarScore10 = table.Column<int>(nullable: true),
                    KangarooCare = table.Column<string>(nullable: true),
                    ChlorhexidineApplied = table.Column<string>(nullable: true),
                    VitaminKGiven = table.Column<string>(nullable: true),
                    StatusBabyDischarge = table.Column<string>(nullable: true),
                    MotherDischargeDate = table.Column<string>(nullable: true),
                    SyphilisTestResults = table.Column<string>(nullable: true),
                    HIVStatusLastANC = table.Column<string>(nullable: true),
                    HIVTestingDone = table.Column<string>(nullable: true),
                    HIVTest1 = table.Column<string>(nullable: true),
                    HIV1Results = table.Column<string>(nullable: true),
                    HIVTest2 = table.Column<string>(nullable: true),
                    HIV2Results = table.Column<string>(nullable: true),
                    HIVTestFinalResult = table.Column<string>(nullable: true),
                    OnARTANC = table.Column<string>(nullable: true),
                    BabyGivenProphylaxis = table.Column<string>(nullable: true),
                    MotherGivenCTX = table.Column<string>(nullable: true),
                    PartnerHIVTestingMAT = table.Column<string>(nullable: true),
                    PartnerHIVStatusMAT = table.Column<string>(nullable: true),
                    CounselledOn = table.Column<string>(nullable: true),
                    ReferredFrom = table.Column<string>(nullable: true),
                    ReferredTo = table.Column<string>(nullable: true),
                    ClinicalNotes = table.Column<string>(nullable: true),
                    LMP = table.Column<DateTime>(nullable: true),
                    EDD = table.Column<DateTime>(nullable: true),
                    MaternalDeathAudited = table.Column<string>(nullable: true),
                    ReferralReason = table.Column<string>(nullable: true),
                    OnARTMat = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatVisitExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatVisitExtracts_PatientMnchExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientMnchExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MnchArtExtracts",
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
                    Pkv = table.Column<string>(nullable: true),
                    PatientMnchID = table.Column<string>(nullable: true),
                    PatientHeiID = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    RegistrationAtCCC = table.Column<DateTime>(nullable: true),
                    StartARTDate = table.Column<DateTime>(nullable: true),
                    StartRegimen = table.Column<string>(nullable: true),
                    StartRegimenLine = table.Column<string>(nullable: true),
                    StatusAtCCC = table.Column<string>(nullable: true),
                    LastARTDate = table.Column<DateTime>(nullable: true),
                    LastRegimen = table.Column<string>(nullable: true),
                    LastRegimenLine = table.Column<string>(nullable: true),
                    FacilityReceivingARTCare = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MnchArtExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MnchArtExtracts_PatientMnchExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientMnchExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MnchEnrolmentExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    ServiceType = table.Column<string>(nullable: true),
                    EnrollmentDateAtMnch = table.Column<DateTime>(nullable: true),
                    MnchNumber = table.Column<DateTime>(nullable: true),
                    FirstVisitAnc = table.Column<DateTime>(nullable: true),
                    Parity = table.Column<string>(nullable: true),
                    Gravidae = table.Column<int>(nullable: false),
                    LMP = table.Column<DateTime>(nullable: true),
                    EDDFromLMP = table.Column<DateTime>(nullable: true),
                    HIVStatusBeforeANC = table.Column<string>(nullable: true),
                    HIVTestDate = table.Column<DateTime>(nullable: true),
                    PartnerHIVStatus = table.Column<string>(nullable: true),
                    PartnerHIVTestDate = table.Column<DateTime>(nullable: true),
                    BloodGroup = table.Column<string>(nullable: true),
                    StatusAtMnch = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MnchEnrolmentExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MnchEnrolmentExtracts_PatientMnchExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientMnchExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MnchImmunizationExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    BCG = table.Column<DateTime>(nullable: true),
                    OPVatBirth = table.Column<DateTime>(nullable: true),
                    OPV1 = table.Column<DateTime>(nullable: true),
                    OPV2 = table.Column<DateTime>(nullable: true),
                    OPV3 = table.Column<DateTime>(nullable: true),
                    IPV = table.Column<DateTime>(nullable: true),
                    DPTHepBHIB1 = table.Column<DateTime>(nullable: true),
                    DPTHepBHIB2 = table.Column<DateTime>(nullable: true),
                    DPTHepBHIB3 = table.Column<DateTime>(nullable: true),
                    PCV101 = table.Column<DateTime>(nullable: true),
                    PCV102 = table.Column<DateTime>(nullable: true),
                    PCV103 = table.Column<DateTime>(nullable: true),
                    ROTA1 = table.Column<DateTime>(nullable: true),
                    MeaslesReubella1 = table.Column<DateTime>(nullable: true),
                    YellowFever = table.Column<DateTime>(nullable: true),
                    MeaslesReubella2 = table.Column<DateTime>(nullable: true),
                    MeaslesAt6Months = table.Column<DateTime>(nullable: true),
                    ROTA2 = table.Column<DateTime>(nullable: true),
                    DateOfNextVisit = table.Column<DateTime>(nullable: true),
                    BCGScarChecked = table.Column<string>(nullable: true),
                    DateChecked = table.Column<DateTime>(nullable: true),
                    DateBCGrepeated = table.Column<DateTime>(nullable: true),
                    VitaminAAt6Months = table.Column<DateTime>(nullable: true),
                    VitaminAAt1Yr = table.Column<DateTime>(nullable: true),
                    VitaminAAt18Months = table.Column<DateTime>(nullable: true),
                    VitaminAAt2Years = table.Column<DateTime>(nullable: true),
                    VitaminAAt2To5Years = table.Column<DateTime>(nullable: true),
                    FullyImmunizedChild = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MnchImmunizationExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MnchImmunizationExtracts_PatientMnchExtracts_SiteCode_Patien~",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientMnchExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MnchLabExtracts",
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
                    PatientMNCH_ID = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    SatelliteName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    OrderedbyDate = table.Column<DateTime>(nullable: true),
                    ReportedbyDate = table.Column<DateTime>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    LabReason = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MnchLabExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MnchLabExtracts_PatientMnchExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientMnchExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherBabyPairExtracts",
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
                    BabyPatientPK = table.Column<int>(nullable: false),
                    MotherPatientPK = table.Column<int>(nullable: false),
                    BabyPatientMncHeiID = table.Column<string>(nullable: true),
                    MotherPatientMncHeiID = table.Column<string>(nullable: true),
                    PatientIDCCC = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBabyPairExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherBabyPairExtracts_PatientMnchExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientMnchExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PncVisitExtracts",
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
                    PatientMnchID = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    PNCRegisterNumber = table.Column<string>(nullable: true),
                    PNCVisitNo = table.Column<int>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: true),
                    ModeOfDelivery = table.Column<string>(nullable: true),
                    PlaceOfDelivery = table.Column<string>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true),
                    Temp = table.Column<decimal>(nullable: true),
                    PulseRate = table.Column<int>(nullable: true),
                    RespiratoryRate = table.Column<int>(nullable: true),
                    OxygenSaturation = table.Column<decimal>(nullable: true),
                    MUAC = table.Column<int>(nullable: true),
                    BP = table.Column<int>(nullable: true),
                    BreastExam = table.Column<string>(nullable: true),
                    GeneralCondition = table.Column<string>(nullable: true),
                    HasPallor = table.Column<string>(nullable: true),
                    Pallor = table.Column<string>(nullable: true),
                    Breast = table.Column<string>(nullable: true),
                    PPH = table.Column<string>(nullable: true),
                    CSScar = table.Column<string>(nullable: true),
                    UterusInvolution = table.Column<string>(nullable: true),
                    Episiotomy = table.Column<string>(nullable: true),
                    Lochia = table.Column<string>(nullable: true),
                    Fistula = table.Column<string>(nullable: true),
                    MaternalComplications = table.Column<string>(nullable: true),
                    TBScreening = table.Column<string>(nullable: true),
                    ClientScreenedCACx = table.Column<string>(nullable: true),
                    CACxScreenMethod = table.Column<string>(nullable: true),
                    CACxScreenResults = table.Column<string>(nullable: true),
                    PriorHIVStatus = table.Column<string>(nullable: true),
                    HIVTestingDone = table.Column<string>(nullable: true),
                    HIVTest1 = table.Column<string>(nullable: true),
                    HIVTest1Result = table.Column<string>(nullable: true),
                    HIVTest2 = table.Column<string>(nullable: true),
                    HIVTest2Result = table.Column<string>(nullable: true),
                    HIVTestFinalResult = table.Column<string>(nullable: true),
                    InfantProphylaxisGiven = table.Column<string>(nullable: true),
                    MotherProphylaxisGiven = table.Column<string>(nullable: true),
                    CoupleCounselled = table.Column<string>(nullable: true),
                    PartnerHIVTestingPNC = table.Column<string>(nullable: true),
                    PartnerHIVResultPNC = table.Column<string>(nullable: true),
                    CounselledOnFP = table.Column<string>(nullable: true),
                    ReceivedFP = table.Column<string>(nullable: true),
                    HaematinicsGiven = table.Column<string>(nullable: true),
                    DeliveryOutcome = table.Column<string>(nullable: true),
                    BabyConditon = table.Column<string>(nullable: true),
                    BabyFeeding = table.Column<string>(nullable: true),
                    UmbilicalCord = table.Column<string>(nullable: true),
                    Immunization = table.Column<string>(nullable: true),
                    InfantFeeding = table.Column<string>(nullable: true),
                    PreventiveServices = table.Column<string>(nullable: true),
                    ReferredFrom = table.Column<string>(nullable: true),
                    ReferredTo = table.Column<string>(nullable: true),
                    NextAppointmentPNC = table.Column<DateTime>(nullable: true),
                    ClinicalNotes = table.Column<string>(nullable: true),
                    VisitTimingMother = table.Column<string>(nullable: true),
                    VisitTimingBaby = table.Column<string>(nullable: true),
                    MotherCameForHIVTest = table.Column<string>(nullable: true),
                    InfactCameForHAART = table.Column<string>(nullable: true),
                    MotherGivenHAART = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PncVisitExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PncVisitExtracts_PatientMnchExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientMnchExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepAdverseEventExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    AdverseEvent = table.Column<string>(nullable: true),
                    AdverseEventStartDate = table.Column<DateTime>(nullable: true),
                    AdverseEventEndDate = table.Column<DateTime>(nullable: true),
                    Severity = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    AdverseEventActionTaken = table.Column<string>(nullable: true),
                    AdverseEventClinicalOutcome = table.Column<string>(nullable: true),
                    AdverseEventIsPregnant = table.Column<string>(nullable: true),
                    AdverseEventCause = table.Column<string>(nullable: true),
                    AdverseEventRegimen = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepAdverseEventExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepAdverseEventExtracts_PatientPrepExtracts_SiteCode_Patien~",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientPrepExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepBehaviourRiskExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    SexPartnerHIVStatus = table.Column<string>(nullable: true),
                    IsHIVPositivePartnerCurrentonART = table.Column<string>(nullable: true),
                    IsPartnerHighrisk = table.Column<string>(nullable: true),
                    PartnerARTRisk = table.Column<string>(nullable: true),
                    ClientAssessments = table.Column<string>(nullable: true),
                    ClientRisk = table.Column<string>(nullable: true),
                    ClientWillingToTakePrep = table.Column<string>(nullable: true),
                    PrEPDeclineReason = table.Column<string>(nullable: true),
                    RiskReductionEducationOffered = table.Column<string>(nullable: true),
                    ReferralToOtherPrevServices = table.Column<string>(nullable: true),
                    FirstEstablishPartnerStatus = table.Column<DateTime>(nullable: true),
                    PartnerEnrolledtoCCC = table.Column<DateTime>(nullable: true),
                    HIVPartnerCCCnumber = table.Column<string>(nullable: true),
                    HIVPartnerARTStartDate = table.Column<DateTime>(nullable: true),
                    MonthsknownHIVSerodiscordant = table.Column<string>(nullable: true),
                    SexWithoutCondom = table.Column<string>(nullable: true),
                    NumberofchildrenWithPartner = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepBehaviourRiskExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepBehaviourRiskExtracts_PatientPrepExtracts_SiteCode_Patie~",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientPrepExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepCareTerminationExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    DateOfLastPrepDose = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepCareTerminationExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepCareTerminationExtracts_PatientPrepExtracts_SiteCode_Pat~",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientPrepExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepLabExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    SampleDate = table.Column<DateTime>(nullable: true),
                    TestResultDate = table.Column<DateTime>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepLabExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepLabExtracts_PatientPrepExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientPrepExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepPharmacyExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    RegimenPrescribed = table.Column<string>(nullable: true),
                    DispenseDate = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepPharmacyExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepPharmacyExtracts_PatientPrepExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientPrepExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepVisitExtracts",
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
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    EncounterId = table.Column<string>(nullable: true),
                    VisitID = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    BloodPressure = table.Column<string>(nullable: true),
                    Temperature = table.Column<string>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    BMI = table.Column<decimal>(nullable: true),
                    STIScreening = table.Column<string>(nullable: true),
                    STISymptoms = table.Column<string>(nullable: true),
                    STITreated = table.Column<string>(nullable: true),
                    Circumcised = table.Column<string>(nullable: true),
                    VMMCReferral = table.Column<string>(nullable: true),
                    LMP = table.Column<DateTime>(nullable: true),
                    MenopausalStatus = table.Column<string>(nullable: true),
                    PregnantAtThisVisit = table.Column<string>(nullable: true),
                    EDD = table.Column<DateTime>(nullable: true),
                    PlanningToGetPregnant = table.Column<string>(nullable: true),
                    PregnancyPlanned = table.Column<string>(nullable: true),
                    PregnancyEnded = table.Column<string>(nullable: true),
                    PregnancyEndDate = table.Column<DateTime>(nullable: true),
                    PregnancyOutcome = table.Column<string>(nullable: true),
                    BirthDefects = table.Column<string>(nullable: true),
                    Breastfeeding = table.Column<string>(nullable: true),
                    FamilyPlanningStatus = table.Column<string>(nullable: true),
                    FPMethods = table.Column<string>(nullable: true),
                    AdherenceDone = table.Column<string>(nullable: true),
                    AdherenceOutcome = table.Column<string>(nullable: true),
                    AdherenceReasons = table.Column<string>(nullable: true),
                    SymptomsAcuteHIV = table.Column<string>(nullable: true),
                    ContraindicationsPrep = table.Column<string>(nullable: true),
                    PrepTreatmentPlan = table.Column<string>(nullable: true),
                    PrepPrescribed = table.Column<string>(nullable: true),
                    RegimenPrescribed = table.Column<string>(nullable: true),
                    MonthsPrescribed = table.Column<string>(nullable: true),
                    CondomsIssued = table.Column<string>(nullable: true),
                    Tobegivennextappointment = table.Column<string>(nullable: true),
                    Reasonfornotgivingnextappointment = table.Column<string>(nullable: true),
                    HepatitisBPositiveResult = table.Column<string>(nullable: true),
                    HepatitisCPositiveResult = table.Column<string>(nullable: true),
                    VaccinationForHepBStarted = table.Column<string>(nullable: true),
                    TreatedForHepB = table.Column<string>(nullable: true),
                    VaccinationForHepCStarted = table.Column<string>(nullable: true),
                    TreatedForHepC = table.Column<string>(nullable: true),
                    NextAppointment = table.Column<DateTime>(nullable: true),
                    ClinicalNotes = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepVisitExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepVisitExtracts_PatientPrepExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientPrepExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_AllergiesChronicIllnessExtracts_SiteCode_PatientPK",
                table: "AllergiesChronicIllnessExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_AncVisitExtracts_SiteCode_PatientPK",
                table: "AncVisitExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_CervicalCancerScreeningExtracts_SiteCode_PatientPK",
                table: "CervicalCancerScreeningExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_ContactListingExtracts_SiteCode_PatientPK",
                table: "ContactListingExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_CovidExtracts_SiteCode_PatientPK",
                table: "CovidExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_CwcEnrolmentExtracts_SiteCode_PatientPK",
                table: "CwcEnrolmentExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_CwcVisitExtracts_SiteCode_PatientPK",
                table: "CwcVisitExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_DefaulterTracingExtracts_SiteCode_PatientPK",
                table: "DefaulterTracingExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_DepressionScreeningExtracts_SiteCode_PatientPK",
                table: "DepressionScreeningExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_DrugAlcoholScreeningExtracts_SiteCode_PatientPK",
                table: "DrugAlcoholScreeningExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_EnhancedAdherenceCounsellingExtracts_SiteCode_PatientPK",
                table: "EnhancedAdherenceCounsellingExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_GbvScreeningExtracts_SiteCode_PatientPK",
                table: "GbvScreeningExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_HeiExtracts_SiteCode_PatientPK",
                table: "HeiExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsClientsLinkageExtracts_SiteCode_PatientPk",
                table: "HtsClientsLinkageExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsClientTestsExtracts_SiteCode_PatientPk",
                table: "HtsClientTestsExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsClientTracingExtracts_SiteCode_PatientPk",
                table: "HtsClientTracingExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsEligibilityExtracts_SiteCode_PatientPk",
                table: "HtsEligibilityExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsPartnerNotificationServicesExtracts_SiteCode_PatientPk",
                table: "HtsPartnerNotificationServicesExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsPartnerTracingExtracts_SiteCode_PatientPk",
                table: "HtsPartnerTracingExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsTestKitsExtracts_SiteCode_PatientPk",
                table: "HtsTestKitsExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_IITRiskScoresExtracts_SiteCode_PatientPK",
                table: "IITRiskScoresExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_IptExtracts_SiteCode_PatientPK",
                table: "IptExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_MatVisitExtracts_SiteCode_PatientPK",
                table: "MatVisitExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_MnchArtExtracts_SiteCode_PatientPK",
                table: "MnchArtExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_MnchEnrolmentExtracts_SiteCode_PatientPK",
                table: "MnchEnrolmentExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_MnchImmunizationExtracts_SiteCode_PatientPK",
                table: "MnchImmunizationExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_MnchLabExtracts_SiteCode_PatientPK",
                table: "MnchLabExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_MotherBabyPairExtracts_SiteCode_PatientPK",
                table: "MotherBabyPairExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_OtzExtracts_SiteCode_PatientPK",
                table: "OtzExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_OvcExtracts_SiteCode_PatientPK",
                table: "OvcExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientAdverseEventExtracts_SiteCode_PatientPK",
                table: "PatientAdverseEventExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientArtExtracts_SiteCode_PatientPK",
                table: "PatientArtExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientBaselinesExtracts_SiteCode_PatientPK",
                table: "PatientBaselinesExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientLaboratoryExtracts_SiteCode_PatientPK",
                table: "PatientLaboratoryExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientPharmacyExtracts_SiteCode_PatientPK",
                table: "PatientPharmacyExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientStatusExtracts_SiteCode_PatientPK",
                table: "PatientStatusExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientVisitExtracts_SiteCode_PatientPK",
                table: "PatientVisitExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PncVisitExtracts_SiteCode_PatientPK",
                table: "PncVisitExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepAdverseEventExtracts_SiteCode_PatientPK",
                table: "PrepAdverseEventExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepBehaviourRiskExtracts_SiteCode_PatientPK",
                table: "PrepBehaviourRiskExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepCareTerminationExtracts_SiteCode_PatientPK",
                table: "PrepCareTerminationExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepLabExtracts_SiteCode_PatientPK",
                table: "PrepLabExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepPharmacyExtracts_SiteCode_PatientPK",
                table: "PrepPharmacyExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepVisitExtracts_SiteCode_PatientPK",
                table: "PrepVisitExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_ValidationError_ValidatorId",
                table: "ValidationError",
                column: "ValidatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergiesChronicIllnessExtracts");

            migrationBuilder.DropTable(
                name: "AncVisitExtracts");

            migrationBuilder.DropTable(
                name: "CervicalCancerScreeningExtracts");

            migrationBuilder.DropTable(
                name: "ClientRegistryExtracts");

            migrationBuilder.DropTable(
                name: "ContactListingExtracts");

            migrationBuilder.DropTable(
                name: "CovidExtracts");

            migrationBuilder.DropTable(
                name: "CwcEnrolmentExtracts");

            migrationBuilder.DropTable(
                name: "CwcVisitExtracts");

            migrationBuilder.DropTable(
                name: "DefaulterTracingExtracts");

            migrationBuilder.DropTable(
                name: "DepressionScreeningExtracts");

            migrationBuilder.DropTable(
                name: "DiffLogs");

            migrationBuilder.DropTable(
                name: "DrugAlcoholScreeningExtracts");

            migrationBuilder.DropTable(
                name: "EmrMetrics");

            migrationBuilder.DropTable(
                name: "EnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropTable(
                name: "ExtractHistory");

            migrationBuilder.DropTable(
                name: "GbvScreeningExtracts");

            migrationBuilder.DropTable(
                name: "HeiExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientLinkageExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientPartnerExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientsLinkageExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientTestsExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientTracingExtracts");

            migrationBuilder.DropTable(
                name: "HtsEligibilityExtracts");

            migrationBuilder.DropTable(
                name: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropTable(
                name: "HtsPartnerTracingExtracts");

            migrationBuilder.DropTable(
                name: "HtsTestKitsExtracts");

            migrationBuilder.DropTable(
                name: "IITRiskScoresExtracts");

            migrationBuilder.DropTable(
                name: "IndicatorExtracts");

            migrationBuilder.DropTable(
                name: "IptExtracts");

            migrationBuilder.DropTable(
                name: "MasterPatientIndices");

            migrationBuilder.DropTable(
                name: "MatVisitExtracts");

            migrationBuilder.DropTable(
                name: "MetricMigrationExtracts");

            migrationBuilder.DropTable(
                name: "MnchArtExtracts");

            migrationBuilder.DropTable(
                name: "MnchEnrolmentExtracts");

            migrationBuilder.DropTable(
                name: "MnchImmunizationExtracts");

            migrationBuilder.DropTable(
                name: "MnchLabExtracts");

            migrationBuilder.DropTable(
                name: "MotherBabyPairExtracts");

            migrationBuilder.DropTable(
                name: "OtzExtracts");

            migrationBuilder.DropTable(
                name: "OvcExtracts");

            migrationBuilder.DropTable(
                name: "PatientAdverseEventExtracts");

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
                name: "PncVisitExtracts");

            migrationBuilder.DropTable(
                name: "PrepAdverseEventExtracts");

            migrationBuilder.DropTable(
                name: "PrepBehaviourRiskExtracts");

            migrationBuilder.DropTable(
                name: "PrepCareTerminationExtracts");

            migrationBuilder.DropTable(
                name: "PrepLabExtracts");

            migrationBuilder.DropTable(
                name: "PrepPharmacyExtracts");

            migrationBuilder.DropTable(
                name: "PrepVisitExtracts");

            migrationBuilder.DropTable(
                name: "PsmartStage");

            migrationBuilder.DropTable(
                name: "TempAllergiesChronicIllnessExtracts");

            migrationBuilder.DropTable(
                name: "TempAncVisitExtracts");

            migrationBuilder.DropTable(
                name: "TempCervicalCancerScreeningExtracts");

            migrationBuilder.DropTable(
                name: "TempClientRegistryExtracts");

            migrationBuilder.DropTable(
                name: "TempContactListingExtracts");

            migrationBuilder.DropTable(
                name: "TempCovidExtracts");

            migrationBuilder.DropTable(
                name: "TempCwcEnrolmentExtracts");

            migrationBuilder.DropTable(
                name: "TempCwcVisitExtracts");

            migrationBuilder.DropTable(
                name: "TempDefaulterTracingExtracts");

            migrationBuilder.DropTable(
                name: "TempDepressionScreeningExtracts");

            migrationBuilder.DropTable(
                name: "TempDrugAlcoholScreeningExtracts");

            migrationBuilder.DropTable(
                name: "TempEnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropTable(
                name: "TempGbvScreeningExtracts");

            migrationBuilder.DropTable(
                name: "TempHeiExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientLinkageExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientPartnerExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientsExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientsLinkageExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientTestsExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientTracingExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsEligibilityExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsPartnerTracingExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsTestKitsExtracts");

            migrationBuilder.DropTable(
                name: "TempIITRiskScoresExtracts");

            migrationBuilder.DropTable(
                name: "TempIndicatorExtracts");

            migrationBuilder.DropTable(
                name: "TempIptExtracts");

            migrationBuilder.DropTable(
                name: "TempMasterPatientIndices");

            migrationBuilder.DropTable(
                name: "TempMatVisitExtracts");

            migrationBuilder.DropTable(
                name: "TempMetricMigrationExtracts");

            migrationBuilder.DropTable(
                name: "TempMnchArtExtracts");

            migrationBuilder.DropTable(
                name: "TempMnchEnrolmentExtracts");

            migrationBuilder.DropTable(
                name: "TempMnchImmunizationExtracts");

            migrationBuilder.DropTable(
                name: "TempMnchLabExtracts");

            migrationBuilder.DropTable(
                name: "TempMotherBabyPairExtracts");

            migrationBuilder.DropTable(
                name: "TempOtzExtracts");

            migrationBuilder.DropTable(
                name: "TempOvcExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientAdverseEventExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientArtExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientBaselinesExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientMnchExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientPharmacyExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientPrepExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientStatusExtracts");

            migrationBuilder.DropTable(
                name: "TempPatientVisitExtracts");

            migrationBuilder.DropTable(
                name: "TempPncVisitExtracts");

            migrationBuilder.DropTable(
                name: "TempPrepAdverseEventExtracts");

            migrationBuilder.DropTable(
                name: "TempPrepBehaviourRiskExtracts");

            migrationBuilder.DropTable(
                name: "TempPrepCareTerminationExtracts");

            migrationBuilder.DropTable(
                name: "TempPrepLabExtracts");

            migrationBuilder.DropTable(
                name: "TempPrepPharmacyExtracts");

            migrationBuilder.DropTable(
                name: "TempPrepVisitExtracts");

            migrationBuilder.DropTable(
                name: "ValidationError");

            migrationBuilder.DropTable(
                name: "HtsClientsExtracts");

            migrationBuilder.DropTable(
                name: "PatientExtracts");

            migrationBuilder.DropTable(
                name: "PatientMnchExtracts");

            migrationBuilder.DropTable(
                name: "PatientPrepExtracts");

            migrationBuilder.DropTable(
                name: "Validator");
        }
    }
}
