using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh
{
    public interface IDwhExtractReader
    {
        IEnumerable<SitePatientProfile> ReadProfiles();
        IEnumerable<Guid> ReadAllIds();
        PatientExtractView Read(Guid id);

        IEnumerable<Site> GetSites();
        IEnumerable<SitePatientProfile> GetSitePatientProfiles();

        IEnumerable<T> Read<T, TId>(int page, int pageSize) where T : Entity<TId>;
        IEnumerable<T> ReadDiff<T, TId>(int page, int pageSize) where T : Entity<TId>;
        long GetTotalRecords<T, TId>() where T : Entity<TId>;
    }
}
