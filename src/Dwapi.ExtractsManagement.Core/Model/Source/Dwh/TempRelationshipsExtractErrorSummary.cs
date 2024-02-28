using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempRelationshipsExtractErrorSummary")]
    public class TempRelationshipsExtractErrorSummary : TempExtractErrorSummary,IRelationship
    {
        public string FacilityName { get; set; }
        public string RelationshipToPatient { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
