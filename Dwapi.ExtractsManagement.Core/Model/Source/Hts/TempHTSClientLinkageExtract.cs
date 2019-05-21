using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
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
