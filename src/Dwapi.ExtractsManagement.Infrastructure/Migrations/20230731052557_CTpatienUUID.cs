using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CTpatienUUID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempPatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempOvcExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempOtzExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempIptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempGbvScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempEnhancedAdherenceCounsellingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempDrugAlcoholScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempDepressionScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempDefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempCovidExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempContactListingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempCervicalCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "TempAllergiesChronicIllnessExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "PatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "OvcExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "OtzExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "IptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "GbvScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "EnhancedAdherenceCounsellingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "DrugAlcoholScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "DepressionScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "DefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "CovidExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "ContactListingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "CervicalCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordUUID",
                table: "AllergiesChronicIllnessExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempPatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempOvcExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempOtzExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempIptExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempGbvScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempEnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempDrugAlcoholScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempDepressionScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempDefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempCovidExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempContactListingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempCervicalCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "TempAllergiesChronicIllnessExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "PatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "OvcExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "OtzExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "IptExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "GbvScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "EnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "DrugAlcoholScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "DepressionScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "DefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "CovidExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "ContactListingExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "CervicalCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "RecordUUID",
                table: "AllergiesChronicIllnessExtracts");
        }
    }
}
