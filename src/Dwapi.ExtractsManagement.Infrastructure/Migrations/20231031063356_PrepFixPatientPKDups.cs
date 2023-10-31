using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class PrepFixPatientPKDups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"ALTER TABLE dwapi.PrepAdverseEventExtracts DROP CONSTRAINT FK_PrepAdverseEventExtracts_PatientPrepExtracts_SiteCode_Patient;");
                migrationBuilder.Sql(@"ALTER TABLE dwapi.PrepBehaviourRiskExtracts DROP CONSTRAINT FK_PrepBehaviourRiskExtracts_PatientPrepExtracts_SiteCode_Patien;");
                migrationBuilder.Sql(@"ALTER TABLE dwapi.PrepCareTerminationExtracts DROP CONSTRAINT FK_PrepCareTerminationExtracts_PatientPrepExtracts_SiteCode_Pati;");
                migrationBuilder.Sql(@"ALTER TABLE dwapi.PrepLabExtracts DROP CONSTRAINT FK_PrepLabExtracts_PatientPrepExtracts_SiteCode_PatientPK;");
                migrationBuilder.Sql(@"ALTER TABLE dwapi.PrepPharmacyExtracts DROP CONSTRAINT FK_PrepPharmacyExtracts_PatientPrepExtracts_SiteCode_PatientPK;");
                migrationBuilder.Sql(@"ALTER TABLE dwapi.PrepVisitExtracts DROP CONSTRAINT FK_PrepVisitExtracts_PatientPrepExtracts_SiteCode_PatientPK;");
                migrationBuilder.Sql(@"ALTER TABLE dwapi.PrepMonthlyRefillExtracts DROP CONSTRAINT FK_PrepMonthlyRefillExtracts_PatientPrepExtracts_SiteCode_Patien;");
               
                migrationBuilder.Sql(@"ALTER TABLE `dwapi`.`PatientPrepExtracts` 
                        DROP PRIMARY KEY,
                        ADD PRIMARY KEY (`SiteCode`, `Id`);
                        ");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
