using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientStatusExtractError")]
    public class TempPatientStatusExtractError : TempExtract
    {
        

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

        public string FacilityName { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }

        public virtual ICollection<TempPatientStatusExtractErrorSummary> TempPatientStatusExtractErrorSummaries { get; set; } = new List<TempPatientStatusExtractErrorSummary>();
    }
}
