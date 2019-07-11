using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs
{
    public interface ICbsPackager
    {
        IEnumerable<Manifest> Generate();
        IEnumerable<Manifest> GenerateWithMetrics();
        IEnumerable<MasterPatientIndexDto> GenerateDtoMpi();
    }
}
