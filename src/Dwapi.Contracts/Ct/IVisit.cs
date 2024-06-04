using System;

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


        string GeneralExamination {get;set;}
        string SystemExamination {get;set;}
        string Skin {get;set;}
        string Eyes {get;set;}
        string ENT {get;set;}
        string Chest {get;set;}
        string CVS {get;set;}
        string Abdomen {get;set;}
        string CNS {get;set;}
        string Genitourinary {get;set;}
        DateTime? RefillDate { get; set; }
        string PaedsDisclosure { get; set; }
        string ZScore { get; set; }
        int? ZScoreAbsolute { get; set; }
        string RecordUUID { get; set; }
        bool? Voided { get; set; }
        string WHOStagingOI  { get; set; }
        string WantsToGetPregnant  { get; set; }
        string AppointmentReminderWillingness  { get; set; }


    }
}
