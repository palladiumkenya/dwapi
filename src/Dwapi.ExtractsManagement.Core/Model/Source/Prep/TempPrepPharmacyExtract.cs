using System;
using Dwapi.Contracts.Prep;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Prep
{
    public class TempPrepPharmacyExtract:TempExtract,IPrepPharmacy
    {
        public string FacilityName { get; set; }
        public string PrepNumber { get; set; }
        public string HtsNumber { get; set; }
        public int? VisitID { get; set; }
        public string RegimenPrescribed { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
