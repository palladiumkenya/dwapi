using System;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model.Diff
{
    public class DiffLog : Entity<Guid>
    {
        public string Docket { get; set; }
        public string Extract { get; set; }
        public DateTime? LastCreated { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? MaxCreated { get; set; }
        public DateTime? MaxModified { get; set; }
        public DateTime? LastSent { get; set; }
        public int SiteCode { get; set; }
        public DiffLog()
        {

        }
        public DiffLog(string docket, string extract, int siteCode)
        {
            Docket = docket;
            Extract = extract;
            SiteCode = siteCode;
        }

        public static DiffLog Create(string docket, string extract, int siteCode)
        {
            return new DiffLog(docket, extract, siteCode);
        }

        public void LogLoad(DateTime? maxCreated, DateTime? maxModified)
        {
            if (!maxCreated.IsNullOrEmpty())
                MaxCreated = maxCreated;
            if (!maxModified.IsNullOrEmpty())
                MaxModified = maxModified;
        }

        public void LogSent()
        {
            LastSent = DateTime.Now;
            LastCreated = MaxCreated;
            LastModified = MaxModified;
        }

        public bool CanUseDiff()
        {
            return !LastCreated.IsNullOrEmpty();
        }

        public bool HasDiff()
        {
            if (CanUseDiff())
            {
                return MaxCreated > LastCreated || MaxModified > LastCreated;
            }

            return false;
        }

        public string GenDiffChecker()
        {
            if (CanUseDiff())
            {
                return
                    $" {nameof(PatientExtract.Date_Created)}>={LastCreated.Value:yyyyMMdd} or {nameof(PatientExtract.Date_Last_Modified)}>={LastCreated.Value:yyyyMMdd} ";
            }

            return string.Empty;
        }
    }
}
