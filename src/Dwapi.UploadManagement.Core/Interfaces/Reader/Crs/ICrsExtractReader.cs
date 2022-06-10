using System.Collections.Generic;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Model.Crs;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader.Crs
{
    public interface ICrsExtractReader
    {
        IEnumerable<ClientRegistryExtractView> ReadAll();
        IEnumerable<Site> GetSites();
        IEnumerable<SitePatientProfile> GetSitePatientProfiles();
    }
}
