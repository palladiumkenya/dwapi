using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;
using Dwapi.ExtractsManagement.Core.Model.Source.Prep;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Prep.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Prep.Validations
{
    public class TempPrepCareTerminationExtractErrorSummaryRepository: TempPrepExtractErrorSummaryRepository<TempPrepCareTerminationExtractErrorSummary>, ITempPrepCareTerminationExtractErrorSummaryRepository{public TempPrepCareTerminationExtractErrorSummaryRepository(ExtractsContext context) : base(context){}}
}
