using System;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    public  class TempHtsTestKits : TempHtsExtract
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
        public  string SyphilisResult { get; set; } 
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
