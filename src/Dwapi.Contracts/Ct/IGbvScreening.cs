using System;

namespace Dwapi.Contracts.Ct
{
    public interface IGbvScreening
    {
        string FacilityName { get; set; }
        int? VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        string IPV { get; set; }
        string PhysicalIPV { get; set; }
        string EmotionalIPV { get; set; }
        string SexualIPV { get; set; }
        string IPVRelationship { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
        string RecordUUID { get; set; }
         bool? Voided { get; set; }

    }
}
