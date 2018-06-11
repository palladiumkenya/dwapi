using System.Collections.Generic;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Packager
{
    public interface ICbsPackager
    {
        IEnumerable<Manifest> Generate();
    }
}