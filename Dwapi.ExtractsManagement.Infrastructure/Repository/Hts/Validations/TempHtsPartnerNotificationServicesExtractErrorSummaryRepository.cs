using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts
{ 
    public class TempHtsPartnerNotificationServicesExtractErrorSummaryRepository : TempHTSExtractErrorSummaryRepository<TempHtsPartnerNotificationServicesErrorSummary>, ITempHtsPartnerNotificationServicesErrorSummaryRepository
    {
        public TempHtsPartnerNotificationServicesExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
