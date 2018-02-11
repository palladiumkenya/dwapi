using System;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Stage
{
    public interface IStage
    {
        Guid Id { get; set; }
        string Emr { get; set; }
        int? FacilityCode { get; set; }
        DateTime? DateExtracted { get; set; }
        DateTime DateStaged { get; set; }
    }
}