using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.Validations.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.Validations
{
    public class TempHTSClientExtractErrorSummaryRepository :TempHTSExtractErrorSummaryRepository<TempHTSClientExtractErrorSummary>, ITempHTSClientExtractErrorSummaryRepository
    {
        public TempHTSClientExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
