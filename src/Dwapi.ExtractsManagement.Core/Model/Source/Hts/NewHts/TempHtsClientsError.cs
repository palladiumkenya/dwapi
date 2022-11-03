using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsClientsError")]
    public class TempHtsClientsError  
    {
        [NotMapped]
        public virtual ICollection<TempHtsClientsErrorSummary> TempHtsClientsErrorSummaries { get; set; } = new List<TempHtsClientsErrorSummary>();

        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string PopulationType { get; set; }
        public string KeyPopulationType { get; set; }
        public string PatientDisabled { get; set; }
        public string County { get; set; }
        public string SubCounty { get; set; }
        public string Ward { get; set; }
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
        public string NUPI { get; set; }
        public string Pkv { get; set; }
        public string Occupation { get; set; }
        public string PriorityPopulationType { get; set; }

    }
}
