using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations.Base;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mnch.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Mnch.Validations
{
    public class TempHeiExtractErrorSummaryRepository: TempMnchExtractErrorSummaryRepository<TempHeiExtractErrorSummary>, ITempHeiExtractErrorSummaryRepository{public TempHeiExtractErrorSummaryRepository(ExtractsContext context) : base(context){}}
}
