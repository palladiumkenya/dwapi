using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Dwapi.SettingsManagement.Core.DTOs;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Checks.Queries
{
    public class GetCheckSummary:IRequest<Result<List<IntegrityCheckSummaryDto>>>
    {
    }

    public class GetCheckSummaryHandler : IRequestHandler<GetCheckSummary, Result<List<IntegrityCheckSummaryDto>>>
    {
        private readonly IIntegrityCheckRepository _integrityCheckRepository;

        public GetCheckSummaryHandler(IIntegrityCheckRepository integrityCheckRepository)
        {
            _integrityCheckRepository = integrityCheckRepository;
        }


        public Task<Result<List<IntegrityCheckSummaryDto>>> Handle(GetCheckSummary request, CancellationToken cancellationToken)
        {
            var checks = _integrityCheckRepository.LoadAll().ToList();

            var summaries = IntegrityCheckSummaryDto.Generate(checks);

            return Task.FromResult(Result.Success<List<IntegrityCheckSummaryDto>>(summaries));
        }
    }
}
