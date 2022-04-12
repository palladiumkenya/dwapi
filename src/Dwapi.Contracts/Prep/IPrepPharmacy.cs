using System;

namespace Dwapi.Contracts.Prep
{
    public interface IPrepPharmacy
    {
        string FacilityName { get; set; }
        string PrepNumber { get; set; }
        string HtsNumber { get; set; }
        int? VisitID { get; set; }
        string RegimenPrescribed { get; set; }
        DateTime? DispenseDate { get; set; }
        decimal? Duration { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
