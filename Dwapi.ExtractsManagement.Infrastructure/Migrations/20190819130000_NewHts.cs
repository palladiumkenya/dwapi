using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class NewHts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PatientAdverseEventExtracts_PatientExtracts_SiteCode_Patient~",
            //    table: "PatientAdverseEventExtracts");

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
                    DoB = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    PopulationType = table.Column<string>(nullable: true),
                    KeyPopulationType = table.Column<string>(nullable: true),
                    PatientDisabled = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    SubCounty = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientsExtracts", x => x.Id);
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
                    DatePrefferedToBeEnrolled = table.Column<DateTime>(nullable: false),
                    FacilityReferredTo = table.Column<string>(nullable: true),
                    HandedOverTo = table.Column<string>(nullable: true),
                    HandedOverToCadre = table.Column<string>(nullable: true),
                    EnrolledFacilityName = table.Column<string>(nullable: true),
                    ReferralDate = table.Column<DateTime>(nullable: false),
                    DateEnrolled = table.Column<DateTime>(nullable: false),
                    ReportedCCCNumber = table.Column<string>(nullable: true),
                    ReportedStartARTDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientsLinkageExtracts", x => x.Id);
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
                    TestDate = table.Column<DateTime>(nullable: false),
                    EverTestedForHiv = table.Column<string>(nullable: true),
                    MonthsSinceLastTest = table.Column<int>(nullable: false),
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
                    Consent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientTestsExtracts", x => x.Id);
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
                    TracingType = table.Column<DateTime>(nullable: false),
                    TracingDate = table.Column<DateTime>(nullable: false),
                    TracingOutcome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientTracingExtracts", x => x.Id);
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
                    EncounterId = table.Column<int>(nullable: true),
                    TestDate = table.Column<DateTime>(nullable: false),
                    EverTestedForHiv = table.Column<string>(nullable: true),
                    MonthsSinceLastTest = table.Column<int>(nullable: false),
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
                    Consent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsPartnerNotificationServicesExtracts", x => x.Id);
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
                    TraceDate = table.Column<DateTime>(nullable: false),
                    TraceOutcome = table.Column<string>(nullable: true),
                    BookingDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsPartnerTracingExtracts", x => x.Id);
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
                    EncounterId = table.Column<int>(nullable: false),
                    TestKitName1 = table.Column<string>(nullable: true),
                    TestKitLotNumber1 = table.Column<string>(nullable: true),
                    TestKitExpiry1 = table.Column<string>(nullable: true),
                    TestResult1 = table.Column<string>(nullable: true),
                    TestKitName2 = table.Column<string>(nullable: true),
                    TestKitLotNumber2 = table.Column<string>(nullable: true),
                    TestKitExpiry2 = table.Column<string>(nullable: true),
                    TestResult2 = table.Column<string>(nullable: true),
                    TestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsTestKitsExtracts", x => x.Id);
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
                    Ward = table.Column<string>(nullable: true)
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
                    ReportedStartARTDate = table.Column<DateTime>(nullable: true)
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
                    MonthsSinceLastTest = table.Column<int>(nullable: false),
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
                    Consent = table.Column<string>(nullable: true)
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
                    TracingType = table.Column<DateTime>(nullable: true),
                    TracingDate = table.Column<DateTime>(nullable: true),
                    TracingOutcome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsClientTracingExtracts", x => x.Id);
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
                    MaritalStatus = table.Column<string>(nullable: true)
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
                    BookingDate = table.Column<DateTime>(nullable: true)
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
                    EncounterId = table.Column<int>(nullable: false),
                    TestKitName1 = table.Column<string>(nullable: true),
                    TestKitLotNumber1 = table.Column<string>(nullable: true),
                    TestKitExpiry1 = table.Column<string>(nullable: true),
                    TestResult1 = table.Column<string>(nullable: true),
                    TestKitName2 = table.Column<string>(nullable: true),
                    TestKitLotNumber2 = table.Column<string>(nullable: true),
                    TestKitExpiry2 = table.Column<string>(nullable: true),
                    TestResult2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsTestKitsExtracts", x => x.Id);
                });

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PatientAdverseEventExtracts_PatientExtracts_SiteCode_PatientPK",
            //    table: "PatientAdverseEventExtracts",
            //    columns: new[] { "SiteCode", "PatientPK" },
            //    principalTable: "PatientExtracts",
            //    principalColumns: new[] { "SiteCode", "PatientPK" },
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PatientAdverseEventExtracts_PatientExtracts_SiteCode_PatientPK",
            //    table: "PatientAdverseEventExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientsExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientsLinkageExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientTestsExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientTracingExtracts");

            migrationBuilder.DropTable(
                name: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropTable(
                name: "HtsPartnerTracingExtracts");

            migrationBuilder.DropTable(
                name: "HtsTestKitsExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientsExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientsLinkageExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientTestsExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientTracingExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsPartnerTracingExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsTestKitsExtracts");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PatientAdverseEventExtracts_PatientExtracts_SiteCode_Patient~",
            //    table: "PatientAdverseEventExtracts",
            //    columns: new[] { "SiteCode", "PatientPK" },
            //    principalTable: "PatientExtracts",
            //    principalColumns: new[] { "SiteCode", "PatientPK" },
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
