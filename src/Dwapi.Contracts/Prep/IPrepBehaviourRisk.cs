using System;

namespace Dwapi.Contracts.Prep
{
    public interface IPrepBehaviourRisk
    {
        string FacilityName { get; set; }
        string PrepNumber { get; set; }
        string HtsNumber { get; set; }
        DateTime? VisitDate { get; set; }
        int? VisitID { get; set; }
        string SexPartnerHIVStatus { get; set; }
        string IsHIVPositivePartnerCurrentonART { get; set; }
        string IsPartnerHighrisk { get; set; }
        string PartnerARTRisk { get; set; }
        string ClientAssessments { get; set; }
        string ClientRisk { get; set; }
        string ClientWillingToTakePrep { get; set; }
        string PrEPDeclineReason { get; set; }
        string RiskReductionEducationOffered { get; set; }
        string ReferralToOtherPrevServices { get; set; }
        DateTime? FirstEstablishPartnerStatus { get; set; }
        DateTime? PartnerEnrolledtoCCC { get; set; }
        string HIVPartnerCCCnumber { get; set; }
        DateTime? HIVPartnerARTStartDate { get; set; }
        string MonthsknownHIVSerodiscordant { get; set; }
        string SexWithoutCondom { get; set; }
        string NumberofchildrenWithPartner { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
