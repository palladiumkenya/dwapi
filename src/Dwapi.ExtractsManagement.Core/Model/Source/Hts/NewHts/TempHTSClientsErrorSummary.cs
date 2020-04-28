using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsClientsExtractErrorSummary")]
    public class TempHtsClientsErrorSummary : TempHTSExtractErrorSummary
    {
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string PopulationType { get; set; }
        public string KeyPopulationType { get; set; }
        public string PatientDisabled { get; set; }
        public string County { get; set; }
        public string SubCounty { get; set; }
        public string Ward { get; set; } 
    }
}
