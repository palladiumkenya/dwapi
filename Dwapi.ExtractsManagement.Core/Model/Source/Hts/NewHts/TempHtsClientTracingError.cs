using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsClientTracingError")]
    public class TempHtsClientTracingError
    {
        [NotMapped]
        public virtual ICollection<TempHtsClientTracingErrorSummary> TempHtsClientTracingErrorSummaries { get; set; } = new List<TempHtsClientTracingErrorSummary>();
        public DateTime? TracingType { get; set; }
        public DateTime? TracingDate { get; set; }
        public string TracingOutcome { get; set; }
        public string FacilityName { get; set; }
        public virtual int? SiteCode { get; set; }
        public virtual int? PatientPk { get; set; }
        public virtual string HtsNumber { get; set; }
        public virtual string Emr { get; set; }
        public virtual string Project { get; set; }
        public virtual bool CheckError { get; set; }
        public virtual DateTime  DateExtracted { get; set; } = DateTime.Now;
        [NotMapped]
        public virtual bool HasError { get; set; }
        public Guid Id { get; set; }
    }
}
