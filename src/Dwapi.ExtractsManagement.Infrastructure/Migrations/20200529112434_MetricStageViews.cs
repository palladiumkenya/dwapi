using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class MetricStageViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 0;");
                migrationBuilder.Sql(@"alter table MetricMigrationExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempMetricMigrationExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table EmrSystems convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 1;");
            }

            migrationBuilder.Sql(@"
                        alter view vTempMetricMigrationExtractError as
                        SELECT  *
                        FROM    TempMetricMigrationExtracts
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        alter VIEW vTempMetricMigrationExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempMetricMigrationExtractError.FacilityName,
                        vTempMetricMigrationExtractError.MetricId,
                        vTempMetricMigrationExtractError.SiteCode,
                        vTempMetricMigrationExtractError.Dataset,
                        vTempMetricMigrationExtractError.Metric,
                        vTempMetricMigrationExtractError.MetricValue,
                        vTempMetricMigrationExtractError.Stage,
                        vTempMetricMigrationExtractError.CreateDate
                        FROM vTempMetricMigrationExtractError 
                        INNER JOIN ValidationError ON vTempMetricMigrationExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
