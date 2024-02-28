using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Mts
{
    public class IndicatorExtract:Entity<Guid>
    {
        public string Indicator { get; set; }
        public string IndicatorValue { get; set; }
        public DateTime? IndicatorDate { get; set; }
        public DateTime DateCreated { get; set; }

        public  string Status { get; set; }
        public  DateTime? StatusDate { get; set; }
        public DateTime? DateExtracted { get; set; } = DateTime.Now;
        public int SiteCode { get; set; }


        [NotMapped]
        public bool IsSent => !string.IsNullOrWhiteSpace(Status) && Status.IsSameAs(nameof(SendStatus.Sent));
    }
}
