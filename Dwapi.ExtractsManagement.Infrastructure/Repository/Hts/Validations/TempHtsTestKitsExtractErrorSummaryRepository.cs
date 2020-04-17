using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts
{ 
    public class TempHtsTestKitsExtractErrorSummaryRepository : TempHTSExtractErrorSummaryRepository<TempHtsTestKitsErrorSummary>, ITempHtsTestKitsErrorSummaryRepository
    {
        public TempHtsTestKitsExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
