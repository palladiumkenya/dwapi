using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class PrepFixPatientPKDups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrepAdverseEventExtracts_PatientPrepExtracts_SiteCode_Patient",
                table: "PrepAdverseEventExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepBehaviourRiskExtracts_PatientPrepExtracts_SiteCode_Patien",
                table: "PrepBehaviourRiskExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepCareTerminationExtracts_PatientPrepExtracts_SiteCode_Pati",
                table: "PrepCareTerminationExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepLabExtracts_PatientPrepExtracts_SiteCode_PatientPK",
                table: "PrepLabExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepMonthlyRefillExtracts_PatientPrepExtracts_SiteCode_Patien",
                table: "PrepMonthlyRefillExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepPharmacyExtracts_PatientPrepExtracts_SiteCode_PatientPK",
                table: "PrepPharmacyExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepVisitExtracts_PatientPrepExtracts_SiteCode_PatientPK",
                table: "PrepVisitExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepVisitExtracts_SiteCode_PatientPK",
                table: "PrepVisitExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepPharmacyExtracts_SiteCode_PatientPK",
                table: "PrepPharmacyExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepMonthlyRefillExtracts_SiteCode_PatientPK",
                table: "PrepMonthlyRefillExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepLabExtracts_SiteCode_PatientPK",
                table: "PrepLabExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepCareTerminationExtracts_SiteCode_PatientPK",
                table: "PrepCareTerminationExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepBehaviourRiskExtracts_SiteCode_PatientPK",
                table: "PrepBehaviourRiskExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepAdverseEventExtracts_SiteCode_PatientPK",
                table: "PrepAdverseEventExtracts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PatientPrepExtracts_Id",
                table: "PatientPrepExtracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientPrepExtracts",
                table: "PatientPrepExtracts");

            migrationBuilder.AddColumn<Guid>(
                name: "PatientPrepExtractId",
                table: "PrepVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PatientPrepExtractId",
                table: "PrepPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PatientPrepExtractId",
                table: "PrepMonthlyRefillExtracts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PatientPrepExtractId",
                table: "PrepLabExtracts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PatientPrepExtractId",
                table: "PrepCareTerminationExtracts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PatientPrepExtractId",
                table: "PrepBehaviourRiskExtracts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PatientPrepExtractId",
                table: "PrepAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientPrepExtracts",
                table: "PatientPrepExtracts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PrepVisitExtracts_PatientPrepExtractId",
                table: "PrepVisitExtracts",
                column: "PatientPrepExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PrepPharmacyExtracts_PatientPrepExtractId",
                table: "PrepPharmacyExtracts",
                column: "PatientPrepExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PrepMonthlyRefillExtracts_PatientPrepExtractId",
                table: "PrepMonthlyRefillExtracts",
                column: "PatientPrepExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PrepLabExtracts_PatientPrepExtractId",
                table: "PrepLabExtracts",
                column: "PatientPrepExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PrepCareTerminationExtracts_PatientPrepExtractId",
                table: "PrepCareTerminationExtracts",
                column: "PatientPrepExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PrepBehaviourRiskExtracts_PatientPrepExtractId",
                table: "PrepBehaviourRiskExtracts",
                column: "PatientPrepExtractId");

            migrationBuilder.CreateIndex(
                name: "IX_PrepAdverseEventExtracts_PatientPrepExtractId",
                table: "PrepAdverseEventExtracts",
                column: "PatientPrepExtractId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrepAdverseEventExtracts_PatientPrepExtracts_SiteCode_Patient",
                table: "PrepAdverseEventExtracts",
                column: "PatientPrepExtractId",
                principalTable: "PatientPrepExtracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepBehaviourRiskExtracts_PatientPrepExtracts_SiteCode_Patien",
                table: "PrepBehaviourRiskExtracts",
                column: "PatientPrepExtractId",
                principalTable: "PatientPrepExtracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepCareTerminationExtracts_PatientPrepExtracts_SiteCode_Pati",
                table: "PrepCareTerminationExtracts",
                column: "PatientPrepExtractId",
                principalTable: "PatientPrepExtracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepLabExtracts_PatientPrepExtracts_PatientPrepExtractId",
                table: "PrepLabExtracts",
                column: "PatientPrepExtractId",
                principalTable: "PatientPrepExtracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepMonthlyRefillExtracts_PatientPrepExtracts_SiteCode_Patien",
                table: "PrepMonthlyRefillExtracts",
                column: "PatientPrepExtractId",
                principalTable: "PatientPrepExtracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepPharmacyExtracts_PatientPrepExtracts_PatientPrepExtractId",
                table: "PrepPharmacyExtracts",
                column: "PatientPrepExtractId",
                principalTable: "PatientPrepExtracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepVisitExtracts_PatientPrepExtracts_PatientPrepExtractId",
                table: "PrepVisitExtracts",
                column: "PatientPrepExtractId",
                principalTable: "PatientPrepExtracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrepAdverseEventExtracts_PatientPrepExtracts_SiteCode_Patient",
                table: "PrepAdverseEventExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepBehaviourRiskExtracts_PatientPrepExtracts_SiteCode_Patien",
                table: "PrepBehaviourRiskExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepCareTerminationExtracts_PatientPrepExtracts_SiteCode_Pati",
                table: "PrepCareTerminationExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepLabExtracts_PatientPrepExtracts_PatientPrepExtractId",
                table: "PrepLabExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepMonthlyRefillExtracts_PatientPrepExtracts_SiteCode_Patien",
                table: "PrepMonthlyRefillExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepPharmacyExtracts_PatientPrepExtracts_PatientPrepExtractId",
                table: "PrepPharmacyExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrepVisitExtracts_PatientPrepExtracts_PatientPrepExtractId",
                table: "PrepVisitExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepVisitExtracts_PatientPrepExtractId",
                table: "PrepVisitExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepPharmacyExtracts_PatientPrepExtractId",
                table: "PrepPharmacyExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepMonthlyRefillExtracts_PatientPrepExtractId",
                table: "PrepMonthlyRefillExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepLabExtracts_PatientPrepExtractId",
                table: "PrepLabExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepCareTerminationExtracts_PatientPrepExtractId",
                table: "PrepCareTerminationExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepBehaviourRiskExtracts_PatientPrepExtractId",
                table: "PrepBehaviourRiskExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PrepAdverseEventExtracts_PatientPrepExtractId",
                table: "PrepAdverseEventExtracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientPrepExtracts",
                table: "PatientPrepExtracts");

            migrationBuilder.DropColumn(
                name: "PatientPrepExtractId",
                table: "PrepVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PatientPrepExtractId",
                table: "PrepPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "PatientPrepExtractId",
                table: "PrepMonthlyRefillExtracts");

            migrationBuilder.DropColumn(
                name: "PatientPrepExtractId",
                table: "PrepLabExtracts");

            migrationBuilder.DropColumn(
                name: "PatientPrepExtractId",
                table: "PrepCareTerminationExtracts");

            migrationBuilder.DropColumn(
                name: "PatientPrepExtractId",
                table: "PrepBehaviourRiskExtracts");

            migrationBuilder.DropColumn(
                name: "PatientPrepExtractId",
                table: "PrepAdverseEventExtracts");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PatientPrepExtracts_Id",
                table: "PatientPrepExtracts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientPrepExtracts",
                table: "PatientPrepExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepVisitExtracts_SiteCode_PatientPK",
                table: "PrepVisitExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepPharmacyExtracts_SiteCode_PatientPK",
                table: "PrepPharmacyExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepMonthlyRefillExtracts_SiteCode_PatientPK",
                table: "PrepMonthlyRefillExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepLabExtracts_SiteCode_PatientPK",
                table: "PrepLabExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepCareTerminationExtracts_SiteCode_PatientPK",
                table: "PrepCareTerminationExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepBehaviourRiskExtracts_SiteCode_PatientPK",
                table: "PrepBehaviourRiskExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepAdverseEventExtracts_SiteCode_PatientPK",
                table: "PrepAdverseEventExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.AddForeignKey(
                name: "FK_PrepAdverseEventExtracts_PatientPrepExtracts_SiteCode_Patient",
                table: "PrepAdverseEventExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientPrepExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepBehaviourRiskExtracts_PatientPrepExtracts_SiteCode_Patien",
                table: "PrepBehaviourRiskExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientPrepExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepCareTerminationExtracts_PatientPrepExtracts_SiteCode_Pati",
                table: "PrepCareTerminationExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientPrepExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepLabExtracts_PatientPrepExtracts_SiteCode_PatientPK",
                table: "PrepLabExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientPrepExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepMonthlyRefillExtracts_PatientPrepExtracts_SiteCode_Patien",
                table: "PrepMonthlyRefillExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientPrepExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepPharmacyExtracts_PatientPrepExtracts_SiteCode_PatientPK",
                table: "PrepPharmacyExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientPrepExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrepVisitExtracts_PatientPrepExtracts_SiteCode_PatientPK",
                table: "PrepVisitExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientPrepExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
