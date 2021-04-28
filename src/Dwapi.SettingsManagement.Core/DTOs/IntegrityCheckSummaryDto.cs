using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using Humanizer;

namespace Dwapi.SettingsManagement.Core.DTOs
{
    public class IntegrityCheckSummaryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string Docket { get; set; }
        public string Status { get; set; }
        public DateTime RunDate { get; set; }
        public string TimeAgo => GetTimeAgo();
        private bool Deferred => CheckDeferred();
        private bool CheckDeferred()
        {
            var list = new List<string>() {"MFL_CODE"};
            return list.Any(x => x.ToLower() == Name.ToLower());
        }

        public IntegrityCheckSummaryDto(string name, string description, string logic, string message, string docket,
            List<IntegrityCheckRun> runs)
        {
            Name = FormatName(name);
            Description = description;
            Docket = docket;

            var run = runs.OrderByDescending(x => x.RunDate).FirstOrDefault();
            if (null != run)
            {
                RunDate = run.RunDate;
                Message = ParseMessage(message, run, logic);
                Status = $"{run.RunStatus}";
            }
            else
            {
                Message = "Not uploaded";
                Status= $"{LogicStatus.Fail}";
            }
        }

        private string FormatName(string name)
        {
            return name.Replace('_', ' ');
        }


        public static List<IntegrityCheckSummaryDto> Generate(List<IntegrityCheck> checks)
        {
            var list = new List<IntegrityCheckSummaryDto>();

            foreach (var check in checks)
            {
                var summary = new IntegrityCheckSummaryDto(check.Name, check.Description, check.Logic, check.Message,
                    check.Docket,
                    check.IntegrityCheckRuns.ToList());

                if(!summary.Deferred)
                    list.Add(summary);
            }

            return list;
        }

        private string GetTimeAgo()
        {
            if (RunDate.Year < 1983)
                return string.Empty;
            return RunDate.Humanize(false);
        }

        private string ParseMessage(string message, IntegrityCheckRun run, string logic)
        {
            var finalLogic = ParseLogic(run, logic);
            var finalMessage = message.Replace("{0}", run.Finding).Replace("{1}", finalLogic);

            if (run.RunStatus == LogicStatus.Pass)
            {
                finalMessage = finalMessage.Split('|')[0];
            }
            else
            {
                finalMessage = finalMessage.Split('|')[1];
            }

            return finalMessage;
        }

        private string ParseLogic(IntegrityCheckRun run, string logic)
        {
            if (
                run.IntegrityCheckId == new Guid("d0586c5e-678a-11eb-ae93-0242ac130002") ||
                run.IntegrityCheckId == new Guid("d0586e3e-678a-11eb-ae93-0242ac130002") ||
                run.IntegrityCheckId == new Guid("d0586f06-678a-11eb-ae93-0242ac130002"))
            {
                //
                var date = DateTime.TryParse(run.Finding, out DateTime local);
                if (date)
                {
                    var daysElapsed = DateTime.Today.Subtract(local).Days;
                    logic = $"{daysElapsed}";
                }
            }

            return logic;
        }
    }
}
