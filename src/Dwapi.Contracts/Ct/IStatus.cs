using System;

namespace Dwapi.Contracts.Ct
{
    public interface IStatus
    {
        string TOVerified { get; set; }
        DateTime? TOVerifiedDate { get; set; }
        DateTime? ReEnrollmentDate { get; set; }


        string ReasonForDeath { get; set; }
        string SpecificDeathReason { get; set; }
        DateTime? DeathDate { get; set; }
        DateTime? EffectiveDiscontinuationDate { get; set; }
    }
}
