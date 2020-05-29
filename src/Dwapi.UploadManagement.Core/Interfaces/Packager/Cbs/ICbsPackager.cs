using System.Collections.Generic;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs
{
    public interface ICbsPackager
    {
        IEnumerable<Manifest> Generate(EmrDto emrDto);
        IEnumerable<Manifest> GenerateWithMetrics(EmrDto emrDto);
        IEnumerable<MasterPatientIndexDto> GenerateDtoMpi();
    }
}
