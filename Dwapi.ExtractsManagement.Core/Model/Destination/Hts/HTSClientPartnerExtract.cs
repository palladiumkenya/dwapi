using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    public class HTSClientPartnerExtract : HTSExtract
    {

        public string RelationsipToIndexClient { get; set; }
        public string ScreenedForIPV { get; set; }
        public string IpvScreeningOutcome { get; set; }
        public string CurrentlyLivingWithIndexClient { get; set; }
        public string KnowledgeOfHivStatus { get; set; }
        public string PNSApproach { get; set; }
        public string Trace1Outcome { get; set; }
        public string Trace1Type { get; set; }
        public DateTime? Trace1Date { get; set; }
        public string Trace2Outcome { get; set; }
        public string Trace2Type { get; set; }
        public DateTime? Trace2Date { get; set; }
        public string Trace3Outcome { get; set; }
        public string Trace3Type { get; set; }
        public DateTime? Trace3Date { get; set; }
        public string PNSConsent { get; set; }
        public string Linked { get; set; }
        public DateTime? LinkDateLinkedToCareed { get; set; }
        public string CCCNumber { get; set; }
    }
}
