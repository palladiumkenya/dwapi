using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class mysqlcolations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"alter table TempHtsClientExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsClientLinkageExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsClientPartnerExtracts convert to character set utf8 collate utf8_unicode_ci;"); 

                migrationBuilder.Sql(@"alter table HtsClientExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsClientLinkageExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsClientPartnerExtracts convert to character set utf8 collate utf8_unicode_ci;");

                migrationBuilder.Sql(@"alter table TempPatientAdverseEventExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempPatientArtExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempPatientBaselinesExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempPatientExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempPatientLaboratoryExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempPatientPharmacyExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempPatientStatusExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempPatientVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");

                migrationBuilder.Sql(@"alter table PatientAdverseEventExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table PatientArtExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table PatientBaselinesExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table PatientExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table PatientLaboratoryExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table PatientPharmacyExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table PatientStatusExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table PatientVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");

                migrationBuilder.Sql(@"alter table MasterPatientIndices convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempMasterPatientIndices convert to character set utf8 collate utf8_unicode_ci;");

            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
