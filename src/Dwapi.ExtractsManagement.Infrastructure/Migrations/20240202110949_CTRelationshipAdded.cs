using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CTRelationshipAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WHOStagingOI",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IndexPatientPk",
                table: "TempHtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WHOStagingOI",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IndexPatientPk",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RelationshipsExtracts",
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
                    RelationshipToPatient = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipsExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelationshipsExtracts_PatientExtracts_SiteCode_PatientPK",
                        columns: x => new { x.SiteCode, x.PatientPK },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "SiteCode", "PatientPK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempRelationshipsExtracts",
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
                    RelationshipToPatient = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempRelationshipsExtracts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipsExtracts_SiteCode_PatientPK",
                table: "RelationshipsExtracts",
                columns: new[] { "SiteCode", "PatientPK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelationshipsExtracts");

            migrationBuilder.DropTable(
                name: "TempRelationshipsExtracts");

            migrationBuilder.DropColumn(
                name: "WHOStagingOI",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "IndexPatientPk",
                table: "TempHtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "WHOStagingOI",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "IndexPatientPk",
                table: "HtsPartnerNotificationServicesExtracts");
        }
    }
}
