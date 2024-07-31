using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempAllergiesChronicIllnessExtractErrorSummary")]
    public class TempAllergiesChronicIllnessExtractErrorSummary : TempExtractErrorSummary,IAllergiesChronicIllness
    {
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string ChronicIllness { get; set; }
        public string ChronicOnsetDate { get; set; }
        public string knownAllergies { get; set; }
        public string AllergyCausativeAgent { get; set; }
        public string AllergicReaction { get; set; }
        public string AllergySeverity { get; set; }
        public DateTime? AllergyOnsetDate { get; set; }
        public string Skin { get; set; }
        public string Eyes { get; set; }
        public string ENT { get; set; }
        public string Chest { get; set; }
        public string CVS { get; set; }
        public string Abdomen { get; set; }
        public string CNS { get; set; }
        public string Genitourinary { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public string Controlled { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientHasChronicIllness { get; set; }

    }
}
