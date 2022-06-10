using System.Collections.Generic;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Model.Crs;
using Dwapi.UploadManagement.Core.Model.Crs.Dtos;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Crs
{
    public interface ICrsPackager
    {
        IEnumerable<Manifest> Generate(EmrDto emrDto);
        IEnumerable<Manifest> GenerateWithMetrics(EmrDto emrDto);
        IEnumerable<ClientRegistryExtractView> GenerateCrs();
    }
}
