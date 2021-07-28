using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch
{
    public interface ITempMnchExtractErrorSummaryRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByExtract(string extract);
    }
}
