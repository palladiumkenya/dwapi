using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class MgsViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                        create view vTempMetricMigrationExtractError as
                        SELECT  *
                        FROM    TempMetricMigrationExtracts
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempMetricMigrationExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempMetricMigrationExtractError.FacilityName,
                        vTempMetricMigrationExtractError.MetricId,
                        vTempMetricMigrationExtractError.SiteCode,
                        vTempMetricMigrationExtractError.Dataset,
                        vTempMetricMigrationExtractError.Metric,
                        vTempMetricMigrationExtractError.MetricValue
                        FROM vTempMetricMigrationExtractError 
                        INNER JOIN ValidationError ON vTempMetricMigrationExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP View vTempMetricMigrationExtractError");
            migrationBuilder.Sql("DROP View vTempMetricMigrationExtractErrorSummary");
        }
    }
}
