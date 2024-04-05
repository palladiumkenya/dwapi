using System;
using Dwapi.Contracts.Prep;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Prep
{
    public class PrepMonthlyRefillExtract : PrepClientExtract,IPrepMonthlyRefill
    {
        public string FacilityName { get; set; }
        public string PrepNumber { get; set; }
        public string VisitDate { get; set; }
        public string BehaviorRiskAssessment { get; set; }
        public string SexPartnerHIVStatus { get; set; }
        public string SymptomsAcuteHIV { get; set; }
        public string AdherenceCounsellingDone { get; set; }
        public string ContraIndicationForPrEP { get; set; }
        public string PrescribedPrepToday { get; set; }
        public string RegimenPrescribed { get; set; }
        public string NumberOfMonths { get; set; }
        public string CondomsIssued { get; set; }
        public int NumberOfCondomsIssued { get; set; }
        public string ClientGivenNextAppointment { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string ReasonForFailureToGiveAppointment { get; set; }
        public DateTime? DateOfLastPrepDose { get; set; }
        
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        
    }
}
