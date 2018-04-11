using Dwapi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dwapi.Domain
{
    [Table("PatientPharmacyExtract")]
    public class ClientPatientPharmacyExtract
    {
        [Key]
        public Guid Id { get; set; }
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int SiteCode { get; set; }
        [Column(Order = 100)]
        public string Emr { get; set; }
        [Column(Order = 101)]
        public string Project { get; set; }
        [DoNotRead]
        [Column(Order = 102)]
        public bool? Processed { get; set; }
        [DoNotRead]
        public string QueueId { get; set; }
        [DoNotRead]
        public string Status { get; set; }
        [DoNotRead]
        public DateTime? StatusDate { get; set; }

        
        public int? VisitID { get; set; }
        public string Drug { get; set; }
        public string Provider { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public string TreatmentType { get; set; }
        public string RegimenLine { get; set; }
        public string PeriodTaken { get; set; }
        public string ProphylaxisType { get; set; }

        public ClientPatientPharmacyExtract()
        {
        }

        public ClientPatientPharmacyExtract(int patientPk, string patientId, int siteCode, int? visitId, string drug, string provider, DateTime? dispenseDate, decimal? duration, DateTime? expectedReturn, string treatmentType, string regimenLine, string periodTaken, string prophylaxisType, string emr, string project)
        {
            PatientPK = patientPk;
            PatientID = patientId;
            SiteCode = siteCode;
            VisitID = visitId;
            Drug = drug;
            Provider = provider;
            DispenseDate = dispenseDate;
            Duration = duration;
            ExpectedReturn = expectedReturn;
            TreatmentType = treatmentType;
            RegimenLine = regimenLine;
            PeriodTaken = periodTaken;
            ProphylaxisType = prophylaxisType;
            Emr = emr;
            Project = project;
        }

        public ClientPatientPharmacyExtract(TempPatientPharmacyExtract extract)
        {
            PatientPK = extract.PatientPK.Value;
            PatientID = extract.PatientID;
            SiteCode = extract.SiteCode.Value;
            VisitID = extract.VisitID;
            Drug = extract.Drug;
            Provider = extract.Provider;
            DispenseDate = extract.DispenseDate;
            Duration = extract.Duration;
            ExpectedReturn = extract.ExpectedReturn;
            TreatmentType = extract.TreatmentType;
            RegimenLine = extract.RegimenLine;
            PeriodTaken = extract.PeriodTaken;
            ProphylaxisType = extract.ProphylaxisType;
            Emr = extract.Emr;
            Project = extract.Project;

        }
    }
}
