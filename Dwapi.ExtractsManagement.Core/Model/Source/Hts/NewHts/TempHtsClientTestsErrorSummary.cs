using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsClientTestsExtractErrorSummary")]
    public class TempHtsClientTestsErrorSummary : TempHTSExtractErrorSummary
    {
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
    }
}
