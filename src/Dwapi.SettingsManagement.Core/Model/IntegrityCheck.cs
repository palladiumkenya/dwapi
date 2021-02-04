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
            {
                status = subject.ToString() == Logic ? LogicStatus.Pass : LogicStatus.Fail;
            }

            if (LogicType == LogicType.DateDiff)
            {
                DateTime intSubj = Convert.ToDateTime(subject);
                var daysSinceLastRefresh = DateTime.Today.Subtract(intSubj).Days;
                int intlogic = Convert.ToInt32(Logic);
                status = daysSinceLastRefresh > intlogic ? LogicStatus.Pass : LogicStatus.Fail;
            }

            if (LogicType == LogicType.NumericGT)
            {
                int intlogic = Convert.ToInt32(Logic);
                int intSubj = Convert.ToInt32(subject);

                status = intSubj >intlogic?  LogicStatus.Pass : LogicStatus.Fail;
            }

            if (LogicType == LogicType.NumericLT)
            {
                int intlogic = Convert.ToInt32(Logic);
                int intSubj = Convert.ToInt32(subject);

                status = intSubj < intlogic?  LogicStatus.Pass : LogicStatus.Fail;
            }

            if (LogicType == LogicType.NumericEQ)
            {
                int intlogic = Convert.ToInt32(Logic);
                int intSubj = Convert.ToInt32(subject);

                status = intSubj == intlogic?  LogicStatus.Pass : LogicStatus.Fail;
            }

            return new IntegrityCheckRun(status, Id);
        }

        public void UpdateLogic(string logic)
        {
            Logic = logic;
        }
    }

    public class IntegrityCheckRun:Entity<Guid>
    {
        public Guid IntegrityCheckId { get; set; }
        public DateTime RunDate { get; set; }
        public LogicStatus RunStatus { get; set; }

        public IntegrityCheckRun()
        {
        }
        public IntegrityCheckRun(LogicStatus runStatus,Guid integrityCheckId)
        {
            IntegrityCheckId = integrityCheckId;
            RunStatus = runStatus;
            RunDate=DateTime.Now;
        }
    }
}
