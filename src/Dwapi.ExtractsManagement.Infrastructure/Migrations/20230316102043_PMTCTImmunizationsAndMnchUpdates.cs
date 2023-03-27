using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class PMTCTImmunizationsAndMnchUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InfactCameForHAART",
                table: "TempPncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherCameForHIVTest",
                table: "TempPncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherGivenHAART",
                table: "TempPncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitTimingBaby",
                table: "TempPncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitTimingMother",
                table: "TempPncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacilityReceivingARTCare",
                table: "TempMnchArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EDD",
                table: "TempMatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LMP",
                table: "TempMatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaternalDeathAudited",
                table: "TempMatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnARTMat",
                table: "TempMatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferralReason",
                table: "TempMatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightLength",
                table: "TempCwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Refferred",
                table: "TempCwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevisitThisYear",
                table: "TempCwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InfactCameForHAART",
                table: "PncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherCameForHIVTest",
                table: "PncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherGivenHAART",
                table: "PncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitTimingBaby",
                table: "PncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitTimingMother",
                table: "PncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacilityReceivingARTCare",
                table: "MnchArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EDD",
                table: "MatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LMP",
                table: "MatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaternalDeathAudited",
                table: "MatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnARTMat",
                table: "MatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferralReason",
                table: "MatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightLength",
                table: "CwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Refferred",
                table: "CwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevisitThisYear",
                table: "CwcVisitExtracts",
                nullable: true);

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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
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
                    FullyImmunizedChild = table.Column<string>(nullable: true)
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
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMnchImmunizationExtracts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MnchImmunizationExtracts_SiteCode_PatientPK",
                table: "MnchImmunizationExtracts",
                columns: new[] { "SiteCode", "PatientPK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MnchImmunizationExtracts");

            migrationBuilder.DropTable(
                name: "TempMnchImmunizationExtracts");

            migrationBuilder.DropColumn(
                name: "InfactCameForHAART",
                table: "TempPncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "MotherCameForHIVTest",
                table: "TempPncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "MotherGivenHAART",
                table: "TempPncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "VisitTimingBaby",
                table: "TempPncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "VisitTimingMother",
                table: "TempPncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "FacilityReceivingARTCare",
                table: "TempMnchArtExtracts");

            migrationBuilder.DropColumn(
                name: "EDD",
                table: "TempMatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "LMP",
                table: "TempMatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "MaternalDeathAudited",
                table: "TempMatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "OnARTMat",
                table: "TempMatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ReferralReason",
                table: "TempMatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "HeightLength",
                table: "TempCwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Refferred",
                table: "TempCwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "RevisitThisYear",
                table: "TempCwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "InfactCameForHAART",
                table: "PncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "MotherCameForHIVTest",
                table: "PncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "MotherGivenHAART",
                table: "PncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "VisitTimingBaby",
                table: "PncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "VisitTimingMother",
                table: "PncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "FacilityReceivingARTCare",
                table: "MnchArtExtracts");

            migrationBuilder.DropColumn(
                name: "EDD",
                table: "MatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "LMP",
                table: "MatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "MaternalDeathAudited",
                table: "MatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "OnARTMat",
                table: "MatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ReferralReason",
                table: "MatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "HeightLength",
                table: "CwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Refferred",
                table: "CwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "RevisitThisYear",
                table: "CwcVisitExtracts");
        }
    }
}
