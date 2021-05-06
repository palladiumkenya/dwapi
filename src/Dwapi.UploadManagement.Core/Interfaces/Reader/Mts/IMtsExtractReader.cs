using System.Collections.Generic;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Model.Mgs;
using Dwapi.UploadManagement.Core.Model.Mts;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader.Mts
{
    public interface IMtsExtractReader
    {
        IEnumerable<IndicatorExtractView> ReadAll();
        IEnumerable<Site> GetSites();
        IEnumerable<SiteMetricProfile> GetSitePatientProfiles();
    }
}
