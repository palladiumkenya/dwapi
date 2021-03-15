using System;

namespace Dwapi.Contracts.Ct
{
    public interface IStatus
    {
        string TOVerified { get; set; }
        DateTime? TOVerifiedDate { get; set; }
    }
}