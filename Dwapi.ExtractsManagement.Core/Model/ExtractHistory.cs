using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model
{
    public class ExtractHistory : Entity<Guid>
    {
        public ExtractStatus Status { get; set; } = ExtractStatus.Idle;
        public int? Found { get; set; }
        public DateTime? DateFound { get; set; }
        public int? Loaded { get; set; }
        public DateTime? DateLoaded { get; set; }
        public int? Accepted { get; set; }
        public int? Rejected { get; set; }
        public DateTime? DateValidated { get; set; }
        public int? Queued { get; set; }
        public int? Sent { get; set; }
        public DateTime? DateSent { get; set; }
        public Guid ExtractId { get; set; }

        public ExtractHistory()
        {
        }

        private ExtractHistory(ExtractStatus status)
        {
            Status = status;
        }

        private ExtractHistory(Guid id, ExtractStatus status) : base(id)
        {
            Status = status;
        }

        private static ExtractStatus GetNextStatus(ExtractStatus status)
        {
            var max = Enum.GetValues(typeof(ExtractStatus)).Cast<int>().Max();

            if (Convert.ToInt32(status) < max)
                return status + 1;

            return status;
        }

        private static bool IsLastOne(ExtractStatus status)
        {
            var max = Enum.GetValues(typeof(ExtractStatus)).Cast<int>().Max();

            return Convert.ToInt16(status) == max;
        }

        public static ExtractHistory UpdateHistory(ExtractHistory currentHistory)
        {
            if (null == currentHistory)
                throw new ArgumentException("History not intialized");
            
            if (!IsLastOne(currentHistory.Status))
            {
                var history = new ExtractHistory(currentHistory.Id, GetNextStatus(currentHistory.Status));
                return history;
            }
        
            return currentHistory;
        }

        public static ExtractHistory UpdateHistoryStats(ExtractHistory currentHistory, int stats)
        {
            var id = currentHistory.Id;
            var status = currentHistory.Status;

            //  Finding
            if (!IsLastOne(status) && status == ExtractStatus.Finding)
            {
                var history = new ExtractHistory(id, GetNextStatus(status));
                history.Found = stats;
                history.DateFound = DateTime.Now;
                return history;
            }

            //  Loading
            if (!IsLastOne(status) && status == ExtractStatus.Loading)
            {
                var history = new ExtractHistory(id, GetNextStatus(status));
                history.Loaded = stats;
                history.DateLoaded = DateTime.Now;
                return history;
            }

            //  Validating
            if (!IsLastOne(status) && status == ExtractStatus.Validating)
            {
                var history = new ExtractHistory(id, GetNextStatus(status));
                history.Rejected = stats;
                history.Accepted = history.Loaded = stats;
                history.Queued = history.Accepted;
                history.DateValidated = DateTime.Now;
                return history;
            }

            //  Sending
            if (!IsLastOne(status) && status == ExtractStatus.Sending)
            {
                var history = new ExtractHistory(id, GetNextStatus(status));
                history.Sent = stats;
                history.Queued = history.Queued - stats;
                history.DateSent = DateTime.Now;
                return history;
            }

            return currentHistory;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (DateFound.HasValue)
                sb.AppendLine($"{nameof(Found)}:{Found} |{DateFound:F}");
            if (DateLoaded.HasValue)
                sb.AppendLine($"{nameof(Loaded)}:{Loaded} |{DateLoaded:F}");
            if (DateValidated.HasValue)
                sb.AppendLine($"{nameof(Accepted)}:{Accepted},{nameof(Rejected)}:{Rejected} |{DateValidated:F}");
            if (DateSent.HasValue)
                sb.AppendLine($"{nameof(Sent)}:{Sent},{nameof(Queued)}:{Queued} |{DateSent:F}");

            return $"{sb} >> {Status}...";
        }
    }
}