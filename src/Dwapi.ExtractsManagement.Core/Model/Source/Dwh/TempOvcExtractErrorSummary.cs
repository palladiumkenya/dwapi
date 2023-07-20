using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempOvcExtractErrorSummary")]
    public class TempOvcExtractErrorSummary : TempExtractErrorSummary,IOvc
    {
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? OVCEnrollmentDate { get; set; }
        public string RelationshipToClient { get; set; }
        public string EnrolledinCPIMS { get; set; }
        public string CPIMSUniqueIdentifier { get; set; }
        public string PartnerOfferingOVCServices { get; set; }
        public string OVCExitReason { get; set; }
        public DateTime? ExitDate { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }
    }
}
