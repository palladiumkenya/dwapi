using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Prep;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Prep
{
    [Table("vTempPrepLabExtractError")]public class TempPrepLabExtractError:TempExtract,IPrepLab
    {
        public string FacilityName { get; set; }
        public string PrepNumber { get; set; }
        public string HtsNumber { get; set; }
        public int? VisitID { get; set; }
        public string TestName { get; set; }
        public string TestResult { get; set; }
        public DateTime? SampleDate { get; set; }
        public DateTime? TestResultDate { get; set; }
        public string Reason { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
    }
}
