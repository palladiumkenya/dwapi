using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class New_Hts_Collations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"alter table TempHtsClientsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsClientTestsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsTestKitsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsClientTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsClientTestsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsPartnerTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsClientsLinkageExtracts convert to character set utf8 collate utf8_unicode_ci;");


                migrationBuilder.Sql(@"alter table HtsClientsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsClientTestsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsTestKitsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsClientTracingExtracts convert to character set utf8 collate utf8_unicode_ci;"); 
                migrationBuilder.Sql(@"alter table HtsPartnerTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsPartnerNotificationServicesExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsClientsLinkageExtracts convert to character set utf8 collate utf8_unicode_ci;");

            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
