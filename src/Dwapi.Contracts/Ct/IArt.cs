using System;

namespace Dwapi.Contracts.Ct
{
    public interface IArt
    {
         string PreviousARTUse { get; set; }
         string  PreviousARTPurpose { get; set; }
         string PatientUUID { get; set; }

         DateTime?  DateLastUsed { get; set; }
    }
}
