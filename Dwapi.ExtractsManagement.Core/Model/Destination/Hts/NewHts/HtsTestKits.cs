using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts
{
    public  class HtsTestKits : HtsExtract
    {

        public  int? EncounterId { get; set; }
        public  string TestKitName1 { get; set; }
        public  string TestKitLotNumber1 { get; set; }
        public  string TestKitExpiry1 { get; set; }
        public  string TestResult1 { get; set; }
        public  string TestKitName2 { get; set; }
        public  string TestKitLotNumber2 { get; set; }
        public  string TestKitExpiry2{ get; set; }
        public  string TestResult2 { get; set; } 
    }
}
