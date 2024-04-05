using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mnch
{
    [Table("vTempCwcVisitExtractError")]public class TempCwcVisitExtractError:TempExtract,ICwcVisit
    {
        public string FacilityName { get; set; }
        public string PatientMnchID { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? VisitID { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Temp { get; set; }
        public int? PulseRate { get; set; }
        public int? RespiratoryRate { get; set; }
        public decimal? OxygenSaturation { get; set; }
        public int? MUAC { get; set; }
        public string WeightCategory { get; set; }
        public string Stunted { get; set; }
        public string InfantFeeding { get; set; }
        public string MedicationGiven { get; set; }
        public string TBAssessment { get; set; }
        public string MNPsSupplementation { get; set; }
        public string Immunization { get; set; }
        public string DangerSigns { get; set; }
        public string Milestones { get; set; }
        public string VitaminA { get; set; }
        public string Disability { get; set; }
        public string ReceivedMosquitoNet { get; set; }
        public string Dewormed { get; set; }
        public string ReferredFrom { get; set; }
        public string ReferredTo { get; set; }
        public string ReferralReasons { get; set; }
        public string FollowUP { get; set; }
        public DateTime? NextAppointment { get; set; }
        public string RevisitThisYear { get; set; }
        public string Refferred { get; set; }
        public decimal? HeightLength { get; set; }
        public string ZScore { get; set; }
        public int? ZScoreAbsolute { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
