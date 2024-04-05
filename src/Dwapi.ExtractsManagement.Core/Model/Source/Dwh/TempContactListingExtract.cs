using System;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    public class TempContactListingExtract : TempExtract,IContactListing
    {
        public string FacilityName { get; set; }
        public int ? PartnerPersonID { get; set; }
        public string ContactAge { get; set; }
        public string ContactSex { get; set; }
        public string ContactMaritalStatus { get; set; }
        public string RelationshipWithPatient { get; set; }
        public string ScreenedForIpv { get; set; }
        public string IpvScreening { get; set; }
        public string IPVScreeningOutcome { get; set; }
        public string CurrentlyLivingWithIndexClient { get; set; }
        public string KnowledgeOfHivStatus { get; set; }
        public string PnsApproach { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public int? ContactPatientPK { get; set; }
    }
}
