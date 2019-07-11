using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    public abstract class TempHtsTestKits : TempHTSExtract
    {

        public virtual int EncounterId { get; set; }
        public virtual string TestKitName1 { get; set; }
        public virtual string TestKitLotNumber1 { get; set; }
        public virtual string TestKitExpiry1 { get; set; }
        public virtual string TestResult1 { get; set; }
        public virtual string TestKitName2 { get; set; }
        public virtual string TestKitLotNumber2 { get; set; }
        public virtual string TestKitExpiry2{ get; set; }
        public virtual string TestResult2 { get; set; }
        public virtual int TestId { get; set; }
        public override string ToString()
        {
            return $"{SiteCode}-{HtsNumber}";
        }
    }
}
