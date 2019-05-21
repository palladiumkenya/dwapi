using System;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Hts
{
    public class HTSClientExtract : HTSExtract
    {
        public int EncounterId { get; set; }
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

        public string KeyPopulationType { get; set; }
        public string PopulationType{ get; set; }
        public string PatientDisabled{ get; set; }
        public string DisabilityType { get; set; }
        public string PatientConsented{ get; set; }
    }
}
