using System; 

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{            
        public  class TempHtsClientTests : TempHtsExtract
        {
            public  int? EncounterId	 { get; set; }
            public  DateTime?   TestDate	 { get; set; }
            public  string   EverTestedForHiv { get; set; }
            public  int?   MonthsSinceLastTest { get; set; }
            public  string    ClientTestedAs	 { get; set; }
            public   string  EntryPoint	 { get; set; }
            public   string  TestStrategy	 { get; set; }
            public   string  TestResult1	 { get; set; }
            public  string   TestResult2	 { get; set; }
            public   string  FinalTestResult	 { get; set; }
            public   string  PatientGivenResult	 { get; set; }
            public   string  TbScreening	 { get; set; }
            public  string  ClientSelfTested	 { get; set; }
            public  string   CoupleDiscordant	 { get; set; }
            public  string   TestType	 { get; set; }
            public  string   Consent	 { get; set; }
            public  string    Setting	 { get; set; }
            public  string    Approach	 { get; set; }
            public  string HtsRiskCategory	 { get; set; }
            public  decimal? HtsRiskScore	 { get; set; }
            
            public  string ReferredForServices { get; set; }
            public  string ReferredServices { get; set; }
            public  string OtherReferredServices { get; set; }
            
            public DateTime? Date_Created { get; set; }
            public DateTime? Date_Last_Modified { get; set; }
        }
}
