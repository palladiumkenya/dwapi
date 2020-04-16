using System.Collections.Generic;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Model.Cbs;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs
{
    public interface ICbsExtractReader
    {
        IEnumerable<MasterPatientIndexView> ReadAll();
        IEnumerable<Site> GetSites();
        IEnumerable<SitePatientProfile> GetSitePatientProfiles();
    }
}
