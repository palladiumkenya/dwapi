using System;

namespace Dwapi.Contracts.Ct
{
    public interface IPharmacy
    {
        string RegimenChangedSwitched { get; set; }
        string RegimenChangeSwitchReason { get; set; }
        string StopRegimenReason { get; set; }
        DateTime? StopRegimenDate { get; set; }
        string RecordUUID { get; set; }

    }
}