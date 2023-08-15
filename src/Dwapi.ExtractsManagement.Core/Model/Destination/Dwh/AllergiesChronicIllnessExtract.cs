using System;
using Dwapi.Contracts.Ct;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{
    public class AllergiesChronicIllnessExtract : ClientExtract, IAllergiesChronicIllness
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string ChronicIllness { get; set; }
        public DateTime? ChronicOnsetDate { get; set; }
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
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
