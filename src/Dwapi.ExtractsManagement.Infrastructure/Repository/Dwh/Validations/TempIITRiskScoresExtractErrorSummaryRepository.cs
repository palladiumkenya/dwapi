using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations.Base;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations
{
    public class TempIITRiskScoresExtractErrorSummaryRepository: TempExtractErrorSummaryRepository<TempIITRiskScoresExtractErrorSummary>, ITempIITRiskScoresExtractErrorSummaryRepository{public TempIITRiskScoresExtractErrorSummaryRepository(ExtractsContext context) : base(context){}}
}
