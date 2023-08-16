using System;

namespace Dwapi.Contracts.Ct
{
    public interface IDrugAlcoholScreening
    {
        string FacilityName { get; set; }
        int? VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        string DrinkingAlcohol { get; set; }
        string Smoking { get; set; }
        string DrugUse { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
        string RecordUUID { get; set; }

    }
}

