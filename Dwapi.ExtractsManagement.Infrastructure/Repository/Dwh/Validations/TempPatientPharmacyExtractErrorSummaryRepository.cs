using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class TempPatientPharmacyExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientPharmacyExtractErrorSummary>, ITempPatientPharmacyExtractErrorSummaryRepository
    {
        public TempPatientPharmacyExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}