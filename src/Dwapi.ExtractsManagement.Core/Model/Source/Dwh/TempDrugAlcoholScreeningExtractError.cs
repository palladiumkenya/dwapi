using System;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    public class TempDrugAlcoholScreeningExtractError : TempExtract,IDrugAlcoholScreening
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string DrinkAlcohol { get; set; }
        public string Smoking { get; set; }
        public string DrugUse { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
