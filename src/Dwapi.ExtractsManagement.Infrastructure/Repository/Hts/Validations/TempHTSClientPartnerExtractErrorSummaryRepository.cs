using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.Validations.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.Validations
{
    public class TempHTSClientPartnerExtractErrorSummaryRepository : TempHTSExtractErrorSummaryRepository<TempHTSClientPartnerExtractErrorSummary>, ITempHTSClientPartnerExtractErrorSummaryRepository
    {
        public TempHTSClientPartnerExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
