using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Mgs
{
    public class TempMetricMigrationExtractErrorSummaryRepository :TempMetricExtractErrorSummaryRepository<TempMetricMigrationExtractErrorSummary>, ITempMetricMigrationExtractErrorSummaryRepository
    {
        public TempMetricMigrationExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
