using System;

namespace Dwapi.Contracts.Mnch
{
    public interface IHei
    {
        string FacilityName { get; set; }
        string PatientMnchID { get; set; }
        DateTime? DNAPCR1Date { get; set; }
        DateTime? DNAPCR2Date { get; set; }
        DateTime? DNAPCR3Date { get; set; }
        DateTime? ConfirmatoryPCRDate { get; set; }
        DateTime? BasellineVLDate { get; set; }
        DateTime? FinalyAntibodyDate { get; set; }
        DateTime? DNAPCR1 { get; set; }
        DateTime? DNAPCR2 { get; set; }
        DateTime? DNAPCR3 { get; set; }
        DateTime? ConfirmatoryPCR { get; set; }
        DateTime? BasellineVL { get; set; }
        DateTime? FinalyAntibody { get; set; }
        DateTime? HEIExitDate { get; set; }
        string HEIHIVStatus { get; set; }
        string HEIExitCritearia { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
