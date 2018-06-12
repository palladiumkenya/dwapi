using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Packager.Cbs
{
    public interface ICbsPackager
    {
        IEnumerable<Manifest> Generate();
        IEnumerable<MasterPatientIndex> GenerateMpi();
    }
}