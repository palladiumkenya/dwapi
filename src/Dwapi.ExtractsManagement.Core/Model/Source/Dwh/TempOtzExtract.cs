using System;
using System.Text.RegularExpressions;
using Dwapi.Contracts.Ct;
using Humanizer;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    public class TempOtzExtract : TempExtract,IOtz
    {
       public string FacilityName { get; set; }
        public int ? VisitID { get; set; }
        public DateTime ? VisitDate { get; set; }
        public DateTime ? OTZEnrollmentDate { get; set; }
        public string TransferInStatus { get; set; }
        public string ModulesPreviouslyCovered { get; set; }
        public string ModulesCompletedToday { get; set; }
        public string SupportGroupInvolvement { get; set; }
        public string Remarks { get; set; }
        public string TransitionAttritionReason { get; set; }
        public DateTime ? OutcomeDate { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
    }
}
