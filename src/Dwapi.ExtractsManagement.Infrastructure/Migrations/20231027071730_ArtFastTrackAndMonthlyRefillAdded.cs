using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ArtFastTrackAndMonthlyRefillAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CervicalCancerScreeningExtracts");

            migrationBuilder.DropTable(
                name: "TempCervicalCancerScreeningExtracts");

            migrationBuilder.CreateTable(
                name: "ArtFastTrackExtracts",
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
                    ARTRefillModel = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    CTXDispensed = table.Column<string>(nullable: true),
                    DapsoneDispensed = table.Column<string>(nullable: true),
                    CondomsDistributed = table.Column<string>(nullable: true),
                    OralContraceptivesDispensed = table.Column<string>(nullable: true),
                    MissedDoses = table.Column<string>(nullable: true),
                    Fatigue = table.Column<string>(nullable: true),
                    Cough = table.Column<string>(nullable: true),
                    Fever = table.Column<string>(nullable: true),
                    Rash = table.Column<string>(nullable: true),
                    NauseaOrVomiting = table.Column<string>(nullable: true),
                    GenitalSoreOrDischarge = table.Column<string>(nullable: true),
                    Diarrhea = table.Column<string>(nullable: true),
                    OtherSymptoms = table.Column<string>(nullable: true),
                    PregnancyStatus = table.Column<string>(nullable: true),
                    FPStatus = table.Column<string>(nullable: true),
                    FPMethod = table.Column<string>(nullable: true),
                    ReasonNotOnFP = table.Column<string>(nullable: true),
                    ReferredToClinic = table.Column<string>(nullable: true),
                    ReturnVisitDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtFastTrackExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtFastTrackExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CancerScreeningExtracts",
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
                    VisitType = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    SmokesCigarette = table.Column<string>(nullable: true),
                    NumberYearsSmoked = table.Column<int>(nullable: true),
                    NumberCigarettesPerDay = table.Column<int>(nullable: true),
                    OtherFormTobacco = table.Column<string>(nullable: true),
                    TakesAlcohol = table.Column<string>(nullable: true),
                    HIVStatus = table.Column<string>(nullable: true),
                    FamilyHistoryOfCa = table.Column<string>(nullable: true),
                    PreviousCaTreatment = table.Column<string>(nullable: true),
                    SymptomsCa = table.Column<string>(nullable: true),
                    CancerType = table.Column<string>(nullable: true),
                    FecalOccultBloodTest = table.Column<string>(nullable: true),
                    TreatmentOccultBlood = table.Column<string>(nullable: true),
                    Colonoscopy = table.Column<string>(nullable: true),
                    TreatmentColonoscopy = table.Column<string>(nullable: true),
                    EUA = table.Column<string>(nullable: true),
                    TreatmentRetinoblastoma = table.Column<string>(nullable: true),
                    RetinoblastomaGene = table.Column<string>(nullable: true),
                    TreatmentEUA = table.Column<string>(nullable: true),
                    DRE = table.Column<string>(nullable: true),
                    TreatmentDRE = table.Column<string>(nullable: true),
                    PSA = table.Column<string>(nullable: true),
                    TreatmentPSA = table.Column<string>(nullable: true),
                    VisualExamination = table.Column<string>(nullable: true),
                    TreatmentVE = table.Column<string>(nullable: true),
                    Cytology = table.Column<string>(nullable: true),
                    TreatmentCytology = table.Column<string>(nullable: true),
                    Imaging = table.Column<string>(nullable: true),
                    TreatmentImaging = table.Column<string>(nullable: true),
                    Biopsy = table.Column<string>(nullable: true),
                    TreatmentBiopsy = table.Column<string>(nullable: true),
                    PostTreatmentComplicationCause = table.Column<string>(nullable: true),
                    OtherPostTreatmentComplication = table.Column<string>(nullable: true),
                    ReferralReason = table.Column<string>(nullable: true),
                    ScreeningMethod = table.Column<string>(nullable: true),
                    TreatmentToday = table.Column<string>(nullable: true),
                    ReferredOut = table.Column<string>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    ScreeningType = table.Column<string>(nullable: true),
                    HPVScreeningResult = table.Column<string>(nullable: true),
                    TreatmentHPV = table.Column<string>(nullable: true),
                    VIAScreeningResult = table.Column<string>(nullable: true),
                    VIAVILIScreeningResult = table.Column<string>(nullable: true),
                    VIATreatmentOptions = table.Column<string>(nullable: true),
                    PAPSmearScreeningResult = table.Column<string>(nullable: true),
                    TreatmentPapSmear = table.Column<string>(nullable: true),
                    ReferalOrdered = table.Column<string>(nullable: true),
                    Colposcopy = table.Column<string>(nullable: true),
                    TreatmentColposcopy = table.Column<string>(nullable: true),
                    BiopsyCINIIandAbove = table.Column<string>(nullable: true),
                    BiopsyCINIIandBelow = table.Column<string>(nullable: true),
                    BiopsyNotAvailable = table.Column<string>(nullable: true),
                    CBE = table.Column<string>(nullable: true),
                    TreatmentCBE = table.Column<string>(nullable: true),
                    Ultrasound = table.Column<string>(nullable: true),
                    TreatmentUltraSound = table.Column<string>(nullable: true),
                    IfTissueDiagnosis = table.Column<string>(nullable: true),
                    DateTissueDiagnosis = table.Column<DateTime>(nullable: true),
                    ReasonNotDone = table.Column<string>(nullable: true),
                    FollowUpDate = table.Column<DateTime>(nullable: true),
                    Referred = table.Column<string>(nullable: true),
                    ReasonForReferral = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancerScreeningExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancerScreeningExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepMonthlyRefillExtracts",
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
                    VisitDate = table.Column<string>(nullable: true),
                    BehaviorRiskAssessment = table.Column<string>(nullable: true),
                    SexPartnerHIVStatus = table.Column<string>(nullable: true),
                    SymptomsAcuteHIV = table.Column<string>(nullable: true),
                    AdherenceCounsellingDone = table.Column<string>(nullable: true),
                    ContraIndicationForPrEP = table.Column<string>(nullable: true),
                    PrescribedPrepToday = table.Column<string>(nullable: true),
                    RegimenPrescribed = table.Column<string>(nullable: true),
                    NumberOfMonths = table.Column<string>(nullable: true),
                    CondomsIssued = table.Column<string>(nullable: true),
                    NumberOfCondomsIssued = table.Column<int>(nullable: false),
                    ClientGivenNextAppointment = table.Column<string>(nullable: true),
                    AppointmentDate = table.Column<DateTime>(nullable: true),
                    ReasonForFailureToGiveAppointment = table.Column<string>(nullable: true),
                    DateOfLastPrepDose = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepMonthlyRefillExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepMonthlyRefillExtracts_PatientPrepExtracts_SiteCode_Patie~",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientPrepExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempArtFastTrackExtracts",
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
                    ARTRefillModel = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    CTXDispensed = table.Column<string>(nullable: true),
                    DapsoneDispensed = table.Column<string>(nullable: true),
                    CondomsDistributed = table.Column<string>(nullable: true),
                    OralContraceptivesDispensed = table.Column<string>(nullable: true),
                    MissedDoses = table.Column<string>(nullable: true),
                    Fatigue = table.Column<string>(nullable: true),
                    Cough = table.Column<string>(nullable: true),
                    Fever = table.Column<string>(nullable: true),
                    Rash = table.Column<string>(nullable: true),
                    NauseaOrVomiting = table.Column<string>(nullable: true),
                    GenitalSoreOrDischarge = table.Column<string>(nullable: true),
                    Diarrhea = table.Column<string>(nullable: true),
                    OtherSymptoms = table.Column<string>(nullable: true),
                    PregnancyStatus = table.Column<string>(nullable: true),
                    FPStatus = table.Column<string>(nullable: true),
                    FPMethod = table.Column<string>(nullable: true),
                    ReasonNotOnFP = table.Column<string>(nullable: true),
                    ReferredToClinic = table.Column<string>(nullable: true),
                    ReturnVisitDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempArtFastTrackExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempCancerScreeningExtracts",
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
                    VisitType = table.Column<string>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    SmokesCigarette = table.Column<string>(nullable: true),
                    NumberYearsSmoked = table.Column<int>(nullable: true),
                    NumberCigarettesPerDay = table.Column<int>(nullable: true),
                    OtherFormTobacco = table.Column<string>(nullable: true),
                    TakesAlcohol = table.Column<string>(nullable: true),
                    HIVStatus = table.Column<string>(nullable: true),
                    FamilyHistoryOfCa = table.Column<string>(nullable: true),
                    PreviousCaTreatment = table.Column<string>(nullable: true),
                    SymptomsCa = table.Column<string>(nullable: true),
                    CancerType = table.Column<string>(nullable: true),
                    FecalOccultBloodTest = table.Column<string>(nullable: true),
                    TreatmentOccultBlood = table.Column<string>(nullable: true),
                    Colonoscopy = table.Column<string>(nullable: true),
                    TreatmentColonoscopy = table.Column<string>(nullable: true),
                    EUA = table.Column<string>(nullable: true),
                    TreatmentRetinoblastoma = table.Column<string>(nullable: true),
                    RetinoblastomaGene = table.Column<string>(nullable: true),
                    TreatmentEUA = table.Column<string>(nullable: true),
                    DRE = table.Column<string>(nullable: true),
                    TreatmentDRE = table.Column<string>(nullable: true),
                    PSA = table.Column<string>(nullable: true),
                    TreatmentPSA = table.Column<string>(nullable: true),
                    VisualExamination = table.Column<string>(nullable: true),
                    TreatmentVE = table.Column<string>(nullable: true),
                    Cytology = table.Column<string>(nullable: true),
                    TreatmentCytology = table.Column<string>(nullable: true),
                    Imaging = table.Column<string>(nullable: true),
                    TreatmentImaging = table.Column<string>(nullable: true),
                    Biopsy = table.Column<string>(nullable: true),
                    TreatmentBiopsy = table.Column<string>(nullable: true),
                    PostTreatmentComplicationCause = table.Column<string>(nullable: true),
                    OtherPostTreatmentComplication = table.Column<string>(nullable: true),
                    ReferralReason = table.Column<string>(nullable: true),
                    ScreeningMethod = table.Column<string>(nullable: true),
                    TreatmentToday = table.Column<string>(nullable: true),
                    ReferredOut = table.Column<string>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    ScreeningType = table.Column<string>(nullable: true),
                    HPVScreeningResult = table.Column<string>(nullable: true),
                    TreatmentHPV = table.Column<string>(nullable: true),
                    VIAScreeningResult = table.Column<string>(nullable: true),
                    VIAVILIScreeningResult = table.Column<string>(nullable: true),
                    VIATreatmentOptions = table.Column<string>(nullable: true),
                    PAPSmearScreeningResult = table.Column<string>(nullable: true),
                    TreatmentPapSmear = table.Column<string>(nullable: true),
                    ReferalOrdered = table.Column<string>(nullable: true),
                    Colposcopy = table.Column<string>(nullable: true),
                    TreatmentColposcopy = table.Column<string>(nullable: true),
                    BiopsyCINIIandAbove = table.Column<string>(nullable: true),
                    BiopsyCINIIandBelow = table.Column<string>(nullable: true),
                    BiopsyNotAvailable = table.Column<string>(nullable: true),
                    CBE = table.Column<string>(nullable: true),
                    TreatmentCBE = table.Column<string>(nullable: true),
                    Ultrasound = table.Column<string>(nullable: true),
                    TreatmentUltraSound = table.Column<string>(nullable: true),
                    IfTissueDiagnosis = table.Column<string>(nullable: true),
                    DateTissueDiagnosis = table.Column<DateTime>(nullable: true),
                    ReasonNotDone = table.Column<string>(nullable: true),
                    FollowUpDate = table.Column<DateTime>(nullable: true),
                    Referred = table.Column<string>(nullable: true),
                    ReasonForReferral = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCancerScreeningExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempPrepMonthlyRefillExtracts",
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
                    VisitDate = table.Column<string>(nullable: true),
                    BehaviorRiskAssessment = table.Column<string>(nullable: true),
                    SexPartnerHIVStatus = table.Column<string>(nullable: true),
                    SymptomsAcuteHIV = table.Column<string>(nullable: true),
                    AdherenceCounsellingDone = table.Column<string>(nullable: true),
                    ContraIndicationForPrEP = table.Column<string>(nullable: true),
                    PrescribedPrepToday = table.Column<string>(nullable: true),
                    RegimenPrescribed = table.Column<string>(nullable: true),
                    NumberOfMonths = table.Column<string>(nullable: true),
                    CondomsIssued = table.Column<string>(nullable: true),
                    NumberOfCondomsIssued = table.Column<int>(nullable: false),
                    ClientGivenNextAppointment = table.Column<string>(nullable: true),
                    AppointmentDate = table.Column<DateTime>(nullable: true),
                    ReasonForFailureToGiveAppointment = table.Column<string>(nullable: true),
                    DateOfLastPrepDose = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPrepMonthlyRefillExtracts", x => x.Id);
                });
            

            migrationBuilder.CreateIndex(
                name: "IX_ArtFastTrackExtracts_SiteCode_PatientPK",
                table: "ArtFastTrackExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_CancerScreeningExtracts_SiteCode_PatientPK",
                table: "CancerScreeningExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepMonthlyRefillExtracts_SiteCode_PatientPK",
                table: "PrepMonthlyRefillExtracts",
                columns: new[] { "SiteCode", "PatientPK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtFastTrackExtracts");

            migrationBuilder.DropTable(
                name: "CancerScreeningExtracts");

            migrationBuilder.DropTable(
                name: "PrepMonthlyRefillExtracts");

            migrationBuilder.DropTable(
                name: "TempArtFastTrackExtracts");

            migrationBuilder.DropTable(
                name: "TempCancerScreeningExtracts");

            migrationBuilder.DropTable(
                name: "TempPrepMonthlyRefillExtracts");
            
            migrationBuilder.CreateTable(
                name: "CervicalCancerScreeningExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    OtherPostTreatmentComplication = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: false),
                    PostTreatmentComplicationCause = table.Column<string>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    ReferralReason = table.Column<string>(nullable: true),
                    ReferredOut = table.Column<string>(nullable: true),
                    ScreeningMethod = table.Column<string>(nullable: true),
                    ScreeningResult = table.Column<string>(nullable: true),
                    ScreeningType = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    TreatmentToday = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
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
                name: "TempCervicalCancerScreeningExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    ErrorType = table.Column<int>(nullable: false),
                    FacilityId = table.Column<int>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    OtherPostTreatmentComplication = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: true),
                    PostTreatmentComplicationCause = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    ReferralReason = table.Column<string>(nullable: true),
                    ReferredOut = table.Column<string>(nullable: true),
                    ScreeningMethod = table.Column<string>(nullable: true),
                    ScreeningResult = table.Column<string>(nullable: true),
                    ScreeningType = table.Column<string>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    TreatmentToday = table.Column<string>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: true),
                    VisitID = table.Column<int>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCervicalCancerScreeningExtracts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CervicalCancerScreeningExtracts_SiteCode_PatientPK",
                table: "CervicalCancerScreeningExtracts",
                columns: new[] { "SiteCode", "PatientPK" });
        }
    }
}
