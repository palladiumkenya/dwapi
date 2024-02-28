using System;

namespace Dwapi.Contracts.Ct
{
    public interface IRelationship
    {
        string FacilityName { get; set; }
        
        string RelationshipToPatient { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }

        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
        string RecordUUID { get; set; }
        bool? Voided { get; set; }

    }
}
