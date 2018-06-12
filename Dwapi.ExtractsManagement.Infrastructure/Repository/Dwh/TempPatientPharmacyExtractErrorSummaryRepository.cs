using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class TempPatientPharmacyExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientPharmacyExtractErrorSummary>, ITempPatientPharmacyExtractErrorSummaryRepository
    {
        public TempPatientPharmacyExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}