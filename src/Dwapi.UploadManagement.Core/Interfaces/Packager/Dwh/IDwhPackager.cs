using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh
{
    public interface IDwhPackager
    {
        IEnumerable<DwhManifest> Generate(EmrSetup emrSetup);
        IEnumerable<DwhManifest> GenerateWithMetrics(EmrSetup emrSetup);
        PatientExtractView GenerateExtracts(Guid id);
    }
}
