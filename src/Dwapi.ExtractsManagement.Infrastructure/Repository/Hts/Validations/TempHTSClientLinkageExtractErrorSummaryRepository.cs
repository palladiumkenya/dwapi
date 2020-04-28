using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.Validations.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.Validations
{
    public class TempHTSClientLinkageExtractErrorSummaryRepository : TempHTSExtractErrorSummaryRepository<TempHTSClientLinkageExtractErrorSummary>, ITempHTSClientLinkageExtractErrorSummaryRepository
    {
        public TempHTSClientLinkageExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
