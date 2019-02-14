using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.Exchange;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs
{
    public interface ICbsPackager
    {
        IEnumerable<Manifest> Generate();
        IEnumerable<Manifest> GenerateWithMetrics();
        IEnumerable<MasterPatientIndex> GenerateMpi();
    }
}
