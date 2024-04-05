using System;

namespace Dwapi.Contracts.Mnch
{
     public interface IMnchImmunization
    {
         string FacilityName { get; set; }
         string PatientMnchID { get; set; }
         DateTime? BCG { get; set; }
         DateTime? OPVatBirth { get; set; }
         DateTime? OPV1 { get; set; }
         DateTime? OPV2 { get; set; }
         DateTime? OPV3 { get; set; }
         DateTime? IPV { get; set; }
         DateTime? DPTHepBHIB1 { get; set; }
         DateTime? DPTHepBHIB2 { get; set; }
         DateTime? DPTHepBHIB3 { get; set; }
         DateTime? PCV101 { get; set; }
         DateTime? PCV102 { get; set; }
         DateTime? PCV103 { get; set; }
         DateTime? ROTA1 { get; set; }
         DateTime? MeaslesReubella1 { get; set; }
         DateTime? YellowFever { get; set; }
         DateTime? MeaslesReubella2 { get; set; }
         DateTime? MeaslesAt6Months { get; set; }
         DateTime? ROTA2 { get; set; }
         DateTime? DateOfNextVisit { get; set; }
         string BCGScarChecked { get; set; }
         DateTime? DateChecked { get; set; }
         DateTime? DateBCGrepeated  { get; set; }
         DateTime? VitaminAAt6Months { get; set; }
         DateTime? VitaminAAt1Yr  { get; set; }
         DateTime? VitaminAAt18Months { get; set; }
         DateTime? VitaminAAt2Years { get; set; }
         DateTime? VitaminAAt2To5Years { get; set; }
         string FullyImmunizedChild { get; set; }
         string RecordUUID { get; set; }
         bool? Voided { get; set; }
         DateTime? Date_Created { get; set; }
         DateTime? Date_Last_Modified { get; set; }
      
    }
}