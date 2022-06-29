using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    [Table("vTempHTSClientExtractErrorSummary")]
    public class TempHTSClientExtractErrorSummary: TempHTSExtractErrorSummary
    {
        public int? EncounterId { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string TestedBefore { get; set; }
        public int? MonthsLastTested { get; set; }
        public string ClientTestedAs { get; set; }
        public string StrategyHTS { get; set; }
        public string TestKitName1 { get; set; }
        public string TestKitLotNumber1 { get; set; }
        public DateTime? TestKitExpiryDate1 { get; set; }
        public string TestResultsHTS1 { get; set; }
        public string TestKitName2 { get; set; }
        public string TestKitLotNumber2 { get; set; }
        public string TestKitExpiryDate2 { get; set; }
        public string TestResultsHTS2 { get; set; }
        public string FinalResultHTS { get; set; }
        public string FinalResultsGiven { get; set; }
        public string TBScreeningHTS { get; set; }
        public string ClientSelfTested { get; set; }
        public string CoupleDiscordant { get; set; }
        public string TestType { get; set; }
        public string NUPI { get; set; }
    }
}
