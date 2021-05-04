using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations
{
    public class TempOvcExtractErrorSummaryRepository: TempExtractErrorSummaryRepository<TempOvcExtractErrorSummary>, ITempOvcExtractErrorSummaryRepository{public TempOvcExtractErrorSummaryRepository(ExtractsContext context) : base(context){}}
}