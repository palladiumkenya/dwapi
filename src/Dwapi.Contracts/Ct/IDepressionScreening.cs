using System;

namespace Dwapi.Contracts.Ct
{
    public
        interface IDepressionScreening
    {
        string FacilityName { get; set; }
        int? VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        string PHQ9_1 { get; set; }
        string PHQ9_2 { get; set; }
        string PHQ9_3 { get; set; }
        string PHQ9_4 { get; set; }
        string PHQ9_5 { get; set; }
        string PHQ9_6 { get; set; }
        string PHQ9_7 { get; set; }
        string PHQ9_8 { get; set; }
        string PHQ9_9 { get; set; }
        string PHQ_9_rating { get; set; }
        int? DepressionAssesmentScore { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
        string RecordUUID { get; set; }

    }
}
