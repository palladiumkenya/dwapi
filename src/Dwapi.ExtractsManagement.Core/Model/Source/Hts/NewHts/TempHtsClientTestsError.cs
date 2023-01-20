using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsClientTestsError")]
    public class TempHtsClientTestsError
    {
        [NotMapped]
        public virtual ICollection<TempHtsClientTestsErrorSummary> TempHtsClientTestsErrorSummaries { get; set; } = new List<TempHtsClientTestsErrorSummary>();
        public int? EncounterId { get; set; }
        public DateTime? TestDate { get; set; }
        public string EverTestedForHiv { get; set; }
        public int? MonthsSinceLastTest { get; set; }
        public string ClientTestedAs { get; set; }
        public string EntryPoint { get; set; }
        public string TestStrategy { get; set; }
        public string TestResult1 { get; set; }
        public string TestResult2 { get; set; }
        public string FinalTestResult { get; set; }
        public string PatientGivenResult { get; set; }
        public string TbScreening { get; set; }
        public string ClientSelfTested { get; set; }
        public string CoupleDiscordant { get; set; }
        public string TestType { get; set; }
        public string Consent { get; set; }
        public string FacilityName { get; set; }
        public virtual int? SiteCode { get; set; }
        public virtual int? PatientPk { get; set; }
        public virtual string HtsNumber { get; set; }
        public virtual string Emr { get; set; }
        public virtual string Project { get; set; }
        public virtual bool CheckError { get; set; }
        public virtual DateTime? DateExtracted { get; set; } = DateTime.Now;
        [NotMapped]
        public virtual bool HasError { get; set; }
        public Guid Id { get; set; }
        public  string    Setting	 { get; set; }
        public  string    Approach	 { get; set; }
        public  string HtsRiskCategory	 { get; set; }
        public  decimal? HtsRiskScore	 { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
