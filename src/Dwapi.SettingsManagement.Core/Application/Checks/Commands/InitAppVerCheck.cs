using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Dwapi.SettingsManagement.Core.Application.Checks.Queries;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Checks.Commands
{
    public class InitAppVerCheck:IRequest<Result>
    {

    }

    public class InitAppVerCheckHandler : IRequestHandler<InitAppVerCheck, Result>
    {
        private readonly IMediator _mediator;
        private readonly IIntegrityCheckRepository _integrityCheckRepository;

        public InitAppVerCheckHandler(IMediator mediator, IIntegrityCheckRepository integrityCheckRepository)
        {
            _mediator = mediator;
            _integrityCheckRepository = integrityCheckRepository;
        }

        public async Task<Result> Handle(InitAppVerCheck request, CancellationToken cancellationToken)
        {
            var current = await _mediator.Send(new CheckLiveUpdate(string.Empty));
            if (current.IsSuccess)
            {
                if (!string.IsNullOrWhiteSpace(current.Value.LiveVersion))
                {
                    var check = _integrityCheckRepository.GetAll().ToList().FirstOrDefault(x => x.Name=="AppVersion");
                    if (null != check)
                    {
                        check.UpdateLogic(current.Value.LiveVersion.Trim());
                        _integrityCheckRepository.Update<IntegrityCheck>(new List<IntegrityCheck> {check});
                        return Result.Success();
                    }

                }
            }

            return Result.Failure("Unkown error");
        }
    }
}
