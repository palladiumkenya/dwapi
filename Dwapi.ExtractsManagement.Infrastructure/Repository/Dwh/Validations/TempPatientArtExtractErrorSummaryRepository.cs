using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class TempPatientArtExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientArtExtractErrorSummary>, ITempPatientArtExtractErrorSummaryRepository
    {
        public TempPatientArtExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
        }
    }
}