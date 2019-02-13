using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class TempPatientAdverseEventExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientAdverseEventExtractErrorSummary>, ITempPatientAdverseEventExtractErrorSummaryRepository
    {
        public TempPatientAdverseEventExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}