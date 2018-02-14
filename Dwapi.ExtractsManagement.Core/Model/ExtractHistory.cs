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
        public DateTime? StatusDate { get; set; }
        public string StatusInfo { get; set; }
        public Guid ExtractId { get; set; }

        public ExtractHistory()
        {
        }

        public override string ToString()
        {
            var dateInfo = StatusDate.HasValue ? $"[{StatusDate:F}]" : string.Empty;
            return $"{Status} {dateInfo} {StatusInfo}";
        }
    }
}