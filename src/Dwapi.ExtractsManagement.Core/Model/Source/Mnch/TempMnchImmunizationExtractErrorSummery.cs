using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Mnch
{
    [Table("vTempMnchImmunizationExtractErrorSummary")]public class TempMnchImmunizationExtractErrorSummary:TempMnchExtractErrorSummary,IMnchImmunization
    {
        public string PatientMnchID { get; set; }
        public DateTime? BCG { get; set; }
        public DateTime? OPVatBirth { get; set; }
        public DateTime? OPV1 { get; set; }
        public DateTime? OPV2 { get; set; }
        public DateTime? OPV3 { get; set; }
        public DateTime? IPV { get; set; }
        public DateTime? DPTHepBHIB1 { get; set; }
        public DateTime? DPTHepBHIB2 { get; set; }
        public DateTime? DPTHepBHIB3 { get; set; }
        public DateTime? PCV101 { get; set; }
        public DateTime? PCV102 { get; set; }
        public DateTime? PCV103 { get; set; }
        public DateTime? ROTA1 { get; set; }
        public DateTime? MeaslesReubella1 { get; set; }
        public DateTime? YellowFever { get; set; }
        public DateTime? MeaslesReubella2 { get; set; }
        public DateTime? MeaslesAt6Months { get; set; }
        public DateTime? ROTA2 { get; set; }
        public DateTime? DateOfNextVisit { get; set; }
        public string BCGScarChecked { get; set; }
        public DateTime? DateChecked { get; set; }
        public DateTime? DateBCGrepeated { get; set; }
        public DateTime? VitaminAAt6Months { get; set; }
        public DateTime? VitaminAAt1Yr { get; set; }
        public DateTime? VitaminAAt18Months { get; set; }
        public DateTime? VitaminAAt2Years { get; set; }
        public DateTime? VitaminAAt2To5Years { get; set; }
        public string FullyImmunizedChild { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}