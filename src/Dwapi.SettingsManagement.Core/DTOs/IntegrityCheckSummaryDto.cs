using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;

namespace Dwapi.SettingsManagement.Core.DTOs
{
    public class IntegrityCheckSummaryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string Docket { get; set; }
        public string Status { get; set; }

        public ICollection<IntegrityCheckRun> IntegrityCheckRuns { get; set; } = new List<IntegrityCheckRun>();

        public IntegrityCheckSummaryDto(string name, string description, string message,string docket,List<IntegrityCheckRun> runs)
        {
            Name = name;
            Description = description;
            Docket = docket;

            Message = message;

            var run = runs.OrderByDescending(x => x.RunDate).FirstOrDefault();
            if (null!=run)
            {
                Status = $"{run.RunStatus}";
            }
        }

        public static List<IntegrityCheckSummaryDto> Generate(List<IntegrityCheck> checks)
        {
            var list = new List<IntegrityCheckSummaryDto>();


            foreach (var check in checks)
            {
                var summary = new IntegrityCheckSummaryDto(check.Name, check.Description, check.Message, check.Docket,
                    check.IntegrityCheckRuns.ToList());

                list.Add(summary);
            }

            return list;
        }
    }
}
