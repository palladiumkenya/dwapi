namespace Dwapi.Contracts.Ct
{
    public interface IVisit
    {
        string VisitBy { get; set; }
        decimal? Temp { get; set; }
        int? PulseRate { get; set; }
        int? RespiratoryRate { get; set; }
        decimal? OxygenSaturation { get; set; }
        int? Muac { get; set; }
        string NutritionalStatus { get; set; }
        string EverHadMenses { get; set; }
        string Breastfeeding { get; set; }
        string Menopausal { get; set; }
        string NoFPReason { get; set; }
        string ProphylaxisUsed { get; set; }
        string CTXAdherence { get; set; }
        string CurrentRegimen { get; set; }
        string HCWConcern { get; set; }
        string TCAReason { get; set; }
        string ClinicalNotes { get; set; }
    }
}