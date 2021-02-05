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
    public class PerformCheck : IRequest<Result>
    {
        public Guid EmrSystemId { get; }
        public CheckStage Stage { get; }
        public string Version { get; set; }

        public PerformCheck(Guid emrSystemId, CheckStage stage, string version)
        {
            EmrSystemId = emrSystemId;
            Stage = stage;
            Version = version;
        }
    }

    public class PerformCheckHandler : IRequestHandler<PerformCheck, Result>
    {
        private readonly IIntegrityCheckRepository _integrityCheckRepository;

        public PerformCheckHandler(IIntegrityCheckRepository integrityCheckRepository)
        {
            _integrityCheckRepository = integrityCheckRepository;
        }

        public async Task<Result> Handle(PerformCheck request, CancellationToken cancellationToken)
        {
            _integrityCheckRepository.Clear();

            var runs = new List<IntegrityCheckRun>();
            var checks = _integrityCheckRepository.GetAll().Where(x=>x.EmrSystemId==request.EmrSystemId).ToList();
            var indicators = _integrityCheckRepository.LoadIndicators().ToList();
            var metrics = _integrityCheckRepository.LoadEmrMetrics().ToList();

            if (indicators.Any())
            {
                //    App Ver

                var appVersionCheck = checks.FirstOrDefault(x => x.Name == "AppVersion");
                if (null != appVersionCheck)
                {
                    var run = appVersionCheck.Run(request.Version);
                    runs.Add(run);
                }

                //    ETL  EMR_ETL_Refresh
                var emrEtlRefresh = indicators.FirstOrDefault(x => x.Indicator == "EMR_ETL_Refresh");
                if (null != emrEtlRefresh)
                {
                    var check = checks.FirstOrDefault(x => x.Name == "EMR_ETL_Refresh");
                    if (null != check)
                    {
                        var run = check.Run(emrEtlRefresh.IndicatorValue);
                        runs.Add(run);
                    }
                }

                //    Last Encounter date
                var lastEncounter = indicators.FirstOrDefault(x => x.Indicator == "LAST_ENCOUNTER_CREATE_DATE");

                if (null != lastEncounter)
                {
                    var check = checks.FirstOrDefault(x => x.Name == "LAST_ENCOUNTER_CREATE_DATE");
                    if (null != check)
                    {
                        var run = check.Run(lastEncounter.IndicatorValue);
                        runs.Add(run);
                    }
                }

                //TODO:  Last Log in Date from Indicator

                var emrLogin = metrics.FirstOrDefault();
                if (null != emrLogin && emrLogin.LastLoginDate.HasValue)
                {
                    var check = checks.FirstOrDefault(x => x.Name == "LastLoginDate");
                    if (null != check)
                    {
                        var run = check.Run(emrLogin.LastLoginDate.Value);
                        runs.Add(run);
                    }
                }

                // TX Curr

                var txCurr = indicators.FirstOrDefault(x => x.Indicator == "TX_CURR");

                if (null != txCurr)
                {
                    var check = checks.FirstOrDefault(x => x.Name == "TX_CURR");
                    if (null != check)
                    {
                        var run = check.Run(txCurr.IndicatorValue);
                        runs.Add(run);
                    }
                }

                // MFL Codes

                _integrityCheckRepository.Create(runs);
            }

            return Result.Success();
        }
    }
}
