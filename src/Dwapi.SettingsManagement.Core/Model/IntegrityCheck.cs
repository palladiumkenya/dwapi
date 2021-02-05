using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class IntegrityCheck : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public LogicType LogicType { get; set; }
        public string Logic { get; set; }
        public CheckStage Stage { get; set; }
        public string Docket { get; set; }
        public Guid EmrSystemId { get; set; }
        public ICollection<IntegrityCheckRun> IntegrityCheckRuns { get; set; } = new List<IntegrityCheckRun>();

        public IntegrityCheck()
        {
        }

        public IntegrityCheckRun Run(string subject)
        {
            var status = LogicStatus.None;

            if (LogicType == LogicType.AppVer)
                status = subject == Logic ? LogicStatus.Pass : LogicStatus.Fail;

            if (LogicType == LogicType.DateDiff)
            {
                DateTime dateSubj = Convert.ToDateTime(subject);
                var daysSinceLastRefresh = DateTime.Today.Subtract(dateSubj).Days;
                int days = Convert.ToInt32(Logic);
                status = daysSinceLastRefresh > days ? LogicStatus.Pass : LogicStatus.Fail;
            }

            if (LogicType == LogicType.Numeric)
            {
                int intSubj = Convert.ToInt32(subject);
                int intlogic = Convert.ToInt32(Logic);
                status = intSubj > intlogic ? LogicStatus.Pass : LogicStatus.Fail;
            }

            return new IntegrityCheckRun(status, Id, subject);
        }

        public IntegrityCheckRun Run(DateTime subject)
        {
            return Run(subject.ToString("yyyyMMMdd"));
        }

        public void UpdateLogic(string logic)
        {
            Logic = logic;
        }
    }
}
