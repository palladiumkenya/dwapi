using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Utility;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Checks.Commands
{
    public class PerformSingleCheck :  IRequest<Result>
    {
        public Guid EmrSystemId { get; }
        public CheckStage Stage { get; }
        public string Version { get; set; }
        public Guid IntegrityCheckId { get;  }

        public PerformSingleCheck(Guid emrSystemId, CheckStage stage, string version, Guid integrityCheckId)
        {
            EmrSystemId = emrSystemId;
            Stage = stage;
            Version = version;
            IntegrityCheckId = integrityCheckId;
        }
    }

    public class PerformSingleCheckHandler : IRequestHandler<PerformSingleCheck, Result>
    {
        private readonly IIntegrityCheckRepository _integrityCheckRepository;

        public PerformSingleCheckHandler(IIntegrityCheckRepository integrityCheckRepository)
        {
            _integrityCheckRepository = integrityCheckRepository;
        }

        public async Task<Result> Handle(PerformSingleCheck request, CancellationToken cancellationToken)
        {
            _integrityCheckRepository.ClearById(request.IntegrityCheckId);


            var runs = new List<IntegrityCheckRun>();
            var checks = _integrityCheckRepository.GetAll().Where(x=>x.EmrSystemId==request.EmrSystemId).ToList();
            var indicators = _integrityCheckRepository.LoadIndicators().ToList();

            if (indicators.Any())
            {
                //    App Ver

                var appVersionCheck = checks.FirstOrDefault(x => x.Name == "AppVersion");
                if (null != appVersionCheck)
                {
                    var run = appVersionCheck.Run(request.Version);
                    runs.Add(run);
                }

                _integrityCheckRepository.Create(runs);
            }

            return Result.Success();
        }
    }
}
