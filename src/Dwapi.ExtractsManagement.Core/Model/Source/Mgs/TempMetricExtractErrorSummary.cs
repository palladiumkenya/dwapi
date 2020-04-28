using System;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mgs
{
    public abstract class TempMetricExtractErrorSummary : Entity<Guid>
    {
        public virtual string Extract { get; set; }
        public virtual string Field { get; set; }
        public virtual string Type { get; set; }
        public virtual string Summary { get; set; }
        public virtual DateTime? DateGenerated { get; set; }
        public virtual int? SiteCode { get; set; }
        public virtual int? MetricId { get; set; }
        public virtual string FacilityName { get; set; }
        public virtual Guid RecordId { get; set; }

        public override string ToString()
        {
            return $"{SiteCode}|{Summary}";
        }

        protected string GetNullDateValue(DateTime? value)
        {
            return value?.ToString("dd-MMM-yyyy") ?? string.Empty;
        }
        protected string GetNullNumberValue(int? value)
        {
            return value?.ToString() ?? string.Empty;
        }
        protected string GetNullDecimalValue(decimal? value)
        {
            return value?.ToString() ?? string.Empty;
        }
    }
}
