using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface ISourceReader
    {
        IDbConnection Connection { get; }
        int Find(DbProtocol protocol, DbExtract extract);
        Task<IDataReader> ExecuteReader(DbProtocol protocol, DbExtract extract);
        Task<IDataReader> ExecuteReader(DbProtocol protohcol, DbExtract extract, DateTime? maxCreated, DateTime? maxModified, int siteCode);
        IDataReader ExecuteReaderSync(DbProtocol protocol, DbExtract extract);
        bool CheckDiffSupport(DbProtocol protocol);
        string RefreshEtlTtables(DbProtocol protocol);
        DateTime? GetEtlTtablesRefreshedDate(DbProtocol protocol);
        void ChangeSQLmode(DbProtocol protocol);

    }
}
