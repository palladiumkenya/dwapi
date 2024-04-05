using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsPartnerTracingError")]
    public class TempHtsPartnerTracingError
    {
        [NotMapped]
        public virtual ICollection<TempHtsPartnerTracingErrorSummary> TempHtsPartnerTracingErrorSummaries { get; set; } = new List<TempHtsPartnerTracingErrorSummary>();
        public string TraceType { get; set; }
        public int? PartnerPersonId { get; set; }
        public DateTime? TraceDate { get; set; }
        public string TraceOutcome { get; set; }
        public DateTime? BookingDate { get; set; }
        public string FacilityName { get; set; }
        public virtual int? SiteCode { get; set; }
        public virtual int? PatientPk { get; set; }
        public virtual string HtsNumber { get; set; }
        public virtual string Emr { get; set; }
        public virtual string Project { get; set; }
        public virtual bool CheckError { get; set; }
        public virtual DateTime? DateExtracted { get; set; } = DateTime.Now;
        [NotMapped]
        public virtual bool HasError { get; set; }
        public Guid Id { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
    }
}
