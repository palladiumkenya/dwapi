using System;
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
        Task<IDataReader> ExecuteReader(DbProtocol protocol, DbExtract extract, DateTime? maxCreated, DateTime? maxModified, int siteCode);
        IDataReader ExecuteReaderSync(DbProtocol protocol, DbExtract extract);
        bool CheckDiffSupport(DbProtocol protocol);
        bool RefreshETLtables(DbProtocol protocol);
    }
}
