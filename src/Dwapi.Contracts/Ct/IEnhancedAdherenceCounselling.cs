using System;

namespace Dwapi.Contracts.Ct
{
    public interface IEnhancedAdherenceCounselling
    {
        string FacilityName { get; set; }
        int? VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        int? SessionNumber { get; set; }
        DateTime? DateOfFirstSession { get; set; }
        int? PillCountAdherence { get; set; }
        string MMAS4_1 { get; set; }
        string MMAS4_2 { get; set; }
        string MMAS4_3 { get; set; }
        string MMAS4_4 { get; set; }
        string MMSA8_1 { get; set; }
        string MMSA8_2 { get; set; }
        string MMSA8_3 { get; set; }
        string MMSA8_4 { get; set; }
        string MMSAScore { get; set; }
        string EACRecievedVL { get; set; }
        string EACVL { get; set; }
        string EACVLConcerns { get; set; }
        string EACVLThoughts { get; set; }
        string EACWayForward { get; set; }
        string EACCognitiveBarrier { get; set; }
        string EACBehaviouralBarrier_1 { get; set; }
        string EACBehaviouralBarrier_2 { get; set; }
        string EACBehaviouralBarrier_3 { get; set; }
        string EACBehaviouralBarrier_4 { get; set; }
        string EACBehaviouralBarrier_5 { get; set; }
        string EACEmotionalBarriers_1 { get; set; }
        string EACEmotionalBarriers_2 { get; set; }
        string EACEconBarrier_1 { get; set; }
        string EACEconBarrier_2 { get; set; }
        string EACEconBarrier_3 { get; set; }
        string EACEconBarrier_4 { get; set; }
        string EACEconBarrier_5 { get; set; }
        string EACEconBarrier_6 { get; set; }
        string EACEconBarrier_7 { get; set; }
        string EACEconBarrier_8 { get; set; }
        string EACReviewImprovement { get; set; }
        string EACReviewMissedDoses { get; set; }
        string EACReviewStrategy { get; set; }
        string EACReferral { get; set; }
        string EACReferralApp { get; set; }
        string EACReferralExperience { get; set; }
        string EACHomevisit { get; set; }
        string EACAdherencePlan { get; set; }
        DateTime? EACFollowupDate { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
