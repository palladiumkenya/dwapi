using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations
{
    public class TempGbvScreeningExtractErrorSummaryRepository: TempExtractErrorSummaryRepository<TempGbvScreeningExtractErrorSummary>, ITempGbvScreeningExtractErrorSummaryRepository{public TempGbvScreeningExtractErrorSummaryRepository(ExtractsContext context) : base(context){}}
}