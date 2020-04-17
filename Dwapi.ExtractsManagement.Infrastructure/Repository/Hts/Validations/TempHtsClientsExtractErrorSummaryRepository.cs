using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts
{ 
    public class TempHtsClientsExtractErrorSummaryRepository : TempHTSExtractErrorSummaryRepository<TempHtsClientsErrorSummary>, ITempHtsClientsExtractErrorSummaryRepository
    {
        public TempHtsClientsExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
