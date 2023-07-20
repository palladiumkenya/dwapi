using System;

namespace Dwapi.Contracts.Ct
{
    public interface ILab
    {
        DateTime? DateSampleTaken { get; set; }
        string SampleType { get; set; }
        string PatientUUID { get; set; }

    }
}