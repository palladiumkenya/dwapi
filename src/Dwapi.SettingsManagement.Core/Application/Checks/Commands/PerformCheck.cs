using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Checks.Commands
{
    public class PerformCheck:IRequest<Result>
    {
        public Guid EmrSystemId { get;  }
        public CheckStage Stage { get; }

        public PerformCheck()
        {
        }

        public PerformCheck(Guid emrSystemId, CheckStage stage)
        {
            EmrSystemId = emrSystemId;
            Stage = stage;
        }
    }

    public class PerformCheckHandler : IRequestHandler<PerformCheck,Result>
    {
        private readonly IIntegrityCheckRepository _integrityCheckRepository;

        public PerformCheckHandler(IIntegrityCheckRepository integrityCheckRepository)
        {
            _integrityCheckRepository = integrityCheckRepository;
        }

        public async Task<Result> Handle(PerformCheck request, CancellationToken cancellationToken)
        {
            _integrityCheckRepository.Clear();

            var runs=new List<IntegrityCheckRun>();
            var checks = _integrityCheckRepository.GetAll().ToList();
            var indicators = _integrityCheckRepository.LoadIndicators().ToList();
            var metrics = _integrityCheckRepository.LoadIndicators().ToList();

            if (indicators.Any())
            {
                //    ETL  EMR_ETL_Refresh
                var indEtl = indicators.FirstOrDefault(x => x.Indicator == "EMR_ETL_Refresh");

                if (null != indEtl)
                {
                    var check = checks.FirstOrDefault(x => x.Name == "EMR_ETL_Refresh");
                    if (null != check)
                    {
                        var run = check.Run(indEtl.IndicatorValue);
                        runs.Add(run);
                    }
                }

                //    Log in Date
                //    Last Encounter date
                // TX Curr
                // MFL Codes

                _integrityCheckRepository.Create(runs);
            }

            return Result.Success();
        }
    }
}
