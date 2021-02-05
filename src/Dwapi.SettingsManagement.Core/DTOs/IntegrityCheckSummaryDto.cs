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

        public IntegrityCheckSummaryDto(string name, string description,string logic, string message,string docket,List<IntegrityCheckRun> runs)
        {
            Name = name;
            Description = description;
            Docket = docket;

            var run = runs.OrderByDescending(x => x.RunDate).FirstOrDefault();
            if (null!=run)
            {
                RunDate = run.RunDate;
                Message = message.Replace("{0}", run.Finding).Replace("{1}", logic);
                Status = $"{run.RunStatus}";
            }
        }

        public static List<IntegrityCheckSummaryDto> Generate(List<IntegrityCheck> checks)
        {
            var list = new List<IntegrityCheckSummaryDto>();

            foreach (var check in checks)
            {
                var summary = new IntegrityCheckSummaryDto(check.Name, check.Description,check.Logic, check.Message, check.Docket,
                    check.IntegrityCheckRuns.ToList());

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
    }
}
