using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtractHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtractId = table.Column<Guid>(nullable: false),
                    Stats = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    StatusInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContactRelation = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    DateConfirmedHIVPositive = table.Column<DateTime>(nullable: true),
                    DatePreviousARTStart = table.Column<DateTime>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    EducationLevel = table.Column<string>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: false),
                    PatientSource = table.Column<string>(nullable: true),
                    PreviousARTExposure = table.Column<string>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    RegistrationATPMTCT = table.Column<DateTime>(nullable: true),
                    RegistrationAtCCC = table.Column<DateTime>(nullable: true),
                    RegistrationAtTBClinic = table.Column<DateTime>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
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
                    table.PrimaryKey("PK_PatientExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PsmartStage",
                columns: table => new
                {
                    EId = table.Column<Guid>(nullable: false),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    DateSent = table.Column<DateTime>(nullable: true),
                    DateStaged = table.Column<DateTime>(nullable: false),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: true),
                    RequestId = table.Column<string>(nullable: true),
                    Shr = table.Column<string>(nullable: true),
                    Status = table.Column<string>(maxLength: 100, nullable: true),
                    Status_Date = table.Column<DateTime>(nullable: true),
                    Uuid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsmartStage", x => x.EId);
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
                    District = table.Column<string>(nullable: true),
                    EMR = table.Column<string>(nullable: true),
                    EducationLevel = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientSource = table.Column<string>(nullable: true),
                    PreviousARTExposure = table.Column<string>(nullable: true),
                    PreviousARTStartDate = table.Column<DateTime>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    RegistrationAtCCC = table.Column<DateTime>(nullable: true),
                    RegistrationAtPMTCT = table.Column<DateTime>(nullable: true),
                    RegistrationAtTBClinic = table.Column<DateTime>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
                    SatelliteName = table.Column<string>(nullable: true),
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
                name: "Validator",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Extract = table.Column<string>(nullable: true),
                    Field = table.Column<string>(nullable: true),
                    Logic = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientArtExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AgeARTStart = table.Column<decimal>(nullable: true),
                    AgeEnrollment = table.Column<decimal>(nullable: true),
                    AgeLastVisit = table.Column<decimal>(nullable: true),
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
                    PatientExtractId = table.Column<Guid>(nullable: true),
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
                        name: "FK_PatientArtExtract_PatientExtracts_PatientExtractId",
                        column: x => x.PatientExtractId,
                        principalTable: "PatientExtracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientBaselinesExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    PatientExtractId = table.Column<Guid>(nullable: true),
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
                        name: "FK_PatientBaselinesExtract_PatientExtracts_PatientExtractId",
                        column: x => x.PatientExtractId,
                        principalTable: "PatientExtracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientLaboratoryExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    EnrollmentTest = table.Column<int>(nullable: true),
                    OrderedByDate = table.Column<DateTime>(nullable: true),
                    PatientExtractId = table.Column<Guid>(nullable: true),
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
                        name: "FK_PatientLaboratoryExtract_PatientExtracts_PatientExtractId",
                        column: x => x.PatientExtractId,
                        principalTable: "PatientExtracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientPharmacyExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DispenseDate = table.Column<DateTime>(nullable: true),
                    Drug = table.Column<string>(nullable: true),
                    Duration = table.Column<decimal>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    ExpectedReturn = table.Column<DateTime>(nullable: true),
                    PatientExtractId = table.Column<Guid>(nullable: true),
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
                        name: "FK_PatientPharmacyExtract_PatientExtracts_PatientExtractId",
                        column: x => x.PatientExtractId,
                        principalTable: "PatientExtracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientStatusExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    ExitDescription = table.Column<string>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true),
                    PatientExtractId = table.Column<Guid>(nullable: true),
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
                        name: "FK_PatientStatusExtract_PatientExtracts_PatientExtractId",
                        column: x => x.PatientExtractId,
                        principalTable: "PatientExtracts",
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
                    EDD = table.Column<DateTime>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    FamilyPlanningMethod = table.Column<string>(nullable: true),
                    GestationAge = table.Column<decimal>(nullable: true),
                    Height = table.Column<decimal>(nullable: true),
                    LMP = table.Column<DateTime>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    OI = table.Column<string>(nullable: true),
                    OIDate = table.Column<DateTime>(nullable: true),
                    PatientExtractId = table.Column<Guid>(nullable: true),
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
                        name: "FK_PatientVisitExtract_PatientExtracts_PatientExtractId",
                        column: x => x.PatientExtractId,
                        principalTable: "PatientExtracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ValidationError",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateGenerated = table.Column<DateTime>(nullable: false),
                    RecordId = table.Column<Guid>(nullable: false),
                    ValidatorId = table.Column<Guid>(nullable: false)
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
                name: "IX_PatientArtExtract_PatientExtractId",
                table: "PatientArtExtract",
                column: "PatientExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientBaselinesExtract_PatientExtractId",
                table: "PatientBaselinesExtract",
                column: "PatientExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientLaboratoryExtract_PatientExtractId",
                table: "PatientLaboratoryExtract",
                column: "PatientExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPharmacyExtract_PatientExtractId",
                table: "PatientPharmacyExtract",
                column: "PatientExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientStatusExtract_PatientExtractId",
                table: "PatientStatusExtract",
                column: "PatientExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVisitExtract_PatientExtractId",
                table: "PatientVisitExtract",
                column: "PatientExtractId");

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
                name: "PsmartStage");

            migrationBuilder.DropTable(
                name: "TempPatientExtracts");

            migrationBuilder.DropTable(
                name: "ValidationError");

            migrationBuilder.DropTable(
                name: "PatientExtracts");

            migrationBuilder.DropTable(
                name: "Validator");
        }
    }
}
