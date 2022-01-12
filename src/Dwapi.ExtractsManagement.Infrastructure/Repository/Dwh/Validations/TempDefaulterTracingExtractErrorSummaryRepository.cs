using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations
{
    public class TempDefaulterTracingExtractErrorSummaryRepository: TempExtractErrorSummaryRepository<TempDefaulterTracingExtractErrorSummary>, ITempDefaulterTracingExtractErrorSummaryRepository{public TempDefaulterTracingExtractErrorSummaryRepository(ExtractsContext context) : base(context){}}
}
