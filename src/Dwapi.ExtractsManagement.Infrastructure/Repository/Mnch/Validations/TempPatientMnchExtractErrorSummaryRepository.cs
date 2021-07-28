using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations.Base;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mnch.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Mnch.Validations
{
    public class TempPatientMnchExtractErrorSummaryRepository: TempMnchExtractErrorSummaryRepository<TempPatientMnchExtractErrorSummary>, ITempPatientMnchExtractErrorSummaryRepository{public TempPatientMnchExtractErrorSummaryRepository(ExtractsContext context) : base(context){}}
}
