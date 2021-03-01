using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class NewCT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
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
                    Genitourinary = table.Column<string>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    PartnerPersonID = table.Column<int>(nullable: true),
                    ContactAge = table.Column<string>(nullable: true),
                    ContactSex = table.Column<string>(nullable: true),
                    ContactMaritalStatus = table.Column<string>(nullable: true),
                    RelationshipWithPatient = table.Column<string>(nullable: true),
                    ScreenedForIpv = table.Column<string>(nullable: true),
                    IpvScreening = table.Column<string>(nullable: true),
                    IpvScreeningOutcome = table.Column<string>(nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(nullable: true),
                    PnsApproach = table.Column<string>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
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
                    PHQ_9_Rating = table.Column<string>(nullable: true),
                    DepressionAssesmentScore = table.Column<int>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    DrinkAlcohol = table.Column<string>(nullable: true),
                    Smoking = table.Column<string>(nullable: true),
                    DrugUse = table.Column<string>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    SessionNumber = table.Column<int>(nullable: true),
                    DateoffirstSession = table.Column<DateTime>(nullable: true),
                    PillCountAdherence = table.Column<int>(nullable: true),
                    MMAS4_1 = table.Column<string>(nullable: true),
                    MMAS4_2 = table.Column<string>(nullable: true),
                    MMAS4_3 = table.Column<string>(nullable: true),
                    MMAS4_4 = table.Column<string>(nullable: true),
                    MMSA8_1 = table.Column<string>(nullable: true),
                    MMSA4_2 = table.Column<string>(nullable: true),
                    MMSA4_3 = table.Column<string>(nullable: true),
                    MMSA4_4 = table.Column<string>(nullable: true),
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
                    EACFollowupDate = table.Column<DateTime>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    IPV = table.Column<string>(nullable: true),
                    PhysicalIPV = table.Column<string>(nullable: true),
                    EmotionalIPV = table.Column<string>(nullable: true),
                    SexualIPV = table.Column<string>(nullable: true),
                    IPVRelationship = table.Column<string>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
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
                    Lethergy = table.Column<string>(nullable: true),
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
                    IndicationForIPT = table.Column<string>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
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
                    OutcomeDate = table.Column<DateTime>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    OVCEnrollmentDate = table.Column<DateTime>(nullable: true),
                    RelationshipToClient = table.Column<string>(nullable: true),
                    EnrolledinCPIMS = table.Column<string>(nullable: true),
                    CPIMSUniqueIdentifier = table.Column<string>(nullable: true),
                    PartnerOfferingOVCServices = table.Column<string>(nullable: true),
                    OVCExitReason = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempAllergiesChronicIllnessExtracts", x => x.Id);
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
                    IpvScreeningOutcome = table.Column<string>(nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(nullable: true),
                    PnsApproach = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempContactListingExtracts", x => x.Id);
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
                    PHQ_9_Rating = table.Column<string>(nullable: true),
                    DepressionAssesmentScore = table.Column<int>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
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
                    DrinkAlcohol = table.Column<string>(nullable: true),
                    Smoking = table.Column<string>(nullable: true),
                    DrugUse = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
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
                    DateoffirstSession = table.Column<DateTime>(nullable: true),
                    PillCountAdherence = table.Column<int>(nullable: true),
                    MMAS4_1 = table.Column<string>(nullable: true),
                    MMAS4_2 = table.Column<string>(nullable: true),
                    MMAS4_3 = table.Column<string>(nullable: true),
                    MMAS4_4 = table.Column<string>(nullable: true),
                    MMSA8_1 = table.Column<string>(nullable: true),
                    MMSA4_2 = table.Column<string>(nullable: true),
                    MMSA4_3 = table.Column<string>(nullable: true),
                    MMSA4_4 = table.Column<string>(nullable: true),
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
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
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
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempGbvScreeningExtracts", x => x.Id);
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
                    Lethergy = table.Column<string>(nullable: true),
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
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempIptExtracts", x => x.Id);
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
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
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
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOvcExtracts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergiesChronicIllnessExtracts_SiteCode_PatientPK",
                table: "AllergiesChronicIllnessExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_ContactListingExtracts_SiteCode_PatientPK",
                table: "ContactListingExtracts",
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
                name: "IX_IptExtracts_SiteCode_PatientPK",
                table: "IptExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_OtzExtracts_SiteCode_PatientPK",
                table: "OtzExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_OvcExtracts_SiteCode_PatientPK",
                table: "OvcExtracts",
                columns: new[] { "SiteCode", "PatientPK" });


            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 0;");

                migrationBuilder.Sql(@"alter table AllergiesChronicIllnessExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table ContactListingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table DepressionScreeningExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table DrugAlcoholScreeningExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table EnhancedAdherenceCounsellingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table GbvScreeningExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table IptExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table OtzExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table OvcExtracts convert to character set utf8 collate utf8_unicode_ci;");

                migrationBuilder.Sql(@"alter table TempAllergiesChronicIllnessExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempContactListingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempDepressionScreeningExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempDrugAlcoholScreeningExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempEnhancedAdherenceCounsellingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempGbvScreeningExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempIptExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempOtzExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempOvcExtracts convert to character set utf8 collate utf8_unicode_ci;");

                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 1;");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergiesChronicIllnessExtracts");

            migrationBuilder.DropTable(
                name: "ContactListingExtracts");

            migrationBuilder.DropTable(
                name: "DepressionScreeningExtracts");

            migrationBuilder.DropTable(
                name: "DrugAlcoholScreeningExtracts");

            migrationBuilder.DropTable(
                name: "EnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropTable(
                name: "GbvScreeningExtracts");

            migrationBuilder.DropTable(
                name: "IptExtracts");

            migrationBuilder.DropTable(
                name: "OtzExtracts");

            migrationBuilder.DropTable(
                name: "OvcExtracts");

            migrationBuilder.DropTable(
                name: "TempAllergiesChronicIllnessExtracts");

            migrationBuilder.DropTable(
                name: "TempContactListingExtracts");

            migrationBuilder.DropTable(
                name: "TempDepressionScreeningExtracts");

            migrationBuilder.DropTable(
                name: "TempDrugAlcoholScreeningExtracts");

            migrationBuilder.DropTable(
                name: "TempEnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropTable(
                name: "TempGbvScreeningExtracts");

            migrationBuilder.DropTable(
                name: "TempIptExtracts");

            migrationBuilder.DropTable(
                name: "TempOtzExtracts");

            migrationBuilder.DropTable(
                name: "TempOvcExtracts");
        }
    }
}
