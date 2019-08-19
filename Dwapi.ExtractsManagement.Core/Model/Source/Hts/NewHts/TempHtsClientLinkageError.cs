using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsClientLinkageError")]
    public class TempHtsClientLinkageError
    {
        [NotMapped]
        public virtual ICollection<TempHtsClientLinkageErrorSummary> TempHtsClientLinkageErrorSummaries { get; set; } = new List<TempHtsClientLinkageErrorSummary>();
        public DateTime? DatePrefferedToBeEnrolled { get; set; }
        public string FacilityReferredTo { get; set; }
        public string HandedOverTo { get; set; }
        public string HandedOverToCadre { get; set; }
        public string EnrolledFacilityName { get; set; }
        public DateTime? ReferralDate { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public string ReportedCCCNumber { get; set; }
        public DateTime? ReportedStartARTDate { get; set; }
        public string FacilityName { get; set; }
        public virtual int? SiteCode { get; set; }
        public virtual int? PatientPk { get; set; }
        public virtual string HtsNumber { get; set; }
        public virtual string Emr { get; set; }
        public virtual string Project { get; set; }
        public virtual bool CheckError { get; set; }
        public virtual DateTime DateExtracted { get; set; } = DateTime.Now;
        [NotMapped]
        public virtual bool HasError { get; set; }
        public Guid Id { get; set; }
    }
}
