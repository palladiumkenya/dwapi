using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts
{ 
    public class TempHtsPartnerTracingExtractErrorSummaryRepository : TempHTSExtractErrorSummaryRepository<TempHtsPartnerTracingErrorSummary>, ITempHtsPartnerTracingErrorSummaryRepository
    {
        public TempHtsPartnerTracingExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
