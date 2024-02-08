using System; 

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    public  class TempHtsPartnerNotificationServices : TempHtsExtract
    {
        public int? PartnerPatientPk { get; set; }
        public int? PartnerPersonID { get; set; }
        public int? Age { get; set; }
        public  string  Sex	 { get; set; }
        public  string RelationsipToIndexClient { get; set; }
        public  string ScreenedForIpv { get; set; }
        public  string IpvScreeningOutcome { get; set; }
        public  string CurrentlyLivingWithIndexClient { get; set; }
        public string KnowledgeOfHivStatus { get; set; }
        public string PnsApproach { get; set; }
        public string PnsConsent { get; set; }
        public string LinkedToCare { get; set; }
        public  DateTime? LinkDateLinkedToCare { get; set; }
        public string CccNumber { get; set; }
        public string FacilityLinkedTo  { get; set; }
        public  DateTime?  Dob	 { get; set; }
        public  DateTime? DateElicited { get; set; }
        public string MaritalStatus { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public int? IndexPatientPk { get; set; }

        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
