using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsEligibilityInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    VisitID = table.Column<string>(nullable: true),
                    EncounterId = table.Column<string>(nullable: true),
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
                    DateTested = table.Column<DateTime>(nullable: true),
                    StartedOnART = table.Column<string>(nullable: true),
                    CCCNumber = table.Column<string>(nullable: true),
                    EverHadSex = table.Column<string>(nullable: true),
                    SexuallyActive = table.Column<string>(nullable: true),
                    NewPartner = table.Column<string>(nullable: true),
                    PartnerHivStatus = table.Column<string>(nullable: true),
                    CoupleDiscordant = table.Column<string>(nullable: true),
                    MultiplePartners = table.Column<string>(nullable: true),
                    NumberPartners = table.Column<string>(nullable: true),
                    AlcoholSex = table.Column<string>(nullable: true),
                    MoneySex = table.Column<string>(nullable: true),
                    CondomBurst = table.Column<string>(nullable: true),
                    UnknownStatusPartner = table.Column<string>(nullable: true),
                    KnownStatusPartner = table.Column<string>(nullable: true),
                    Pregnant = table.Column<string>(nullable: true),
                    BreastfeedingMother = table.Column<string>(nullable: true),
                    ExperiencedGBV = table.Column<string>(nullable: true),
                    PhysicalViolence = table.Column<string>(nullable: true),
                    SexualViolence = table.Column<string>(nullable: true),
                    EverOnPrep = table.Column<string>(nullable: true),
                    CurrentlyOnPrep = table.Column<string>(nullable: true),
                    EverOnPep = table.Column<string>(nullable: true),
                    CurrentlyOnPep = table.Column<string>(nullable: true),
                    EverHadSTI = table.Column<string>(nullable: true),
                    CurrentlyHasSTI = table.Column<string>(nullable: true),
                    EverHadTB = table.Column<string>(nullable: true),
                    CurrentlyHasTB = table.Column<string>(nullable: true),
                    SharedNeedle = table.Column<string>(nullable: true),
                    NeedleStickInjuries = table.Column<string>(nullable: true),
                    TraditionalProcedures = table.Column<string>(nullable: true),
                    ChildReasonsForIneligibility = table.Column<string>(nullable: true),
                    EligibleForTest = table.Column<string>(nullable: true),
                    ReasonsForIneligibility = table.Column<string>(nullable: true),
                    SpecificReasonForIneligibility = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsEligibilityExtracts", x => x.Id);
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
                    VisitID = table.Column<string>(nullable: true),
                    EncounterId = table.Column<string>(nullable: true),
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
                    DateTested = table.Column<DateTime>(nullable: true),
                    StartedOnART = table.Column<string>(nullable: true),
                    CCCNumber = table.Column<string>(nullable: true),
                    EverHadSex = table.Column<string>(nullable: true),
                    SexuallyActive = table.Column<string>(nullable: true),
                    NewPartner = table.Column<string>(nullable: true),
                    PartnerHivStatus = table.Column<string>(nullable: true),
                    CoupleDiscordant = table.Column<string>(nullable: true),
                    MultiplePartners = table.Column<string>(nullable: true),
                    NumberPartners = table.Column<string>(nullable: true),
                    AlcoholSex = table.Column<string>(nullable: true),
                    MoneySex = table.Column<string>(nullable: true),
                    CondomBurst = table.Column<string>(nullable: true),
                    UnknownStatusPartner = table.Column<string>(nullable: true),
                    KnownStatusPartner = table.Column<string>(nullable: true),
                    Pregnant = table.Column<string>(nullable: true),
                    BreastfeedingMother = table.Column<string>(nullable: true),
                    ExperiencedGBV = table.Column<string>(nullable: true),
                    PhysicalViolence = table.Column<string>(nullable: true),
                    SexualViolence = table.Column<string>(nullable: true),
                    EverOnPrep = table.Column<string>(nullable: true),
                    CurrentlyOnPrep = table.Column<string>(nullable: true),
                    EverOnPep = table.Column<string>(nullable: true),
                    CurrentlyOnPep = table.Column<string>(nullable: true),
                    EverHadSTI = table.Column<string>(nullable: true),
                    CurrentlyHasSTI = table.Column<string>(nullable: true),
                    EverHadTB = table.Column<string>(nullable: true),
                    CurrentlyHasTB = table.Column<string>(nullable: true),
                    SharedNeedle = table.Column<string>(nullable: true),
                    NeedleStickInjuries = table.Column<string>(nullable: true),
                    TraditionalProcedures = table.Column<string>(nullable: true),
                    ChildReasonsForIneligibility = table.Column<string>(nullable: true),
                    EligibleForTest = table.Column<string>(nullable: true),
                    ReasonsForIneligibility = table.Column<string>(nullable: true),
                    SpecificReasonForIneligibility = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHtsEligibilityExtracts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HtsEligibilityExtracts");

            migrationBuilder.DropTable(
                name: "TempHtsEligibilityExtracts");
        }
    }
}
