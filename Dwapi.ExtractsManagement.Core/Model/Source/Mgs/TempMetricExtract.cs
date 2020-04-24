using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mgs
{
    public abstract class TempMetricExtract : Entity<Guid>
    {
        public string FacilityName { get; set; }
        public int MetricId { get; set; }
        public virtual int? SiteCode { get; set; }
        public virtual string Emr { get; set; }
        public string Project { get; set; }
        public virtual bool CheckError { get; set; }
        public virtual ErrorType ErrorType { get; set; }
        public virtual DateTime DateExtracted { get; set; } = DateTime.Now;
        [NotMapped]
        public virtual bool HasError { get; set; }

        public override string ToString()
        {
            return $"{SiteCode}";
        }
    }
}
