using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Mgs
{
    public interface IMgsPackager
    {
        IEnumerable<Manifest> Generate(EmrSetup emrSetup);
        IEnumerable<Manifest> GenerateWithMetrics(EmrSetup emrSetup);
        IEnumerable<MetricMigrationExtract> GenerateMigrations();
    }
}
