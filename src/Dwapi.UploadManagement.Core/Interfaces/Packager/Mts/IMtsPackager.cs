using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Mgs
{
    public interface IMtsPackager
    {
        IEnumerable<Manifest> Generate(EmrDto emrDto);
        IEnumerable<Manifest> GenerateWithMetrics(EmrDto emrDto);
        IEnumerable<IndicatorExtract> GenerateMigrations();
    }
}
