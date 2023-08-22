using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class
        CollationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 0;");
                migrationBuilder.Sql(@"alter table PatientAdverseEventExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempPatientAdverseEventExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table EmrMetrics convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table HtsClientExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table HtsClientLinkageExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table HtsClientPartnerExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempHtsClientExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempHtsClientLinkageExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempHtsClientPartnerExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table Dockets convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table HtsClientsExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table EmrSystems convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table HtsClientsLinkageExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table CentralRegistries convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table HtsClientTestsExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table DatabaseProtocols convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table HtsClientTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table HtsPartnerNotificationServicesExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table RestProtocols convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table HtsPartnerTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table Extracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table HtsTestKitsExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempHtsClientsExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempHtsClientsLinkageExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table Resources convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempHtsClientTestsExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table AppMetrics convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempHtsClientTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table ExtractHistory convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempHtsPartnerNotificationServicesExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table MasterPatientIndices convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempHtsPartnerTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table PatientArtExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempHtsTestKitsExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table PatientBaselinesExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table PatientExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table PatientLaboratoryExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table PatientPharmacyExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table PatientStatusExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table PatientVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table PsmartStage convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempMasterPatientIndices convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempPatientArtExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempPatientBaselinesExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempPatientExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempPatientLaboratoryExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempPatientPharmacyExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempPatientStatusExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table TempPatientVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table Validator convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"alter table ValidationError convert to character set utf8 collate utf8_unicode_ci;");
 migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 1;");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
