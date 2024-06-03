using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempRelationshipsExtractError")]
    public class TempRelationshipsExtractError : TempExtract,IRelationship
    {
        public string FacilityName { get; set; }
        public string RelationshipToPatient { get; set; }
        public int PersonAPatientPk { get; set; }
        public int PersonBPatientPk { get; set; }
        public string PatientRelationshipToOther { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
