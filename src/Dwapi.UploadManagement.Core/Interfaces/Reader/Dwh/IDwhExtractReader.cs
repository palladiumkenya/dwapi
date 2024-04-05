﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Diff;
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
        IEnumerable<T> Read<T, TId>(int page, int pageSize, Expression<Func<T, bool>> predicate) where T : Entity<TId>;

        IEnumerable<T> ReadSmart<T>(int page, int pageSize) where T : ClientExtract;
        IEnumerable<T> ReadSmart<T, TId>(int page, int pageSize) where T : Entity<TId>;
        IEnumerable<T> ReadSmart<T, TId>(int page, int pageSize, Expression<Func<T, bool>> predicate) where T : Entity<TId>;
        IEnumerable<T> ReadMainExtract<T, TId>(int page, int pageSize, Expression<Func<T, bool>> predicate) where T : Entity<TId>;
        IEnumerable<T> ReadMainExtract<T, TId>(int page, int pageSize) where T : Entity<TId>;

        IDataReader GetSmartReader(string extractName);
        
        long GetTotalRecords<T, TId>() where T : Entity<TId>;
    }
}
