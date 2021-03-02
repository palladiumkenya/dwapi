using System;

namespace Dwapi.Contracts.Ct
{
    public interface IIPt
    {
        string FacilityName { get; set; }
        int? VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        string OnTBDrugs { get; set; }
        string OnIPT { get; set; }
        string EverOnIPT { get; set; }
        string Cough { get; set; }
        string Fever { get; set; }
        string NoticeableWeightLoss { get; set; }
        string NightSweats { get; set; }
        string Lethargy { get; set; }
        string ICFActionTaken { get; set; }
        string TestResult { get; set; }
        string TBClinicalDiagnosis { get; set; }
        string ContactsInvited { get; set; }
        string EvaluatedForIPT { get; set; }
        string StartAntiTBs { get; set; }
        DateTime? TBRxStartDate { get; set; }
        string TBScreening { get; set; }
        string IPTClientWorkUp { get; set; }
        string StartIPT { get; set; }
        string IndicationForIPT { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
