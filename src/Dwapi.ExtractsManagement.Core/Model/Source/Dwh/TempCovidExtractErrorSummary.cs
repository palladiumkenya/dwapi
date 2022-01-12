using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempCovidExtractErrorSummary")]
    public class TempCovidExtractErrorSummary : TempExtractErrorSummary,ICovid
    {
        public int? VisitID { get; set; }
        public DateTime? Covid19AssessmentDate { get; set; }
        public string ReceivedCOVID19Vaccine { get; set; }
        public DateTime? DateGivenFirstDose { get; set; }
        public string FirstDoseVaccineAdministered { get; set; }
        public DateTime? DateGivenSecondDose { get; set; }
        public string SecondDoseVaccineAdministered { get; set; }
        public string VaccinationStatus { get; set; }
        public string VaccineVerification { get; set; }
        public string BoosterGiven { get; set; }
        public string BoosterDose { get; set; }
        public DateTime? BoosterDoseDate { get; set; }
        public string EverCOVID19Positive { get; set; }
        public DateTime? COVID19TestDate { get; set; }
        public string PatientStatus { get; set; }
        public string AdmissionStatus { get; set; }
        public string AdmissionUnit { get; set; }
        public string MissedAppointmentDueToCOVID19 { get; set; }
        public string COVID19PositiveSinceLasVisit { get; set; }
        public DateTime? COVID19TestDateSinceLastVisit { get; set; }
        public string PatientStatusSinceLastVisit { get; set; }
        public string AdmissionStatusSinceLastVisit { get; set; }
        public DateTime? AdmissionStartDate { get; set; }
        public DateTime? AdmissionEndDate { get; set; }
        public string AdmissionUnitSinceLastVisit { get; set; }
        public string SupplementalOxygenReceived { get; set; }
        public string PatientVentilated { get; set; }
        public string TracingFinalOutcome { get; set; }
        public string CauseOfDeath { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string COVID19TestResult { get; set; }
        public string Sequence { get; set; }
    }
}
