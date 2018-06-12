using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class TempPatientStatusExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientStatusExtractErrorSummary>, ITempPatientStatusExtractErrorSummaryRepository
    {
        public TempPatientStatusExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}