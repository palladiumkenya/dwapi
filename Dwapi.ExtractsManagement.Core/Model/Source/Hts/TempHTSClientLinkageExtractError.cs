using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    [Table("vTempHTSClientLinkageExtractError")]
    public class TempHTSClientLinkageExtractError
    {

        [NotMapped]
        public virtual ICollection<TempHTSClientLinkageExtractErrorSummary> TempHtsClientLinkageExtractErrorSummaries { get; set; } = new List<TempHTSClientLinkageExtractErrorSummary>();

        public DateTime? PhoneTracingDate { get; set; }
        public DateTime? PhysicalTracingDate { get; set; }
        public string TracingOutcome { get; set; }
        public string CccNumber { get; set; }
        public DateTime? ReferralDate { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public string EnrolledFacilityName { get; set; }
        public string FacilityName { get; set; }
        public int? SiteCode { get; set; }
        public int? PatientPk { get; set; }
        public string HtsNumber { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool CheckError { get; set; }
        public DateTime DateExtracted { get; set; }

        public Guid Id { get; set; }
    }
}
