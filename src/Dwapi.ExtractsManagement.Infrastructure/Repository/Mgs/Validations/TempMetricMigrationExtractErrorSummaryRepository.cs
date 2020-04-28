using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Mgs.Validations
{
    public class TempMetricMigrationExtractErrorSummaryRepository :TempMetricExtractErrorSummaryRepository<TempMetricMigrationExtractErrorSummary>, ITempMetricMigrationExtractErrorSummaryRepository
    {
        public TempMetricMigrationExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
