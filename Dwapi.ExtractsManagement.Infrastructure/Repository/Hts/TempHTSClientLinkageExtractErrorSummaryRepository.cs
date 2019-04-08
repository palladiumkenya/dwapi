using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts
{
    public class TempHTSClientLinkageExtractErrorSummaryRepository : TempHTSExtractErrorSummaryRepository<TempHTSClientLinkageExtractErrorSummary>, ITempHTSClientLinkageExtractErrorSummaryRepository
    {
        public TempHTSClientLinkageExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}