using System;

namespace Dwapi.Contracts.Ct
{
    public interface IArtFastTrack
    {
        string ARTRefillModel  { get; set; }
        DateTime?  VisitDate  { get; set; }
        string CTXDispensed  { get; set; }
        string DapsoneDispensed  { get; set; }
        string CondomsDistributed  { get; set; }
        string OralContraceptivesDispensed  { get; set; }
        string MissedDoses  { get; set; }
        string Fatigue  { get; set; }
        string Cough  { get; set; }
        string Fever  { get; set; }
        string Rash  { get; set; }
        string NauseaOrVomiting { get; set; }
        string GenitalSoreOrDischarge  { get; set; }
        string Diarrhea  { get; set; }
        string OtherSymptoms  { get; set; }
        string PregnancyStatus  { get; set; }
        string FPStatus  { get; set; }
        string FPMethod  { get; set; }
        string ReasonNotOnFP  { get; set; }
        string ReferredToClinic  { get; set; }
        DateTime?  ReturnVisitDate  { get; set; }
        
        string RecordUUID { get; set; }
        bool? Voided { get; set; }

        DateTime? Date_Created  { get; set; }
        DateTime? Date_Last_Modified  { get; set; }
    }
}