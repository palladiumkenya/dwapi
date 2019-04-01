using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    public class HTSClientLinkage : HTSExtract
    {
        public DateTime? PhoneTracing { get; set; }
        public DateTime? PhysicalTracing { get; set; }
        public string tracingOutcome { get; set; }
        public string CCCNumber { get; set; }
        public DateTime ReferralDate { get; set; }
        public DateTime dateEnrolled { get; set; }
        public string PNSConsent { get; set; }
    
    }
}
