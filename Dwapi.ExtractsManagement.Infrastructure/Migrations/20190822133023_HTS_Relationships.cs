using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HTS_Relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HtsClientsExtracts",
                table: "HtsClientsExtracts");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_HtsClientsExtracts_Id",
                table: "HtsClientsExtracts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HtsClientsExtracts",
                table: "HtsClientsExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsTestKitsExtracts_SiteCode_PatientPk",
                table: "HtsTestKitsExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsPartnerTracingExtracts_SiteCode_PatientPk",
                table: "HtsPartnerTracingExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsPartnerNotificationServicesExtracts_SiteCode_PatientPk",
                table: "HtsPartnerNotificationServicesExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsClientTracingExtracts_SiteCode_PatientPk",
                table: "HtsClientTracingExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsClientTestsExtracts_SiteCode_PatientPk",
                table: "HtsClientTestsExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsClientsLinkageExtracts_SiteCode_PatientPk",
                table: "HtsClientsLinkageExtracts",
                columns: new[] { "SiteCode", "PatientPk" });

            migrationBuilder.AddForeignKey(
                name: "FK_HtsClientsLinkageExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsClientsLinkageExtracts",
                columns: new[] { "SiteCode", "PatientPk" },
                principalTable: "HtsClientsExtracts",
                principalColumns: new[] { "SiteCode", "PatientPk" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HtsClientTestsExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsClientTestsExtracts",
                columns: new[] { "SiteCode", "PatientPk" },
                principalTable: "HtsClientsExtracts",
                principalColumns: new[] { "SiteCode", "PatientPk" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HtsClientTracingExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsClientTracingExtracts",
                columns: new[] { "SiteCode", "PatientPk" },
                principalTable: "HtsClientsExtracts",
                principalColumns: new[] { "SiteCode", "PatientPk" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HtsPartnerNotificationServicesExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsPartnerNotificationServicesExtracts",
                columns: new[] { "SiteCode", "PatientPk" },
                principalTable: "HtsClientsExtracts",
                principalColumns: new[] { "SiteCode", "PatientPk" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HtsPartnerTracingExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsPartnerTracingExtracts",
                columns: new[] { "SiteCode", "PatientPk" },
                principalTable: "HtsClientsExtracts",
                principalColumns: new[] { "SiteCode", "PatientPk" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HtsTestKitsExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsTestKitsExtracts",
                columns: new[] { "SiteCode", "PatientPk" },
                principalTable: "HtsClientsExtracts",
                principalColumns: new[] { "SiteCode", "PatientPk" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HtsClientsLinkageExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsClientsLinkageExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_HtsClientTestsExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_HtsClientTracingExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsClientTracingExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_HtsPartnerNotificationServicesExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_HtsPartnerTracingExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsPartnerTracingExtracts");

            migrationBuilder.DropForeignKey(
                name: "FK_HtsTestKitsExtracts_HtsClientsExtracts_SiteCode_PatientPk",
                table: "HtsTestKitsExtracts");

            migrationBuilder.DropIndex(
                name: "IX_HtsTestKitsExtracts_SiteCode_PatientPk",
                table: "HtsTestKitsExtracts");

            migrationBuilder.DropIndex(
                name: "IX_HtsPartnerTracingExtracts_SiteCode_PatientPk",
                table: "HtsPartnerTracingExtracts");

            migrationBuilder.DropIndex(
                name: "IX_HtsPartnerNotificationServicesExtracts_SiteCode_PatientPk",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropIndex(
                name: "IX_HtsClientTracingExtracts_SiteCode_PatientPk",
                table: "HtsClientTracingExtracts");

            migrationBuilder.DropIndex(
                name: "IX_HtsClientTestsExtracts_SiteCode_PatientPk",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropIndex(
                name: "IX_HtsClientsLinkageExtracts_SiteCode_PatientPk",
                table: "HtsClientsLinkageExtracts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_HtsClientsExtracts_Id",
                table: "HtsClientsExtracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HtsClientsExtracts",
                table: "HtsClientsExtracts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HtsClientsExtracts",
                table: "HtsClientsExtracts",
                columns: new[] { "SiteCode", "PatientPK" });
        }
    }
}
