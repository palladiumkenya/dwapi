using System;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Hts
{
    public class HTSClientLinkageExtract : HTSExtract
    {
        public DateTime? PhoneTracingDate { get; set; }
        public DateTime? PhysicalTracingDate { get; set; }
        public string TracingOutcome { get; set; }
        public string CccNumber { get; set; }
        public string EnrolledFacilityName { get; set; }
        public DateTime ReferralDate { get; set; }
        public DateTime DateEnrolled { get; set; }
        public string PNSConsent { get; set; }
    }
}
