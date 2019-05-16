using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts
{
    public class TempHTSClientExtractErrorSummaryRepository :TempHTSExtractErrorSummaryRepository<TempHTSClientExtractErrorSummary>, ITempHTSClientExtractErrorSummaryRepository
    {
        public TempHTSClientExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
