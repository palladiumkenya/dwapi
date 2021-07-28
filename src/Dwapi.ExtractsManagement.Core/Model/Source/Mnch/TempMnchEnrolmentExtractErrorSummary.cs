using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mnch
{
    [Table("vTempMnchEnrolmentExtractErrorSummary")]public class TempMnchEnrolmentExtractErrorSummary:TempMnchExtractErrorSummary,IMnchEnrolment
    {
        public string PatientMnchID { get; set; }
        public string ServiceType { get; set; }
        public DateTime? EnrollmentDateAtMnch { get; set; }
        public DateTime? MnchNumber { get; set; }
        public DateTime? FirstVisitAnc { get; set; }
        public string Parity { get; set; }
        public int Gravidae { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDDFromLMP { get; set; }
        public string HIVStatusBeforeANC { get; set; }
        public DateTime? HIVTestDate { get; set; }
        public string PartnerHIVStatus { get; set; }
        public DateTime? PartnerHIVTestDate { get; set; }
        public string BloodGroup { get; set; }
        public string StatusAtMnch { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
