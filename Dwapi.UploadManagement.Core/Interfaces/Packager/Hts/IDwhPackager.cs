using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Hts
{
    public interface IHtsPackager
    {
        IEnumerable<DwhManifest> Generate();
        IEnumerable<DwhManifest> GenerateWithMetrics();
        PatientExtractView GenerateExtracts(Guid id);
    }
}
