using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class PrepInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDiscontinuationDate",
                table: "TempPatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDiscontinuationDate",
                table: "PatientStatusExtracts",
                nullable: true);

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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
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
                    DateLastUsedPrev = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPrepExtracts", x => new { x.SiteCode, x.PatientPK });
                    table.UniqueConstraint("AK_PatientPrepExtracts_Id", x => x.Id);
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPatientPrepExtracts", x => x.Id);
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPrepVisitExtracts", x => x.Id);
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
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
                    AdverseEventRegimen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepAdverseEventExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepAdverseEventExtracts_PatientPrepExtracts_SiteCode_PatientPK",
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
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
                    NumberofchildrenWithPartner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepBehaviourRiskExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepBehaviourRiskExtracts_PatientPrepExtracts_SiteCode_PatientPK",
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    DateOfLastPrepDose = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepCareTerminationExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepCareTerminationExtracts_PatientPrepExtracts_SiteCode_PatientPK",
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    SampleDate = table.Column<DateTime>(nullable: true),
                    TestResultDate = table.Column<DateTime>(nullable: true),
                    Reason = table.Column<string>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    PrepNumber = table.Column<string>(nullable: true),
                    HtsNumber = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    RegimenPrescribed = table.Column<string>(nullable: true),
                    DispenseDate = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
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
                    ClinicalNotes = table.Column<string>(nullable: true)
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

            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 0;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.TempPatientPrepExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.TempPrepAdverseEventExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.TempPrepBehaviourRiskExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.TempPrepCareTerminationExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.TempPrepLabExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.TempPrepPharmacyExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.TempPrepVisitExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.PatientPrepExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.PrepAdverseEventExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.PrepBehaviourRiskExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.PrepCareTerminationExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.PrepLabExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.PrepPharmacyExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.PrepVisitExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 1;");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "TempPatientPrepExtracts");

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
                name: "PatientPrepExtracts");

            migrationBuilder.DropColumn(
                name: "EffectiveDiscontinuationDate",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "EffectiveDiscontinuationDate",
                table: "PatientStatusExtracts");
        }
    }
}
