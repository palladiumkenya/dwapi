using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsClientLinkageErrorSummary")]
    public class TempHtsClientLinkageErrorSummary : TempHtsExtractErrorSummary
    { 
        public DateTime? DatePrefferedToBeEnrolled { get; set; }
        public string FacilityReferredTo { get; set; }
        public string HandedOverTo { get; set; }
        public string HandedOverToCadre { get; set; }
        public string EnrolledFacilityName { get; set; }
        public DateTime? ReferralDate { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public string ReportedCCCNumber { get; set; }
        public DateTime? ReportedStartARTDate { get; set; }
    }
}
