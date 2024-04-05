using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations
{
    public class TempCancerScreeningExtractErrorSummaryRepository: TempExtractErrorSummaryRepository<TempCancerScreeningExtractErrorSummary>, ITempCancerScreeningExtractErrorSummaryRepository{public TempCancerScreeningExtractErrorSummaryRepository(ExtractsContext context) : base(context){}}
}
