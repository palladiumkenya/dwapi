using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations
{
    public class TempPatientAdverseEventExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientAdverseEventExtractErrorSummary>, ITempPatientAdverseEventExtractErrorSummaryRepository
    {
        public TempPatientAdverseEventExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}