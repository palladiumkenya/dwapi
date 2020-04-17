using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts
{ 
    public class TempHtsClientsLinkageExtractErrorSummaryRepository : TempHTSExtractErrorSummaryRepository<TempHtsClientLinkageErrorSummary>, ITempHtsClientLinkageErrorSummaryRepository
    {
        public TempHtsClientsLinkageExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
