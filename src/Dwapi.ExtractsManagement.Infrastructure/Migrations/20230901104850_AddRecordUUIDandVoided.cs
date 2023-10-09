using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddRecordUUIDandVoided : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPrepVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPrepVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPrepPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPrepPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPrepLabExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPrepLabExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPrepCareTerminationExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPrepCareTerminationExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPrepBehaviourRiskExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPrepBehaviourRiskExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPrepAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPrepAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPatientPrepExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPatientPrepExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPatientMnchExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPatientMnchExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempPatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempOvcExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempOtzExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempMotherBabyPairExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempMotherBabyPairExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempMnchLabExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempMnchLabExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempMnchImmunizationExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempMnchImmunizationExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempMnchEnrolmentExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempMnchEnrolmentExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempMnchArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempMnchArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempMatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempMatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempIptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempIITRiskScoresExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempHtsTestKitsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempHtsTestKitsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempHtsPartnerTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempHtsPartnerTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempHtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempHtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempHtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempHtsClientTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempHtsClientTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempHtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempHtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempHtsClientsLinkageExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempHtsClientsLinkageExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempHtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempHtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempHeiExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempHeiExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempGbvScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempEnhancedAdherenceCounsellingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempDrugAlcoholScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempDepressionScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempDefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempCwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempCwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempCwcEnrolmentExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempCwcEnrolmentExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempCovidExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempContactListingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempCervicalCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempAncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempAncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "TempAllergiesChronicIllnessExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PrepVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PrepVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PrepPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PrepPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PrepLabExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PrepLabExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PrepCareTerminationExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PrepCareTerminationExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PrepBehaviourRiskExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PrepBehaviourRiskExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PrepAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PrepAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PatientPrepExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PatientPrepExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PatientMnchExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PatientMnchExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "PatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "OvcExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "OtzExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "MotherBabyPairExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "MotherBabyPairExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "MnchLabExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "MnchLabExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "MnchImmunizationExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "MnchImmunizationExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "MnchEnrolmentExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "MnchEnrolmentExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "MnchArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "MnchArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "MatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "MatVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "IptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "IITRiskScoresExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "HtsTestKitsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "HtsTestKitsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "HtsPartnerTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "HtsPartnerTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "HtsEligibilityExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "HtsClientTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "HtsClientTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "HtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "HtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "HtsClientsLinkageExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "HtsClientsLinkageExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "HtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "HtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "HeiExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "HeiExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "GbvScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "EnhancedAdherenceCounsellingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "DrugAlcoholScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "DepressionScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "DefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "CwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "CwcVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "CwcEnrolmentExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "CwcEnrolmentExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "CovidExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "ContactListingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "CervicalCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "AncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "AncVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Voided",
                table: "AllergiesChronicIllnessExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPrepVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPrepVisitExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPrepPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPrepPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPrepLabExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPrepLabExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPrepCareTerminationExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPrepCareTerminationExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPrepBehaviourRiskExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPrepBehaviourRiskExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPrepAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPrepAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPatientPrepExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPatientPrepExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPatientMnchExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPatientMnchExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempPatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempOvcExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempOtzExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempMotherBabyPairExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempMotherBabyPairExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempMnchLabExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempMnchLabExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempMnchImmunizationExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempMnchImmunizationExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempMnchEnrolmentExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempMnchEnrolmentExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempMnchArtExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempMnchArtExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempMatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempMatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempIptExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempIITRiskScoresExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempHtsTestKitsExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempHtsTestKitsExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempHtsPartnerTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempHtsPartnerTracingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempHtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempHtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempHtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempHtsClientTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempHtsClientTracingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempHtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempHtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempHtsClientsLinkageExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempHtsClientsLinkageExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempHtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempHtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempHeiExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempHeiExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempGbvScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempEnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempDrugAlcoholScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempDepressionScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempDefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempCwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempCwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempCwcEnrolmentExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempCwcEnrolmentExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempCovidExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempContactListingExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempCervicalCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempAncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempAncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "TempAllergiesChronicIllnessExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PrepVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PrepVisitExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PrepPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PrepPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PrepLabExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PrepLabExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PrepCareTerminationExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PrepCareTerminationExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PrepBehaviourRiskExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PrepBehaviourRiskExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PrepAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PrepAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PatientPrepExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PatientPrepExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PatientMnchExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PatientMnchExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "PatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "OvcExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "OtzExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "MotherBabyPairExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "MotherBabyPairExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "MnchLabExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "MnchLabExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "MnchImmunizationExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "MnchImmunizationExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "MnchEnrolmentExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "MnchEnrolmentExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "MnchArtExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "MnchArtExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "MatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "MatVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "IptExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "IITRiskScoresExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "HtsTestKitsExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "HtsTestKitsExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "HtsPartnerTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "HtsPartnerTracingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "HtsEligibilityExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "HtsClientTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "HtsClientTracingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "HtsClientsLinkageExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "HtsClientsLinkageExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "HtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "HtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "HeiExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "HeiExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "GbvScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "EnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "DrugAlcoholScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "DepressionScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "DefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "CwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "CwcVisitExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "CwcEnrolmentExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "CwcEnrolmentExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "CovidExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "ContactListingExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "CervicalCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "AncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "AncVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Voided",
                table: "AllergiesChronicIllnessExtracts");
        }
    }
}
