using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations
{
    public class TempOtzExtractErrorSummaryRepository: TempExtractErrorSummaryRepository<TempOtzExtractErrorSummary>, ITempOtzExtractErrorSummaryRepository{public TempOtzExtractErrorSummaryRepository(ExtractsContext context) : base(context){}}
}