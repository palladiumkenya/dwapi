using System.Data;
using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface ISourceReader
    {
        int Find(DbProtocol protocol, DbExtract extract);
        Task<IDataReader> ExecuteReader(DbProtocol protocol, DbExtract extract);
    }
}