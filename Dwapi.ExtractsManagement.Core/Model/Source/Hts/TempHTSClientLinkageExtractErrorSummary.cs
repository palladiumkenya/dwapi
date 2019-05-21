using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    [Table("vTempHTSClientLinkageExtractErrorSummary")]
    public class TempHTSClientLinkageExtractErrorSummary: TempHTSExtractErrorSummary
    {
        public DateTime? PhoneTracingDate { get; set; }
        public DateTime? PhysicalTracingDate { get; set; }
        public string TracingOutcome { get; set; }
        public string CccNumber { get; set; }
        public DateTime? ReferralDate { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public string  EnrolledFacilityName { get; set; }
    }
}