using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations
{
    public class TempDepressionScreeningExtractErrorSummaryRepository: TempExtractErrorSummaryRepository<TempDepressionScreeningExtractErrorSummary>, ITempDepressionScreeningExtractErrorSummaryRepository{public TempDepressionScreeningExtractErrorSummaryRepository(ExtractsContext context) : base(context){}}
}