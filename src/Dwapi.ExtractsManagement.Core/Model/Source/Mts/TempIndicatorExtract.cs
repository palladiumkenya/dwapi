using System;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mts
{
    public class TempIndicatorExtract : Entity<Guid>
    {
        public string Indicator { get; set; }
        public string IndicatorValue { get; set; }
        public DateTime? IndicatorDate { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int SiteCode { get; set; }

    }
}
