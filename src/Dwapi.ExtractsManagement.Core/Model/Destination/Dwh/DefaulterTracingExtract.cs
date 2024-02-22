using System;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{
    public class DefaulterTracingExtract : ClientExtract,IDefaulterTracing
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? EncounterId { get; set; }
        public string TracingType { get; set; }
        public string TracingOutcome { get; set; }
        public int? AttemptNumber { get; set; }
        public string IsFinalTrace { get; set; }
        public string TrueStatus { get; set; }
        public string CauseOfDeath { get; set; }
        public string Comments { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? DatePromisedToCome { get; set; }
        public string ReasonForMissedAppointment { get; set; }
        public DateTime? DateOfMissedAppointment { get; set; }
    }
}
