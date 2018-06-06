using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class MPIInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "MasterPatientIndices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowId = table.Column<int>(nullable: false),
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
                name: "TempMasterPatientIndices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    CheckError = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMasterPatientIndices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterPatientIndices");

            migrationBuilder.DropTable(
                name: "TempMasterPatientIndices");

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
        }
    }
}
