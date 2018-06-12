using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class TempPatientVisitExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientVisitExtractErrorSummary>, ITempPatientVisitExtractErrorSummaryRepository
    {
        public TempPatientVisitExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}