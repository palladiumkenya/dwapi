using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface ITempExtractErrorSummaryRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByExtract(string extract);
    }
}

