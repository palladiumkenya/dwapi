using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    public class HTSClientLinkageExtract : HTSExtract
    {
        public DateTime? PhoneTracing { get; set; }
        public DateTime? PhysicalTracing { get; set; }
        public string TracingOutcome { get; set; }
        public string CccNumber { get; set; }
        public DateTime ReferralDate { get; set; }
        public DateTime dateEnrolled { get; set; }
        public string PNSConsent { get; set; }
    
    }
}
