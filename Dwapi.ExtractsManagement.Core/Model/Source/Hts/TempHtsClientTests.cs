using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{

            
        public abstract class TempHtsClientTests : TempHTSExtract
        {
            public virtual int? EncounterId	 { get; set; }
            public virtual DateTime   TestDate	 { get; set; }
            public virtual string   EverTestedForHiv { get; set; }
            public virtual int   MonthsSinceLastTest { get; set; }
            public virtual string    ClientTestedAs	 { get; set; }
            public virtual  string  EntryPoint	 { get; set; }
            public virtual  string  TestStrategy	 { get; set; }
            public virtual  string  TestResult1	 { get; set; }
            public virtual string   TestResult2	 { get; set; }
            public virtual  string  FinalTestResult	 { get; set; }
            public virtual  string  PatientGivenResult	 { get; set; }
            public virtual  string  TbScreening	 { get; set; }
            public virtual string  ClientSelfTested	 { get; set; }
            public virtual string   CoupleDiscordant	 { get; set; }
            public virtual string   TestType	 { get; set; }
            public virtual string   Consent	 { get; set; }
                
            public override string ToString()
            {
                return $"{SiteCode}-{HtsNumber}";
            }
        }
}
