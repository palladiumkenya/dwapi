using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts
{
    public class TempHtsClientTestsExtractErrorSummaryRepository : TempHTSExtractErrorSummaryRepository<TempHtsClientTestsErrorSummary>, ITempHtsClientTestsErrorSummaryRepository
    {
        public TempHtsClientTestsExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
