using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh
{
    public interface IDwhPackager
    {
        IEnumerable<DwhManifest> Generate(EmrDto emrDto);
        IEnumerable<DwhManifest> GenerateWithMetrics(EmrDto emrDto);
        PatientExtractView GenerateExtracts(Guid id);
    }
}
