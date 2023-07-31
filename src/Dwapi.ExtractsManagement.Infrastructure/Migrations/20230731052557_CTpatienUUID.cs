using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CTpatienUUID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempPatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempPatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempPatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempPatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempPatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempPatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempOvcExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempOtzExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempIptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempGbvScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempEnhancedAdherenceCounsellingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempDrugAlcoholScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempDepressionScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempDefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempCovidExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempContactListingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempCervicalCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "TempAllergiesChronicIllnessExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "PatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "PatientBaselinesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "PatientArtExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "PatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "OvcExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "OtzExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "IptExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "GbvScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "EnhancedAdherenceCounsellingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "DrugAlcoholScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "DepressionScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "DefaulterTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "CovidExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "ContactListingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "CervicalCancerScreeningExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientUUID",
                table: "AllergiesChronicIllnessExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempPatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempPatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempPatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempPatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempOvcExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempOtzExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempIptExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempGbvScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempEnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempDrugAlcoholScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempDepressionScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempDefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempCovidExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempContactListingExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempCervicalCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "TempAllergiesChronicIllnessExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "PatientBaselinesExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "PatientArtExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "PatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "OvcExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "OtzExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "IptExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "GbvScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "EnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "DrugAlcoholScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "DepressionScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "DefaulterTracingExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "CovidExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "ContactListingExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "CervicalCancerScreeningExtracts");

            migrationBuilder.DropColumn(
                name: "PatientUUID",
                table: "AllergiesChronicIllnessExtracts");
        }
    }
}
