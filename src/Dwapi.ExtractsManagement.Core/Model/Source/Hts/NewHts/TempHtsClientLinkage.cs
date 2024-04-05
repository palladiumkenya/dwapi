using System;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{       
    public  class TempHtsClientLinkage : TempHtsExtract
    {
        public  DateTime?  DatePrefferedToBeEnrolled { get; set; }
        public  string FacilityReferredTo { get; set; }
        public  string  HandedOverTo { get; set; }
        public  string HandedOverToCadre { get; set; }
        public  string EnrolledFacilityName { get; set; }
        public  DateTime? ReferralDate { get; set; }
        public  DateTime? DateEnrolled { get; set; }
        public  string ReportedCCCNumber { get; set; }
        public  DateTime? ReportedStartARTDate { get; set; } 
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
