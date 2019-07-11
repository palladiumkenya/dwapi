using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    public  class HtsTestKits : TempHTSExtract
    {

        public  int EncounterId { get; set; }
        public  string TestKitName1 { get; set; }
        public  string TestKitLotNumber1 { get; set; }
        public  string TestKitExpiry1 { get; set; }
        public  string TestResult1 { get; set; }
        public  string TestKitName2 { get; set; }
        public  string TestKitLotNumber2 { get; set; }
        public  string TestKitExpiry2{ get; set; }
        public  string TestResult2 { get; set; }
        public  int TestId { get; set; }
        public override string ToString()
        {
            return $"{SiteCode}-{HtsNumber}";
        }
    }
}
