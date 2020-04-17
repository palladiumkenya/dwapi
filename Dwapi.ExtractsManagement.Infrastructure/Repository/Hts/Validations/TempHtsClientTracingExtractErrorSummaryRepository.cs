using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts
{ 
    public class TempHtsClientTracingExtractErrorSummaryRepository : TempHTSExtractErrorSummaryRepository<TempHtsClientTracingErrorSummary>, ITempHtsClientTracingErrorSummaryRepository
    {
        public TempHtsClientTracingExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
