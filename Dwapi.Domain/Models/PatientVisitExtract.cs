using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Domain.Utils;

namespace Dwapi.Domain.Models
{
    //[Table("PatientVisitExtract")]
    public class PatientVisitExtract
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
       
        public int? VisitId { get; set; }
        public DateTime? VisitDate { get; set; }
        public string Service { get; set; }
        public string VisitType { get; set; }
        public int? WHOStage { get; set; }
        public string WABStage { get; set; }
        public string Pregnant { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDD { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string BP { get; set; }
        public string OI { get; set; }
        public DateTime? OIDate { get; set; }
        public DateTime? SubstitutionFirstlineRegimenDate { get; set; }
        public string SubstitutionFirstlineRegimenReason { get; set; }
        public DateTime? SubstitutionSecondlineRegimenDate { get; set; }
        public string SubstitutionSecondlineRegimenReason { get; set; }
        public DateTime? SecondlineRegimenChangeDate { get; set; }
        public string SecondlineRegimenChangeReason { get; set; }
        public string Adherence { get; set; }
        public string AdherenceCategory { get; set; }
        public string FamilyPlanningMethod { get; set; }
        public string PwP { get; set; }
        public decimal? GestationAge { get; set; }
        public DateTime? NextAppointmentDate { get; set; }

        public PatientVisitExtract()
        {
        }

        public PatientVisitExtract(int patientPk, string patientId, int siteCode, int? visitId, DateTime? visitDate, string service, string visitType, int? whoStage, string wabStage, string pregnant, DateTime? lmp, DateTime? edd, decimal? height, decimal? weight, string bp, string oi, DateTime? oiDate, DateTime? substitutionFirstlineRegimenDate, string substitutionFirstlineRegimenReason, DateTime? substitutionSecondlineRegimenDate, string substitutionSecondlineRegimenReason, DateTime? secondlineRegimenChangeDate, string secondlineRegimenChangeReason, string adherence, string adherenceCategory, string familyPlanningMethod, string pwP, decimal? gestationAge, DateTime? nextAppointmentDate, string emr, string project)
        {
            PatientPK = patientPk;
            PatientID = patientId;
            SiteCode = siteCode;
            VisitId = visitId;
            VisitDate = visitDate;
            Service = service;
            VisitType = visitType;
            WHOStage = whoStage;
            WABStage = wabStage;
            Pregnant = pregnant;
            LMP = lmp;
            EDD = edd;
            Height = height;
            Weight = weight;
            BP = bp;
            OI = oi;
            OIDate = oiDate;
            SubstitutionFirstlineRegimenDate = substitutionFirstlineRegimenDate;
            SubstitutionFirstlineRegimenReason = substitutionFirstlineRegimenReason;
            SubstitutionSecondlineRegimenDate = substitutionSecondlineRegimenDate;
            SubstitutionSecondlineRegimenReason = substitutionSecondlineRegimenReason;
            SecondlineRegimenChangeDate = secondlineRegimenChangeDate;
            SecondlineRegimenChangeReason = secondlineRegimenChangeReason;
            Adherence = adherence;
            AdherenceCategory = adherenceCategory;
            FamilyPlanningMethod = familyPlanningMethod;
            PwP = pwP;
            GestationAge = gestationAge;
            NextAppointmentDate = nextAppointmentDate;
            Emr = emr;
            Project = project;
        }

        public PatientVisitExtract(TempPatientVisitExtract extract)
        {
            PatientPK = extract.PatientPK.Value;
            PatientID = extract.PatientID;
            SiteCode = extract.SiteCode.Value;
            VisitId = extract.VisitId;
            VisitDate = extract.VisitDate;
            Service = extract.Service;
            VisitType = extract.VisitType;
            WHOStage = extract.WHOStage;
            WABStage = extract.WABStage;
            Pregnant = extract.Pregnant;
            LMP = extract.LMP;
            EDD = extract.EDD;
            Height = extract.Height;
            Weight = extract.Weight;
            BP = extract.BP;
            OI = extract.OI;
            OIDate = extract.OIDate;
            SubstitutionFirstlineRegimenDate = extract.SubstitutionFirstlineRegimenDate;
            SubstitutionFirstlineRegimenReason = extract.SubstitutionFirstlineRegimenReason;
            SubstitutionSecondlineRegimenDate = extract.SubstitutionSecondlineRegimenDate;
            SubstitutionSecondlineRegimenReason = extract.SubstitutionSecondlineRegimenReason;
            SecondlineRegimenChangeDate = extract.SecondlineRegimenChangeDate;
            SecondlineRegimenChangeReason = extract.SecondlineRegimenChangeReason;
            Adherence = extract.Adherence;
            AdherenceCategory = extract.AdherenceCategory;
            FamilyPlanningMethod = extract.FamilyPlanningMethod;
            PwP = extract.PwP;
            GestationAge = extract.GestationAge;
            NextAppointmentDate = extract.NextAppointmentDate;
            Emr = extract.Emr;
            Project = extract.Project;
        }
    }
}
