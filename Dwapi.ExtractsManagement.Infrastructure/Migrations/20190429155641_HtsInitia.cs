using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsInitia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    PatientConsented = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientExtracts", x => new { x.SiteCode, x.PatientPk, x.EncounterId });
                    table.UniqueConstraint("AK_HtsClientExtracts_Id", x => x.Id);
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
                    PatientConsented = table.Column<string>(nullable: true)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HtsClientExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientLinkageExtracts");

            migrationBuilder.DropTable(
                name: "HtsClientPartnerExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientLinkageExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsClientPartnerExtracts");
        }
    }
}
