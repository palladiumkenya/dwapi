using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsTestKitsError")]
    public class TempHtsTestKitsError
    {
        [NotMapped]
        public virtual ICollection<TempHtsTestKitsErrorSummary> TempHtsTestKitsErrorSummaries { get; set; } = new List<TempHtsTestKitsErrorSummary>();
        public int EncounterId { get; set; }
        public string TestKitName1 { get; set; }
        public string TestKitLotNumber1 { get; set; }
        public string TestKitExpiry1 { get; set; }
        public string TestResult1 { get; set; }
        public string TestKitName2 { get; set; }
        public string TestKitLotNumber2 { get; set; }
        public string TestKitExpiry2 { get; set; }
        public string TestResult2 { get; set; } 
        public string FacilityName { get; set; }
        public virtual int? SiteCode { get; set; }
        public virtual int? PatientPk { get; set; }
        public virtual string HtsNumber { get; set; }
        public virtual string Emr { get; set; }
        public virtual string Project { get; set; }
        public virtual bool CheckError { get; set; }
        public virtual DateTime DateExtracted { get; set; } = DateTime.Now;
        [NotMapped]
        public virtual bool HasError { get; set; }
        public Guid Id { get; set; }
    }
}
