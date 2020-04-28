using System;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    [Obsolete("Class is obsolete,use TempHtsClientLinkage")]
    public class TempHTSClientLinkageExtract : TempHTSExtract
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
