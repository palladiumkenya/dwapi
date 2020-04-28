using System.Collections.Generic;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Model.Mgs;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader.Mgs
{
    public interface IMgsExtractReader
    {
        IEnumerable<MetricMigrationExtractView> ReadAllMigrations();
        IEnumerable<Site> GetSites();
        IEnumerable<SiteMetricProfile> GetSitePatientProfiles();
    }
}
