using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ExtractRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientExtracts",
                table: "PatientExtracts");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PatientExtracts_Id",
                table: "PatientExtracts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientExtracts",
                table: "PatientExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientVisitExtracts_SiteCode_PatientPK",
                table: "PatientVisitExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientStatusExtracts_SiteCode_PatientPK",
                table: "PatientStatusExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientPharmacyExtracts_SiteCode_PatientPK",
                table: "PatientPharmacyExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientLaboratoryExtracts_SiteCode_PatientPK",
                table: "PatientLaboratoryExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientBaselinesExtracts_SiteCode_PatientPK",
                table: "PatientBaselinesExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientArtExtracts_SiteCode_PatientPK",
                table: "PatientArtExtracts",
                columns: new[] { "SiteCode", "PatientPK" });

            migrationBuilder.AddForeignKey(
                name: "FK_PatientArtExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientArtExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientBaselinesExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientBaselinesExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientLaboratoryExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientLaboratoryExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientPharmacyExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientPharmacyExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientStatusExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientStatusExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientVisitExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientVisitExtracts",
                columns: new[] { "SiteCode", "PatientPK" },
                principalTable: "PatientExtracts",
                principalColumns: new[] { "SiteCode", "PatientPK" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientArtExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientArtExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientBaselinesExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientBaselinesExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientLaboratoryExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientPharmacyExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientStatusExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientStatusExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientVisitExtracts_PatientExtracts_SiteCode_PatientPK",
                table: "PatientVisitExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PatientVisitExtracts_SiteCode_PatientPK",
                table: "PatientVisitExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PatientStatusExtracts_SiteCode_PatientPK",
                table: "PatientStatusExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PatientPharmacyExtracts_SiteCode_PatientPK",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PatientLaboratoryExtracts_SiteCode_PatientPK",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PatientExtracts_Id",
                table: "PatientExtracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientExtracts",
                table: "PatientExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PatientBaselinesExtracts_SiteCode_PatientPK",
                table: "PatientBaselinesExtracts");

            migrationBuilder.DropIndex(
                name: "IX_PatientArtExtracts_SiteCode_PatientPK",
                table: "PatientArtExtracts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientExtracts",
                table: "PatientExtracts",
                column: "Id");
        }
    }
}
