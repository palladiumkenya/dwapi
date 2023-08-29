using System;

namespace Dwapi.Contracts.Ct
{
    public interface IPatient
    {
        string Pkv { get; set; }
        string Occupation { get; set; }
        string NUPI { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
        string RecordUUID { get; set; }

    }
}
