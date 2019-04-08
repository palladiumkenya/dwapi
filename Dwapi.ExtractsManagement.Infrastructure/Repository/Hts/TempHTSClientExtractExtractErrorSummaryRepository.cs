using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts
{
    public class TempHTSClientExtractExtractErrorSummaryRepository : TempHTSExtractErrorSummaryRepository<TempHTSClientExtractErrorSummary>, ITempHTSClientExtractErrorSummaryRepository
    {
        public TempHTSClientExtractExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
