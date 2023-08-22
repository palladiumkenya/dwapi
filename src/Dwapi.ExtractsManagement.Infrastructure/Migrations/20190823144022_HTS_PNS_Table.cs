using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HTS_PNS_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientSelfTested",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "MonthsSinceLastTest",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "TestDate",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.RenameColumn(
                name: "TestType",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "TestStrategy",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "ScreenedForIpv");

            migrationBuilder.RenameColumn(
                name: "TestResult2",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "RelationsipToIndexClient");

            migrationBuilder.RenameColumn(
                name: "TestResult1",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "PnsConsent");

            migrationBuilder.RenameColumn(
                name: "TbScreening",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "PnsApproach");

            migrationBuilder.RenameColumn(
                name: "PatientGivenResult",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "MaritalStatus");

            migrationBuilder.RenameColumn(
                name: "FinalTestResult",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "LinkedToCare");

            migrationBuilder.RenameColumn(
                name: "EverTestedForHiv",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "KnowledgeOfHivStatus");

            migrationBuilder.RenameColumn(
                name: "EntryPoint",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "IpvScreeningOutcome");

            migrationBuilder.RenameColumn(
                name: "EncounterId",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "PartnerPersonID");

            migrationBuilder.RenameColumn(
                name: "CoupleDiscordant",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "FacilityLinkedTo");

            migrationBuilder.RenameColumn(
                name: "Consent",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "CurrentlyLivingWithIndexClient");

            migrationBuilder.RenameColumn(
                name: "ClientTestedAs",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "CccNumber");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateElicited",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Dob",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LinkDateLinkedToCare",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartnerPatientPk",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "DateElicited",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "Dob",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "LinkDateLinkedToCare",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "PartnerPatientPk",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "TestType");

            migrationBuilder.RenameColumn(
                name: "ScreenedForIpv",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "TestStrategy");

            migrationBuilder.RenameColumn(
                name: "RelationsipToIndexClient",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "TestResult2");

            migrationBuilder.RenameColumn(
                name: "PnsConsent",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "TestResult1");

            migrationBuilder.RenameColumn(
                name: "PnsApproach",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "TbScreening");

            migrationBuilder.RenameColumn(
                name: "PartnerPersonID",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "EncounterId");

            migrationBuilder.RenameColumn(
                name: "MaritalStatus",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "PatientGivenResult");

            migrationBuilder.RenameColumn(
                name: "LinkedToCare",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "FinalTestResult");

            migrationBuilder.RenameColumn(
                name: "KnowledgeOfHivStatus",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "EverTestedForHiv");

            migrationBuilder.RenameColumn(
                name: "IpvScreeningOutcome",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "EntryPoint");

            migrationBuilder.RenameColumn(
                name: "FacilityLinkedTo",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "CoupleDiscordant");

            migrationBuilder.RenameColumn(
                name: "CurrentlyLivingWithIndexClient",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "Consent");

            migrationBuilder.RenameColumn(
                name: "CccNumber",
                table: "HtsPartnerNotificationServicesExtracts",
                newName: "ClientTestedAs");

            migrationBuilder.AddColumn<string>(
                name: "ClientSelfTested",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthsSinceLastTest",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TestDate",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
