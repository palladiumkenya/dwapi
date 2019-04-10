using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    public class TempHTSClientPartnerExtract : TempHTSExtract
    {
        public string RelationshipToIndexClient { get; set; }
        public string ScreenedForIpv { get; set; }
        public string IpvScreeningOutcome { get; set; }
        public string CurrentlyLivingWithIndexClient { get; set; }
        public string KnowledgeOfHivStatus { get; set; }
        public string PnsApproach { get; set; }
        public string Trace1Outcome { get; set; }
        public string Trace1Type { get; set; }
        public DateTime? Trace1Date { get; set; }
        public string Trace2Outcome { get; set; }
        public string Trace2Type { get; set; }
        public DateTime? Trace2Date { get; set; }
        public string Trace3Outcome { get; set; }
        public string Trace3Type { get; set; }
        public DateTime? Trace3Date { get; set; }
        public string PnsConsent { get; set; }
        public string Linked { get; set; }
        public DateTime? LinkDateLinkedToCare { get; set; }
        public int? PartnerId { get; set; }
        public string CccNumber { get; set; }
        public int? Age { get; set; }
        public string Sex { get; set; }
        public virtual int? PartnerSiteCode { get; set; }
        public virtual int? PartnerPatientPk { get; set; }
    }
}
