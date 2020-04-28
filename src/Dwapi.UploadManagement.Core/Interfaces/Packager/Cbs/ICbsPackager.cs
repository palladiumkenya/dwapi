using System.Collections.Generic;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs
{
    public interface ICbsPackager
    {
        IEnumerable<Manifest> Generate(EmrSetup emrSetup);
        IEnumerable<Manifest> GenerateWithMetrics(EmrSetup emrSetup);
        IEnumerable<MasterPatientIndexDto> GenerateDtoMpi();
    }
}
