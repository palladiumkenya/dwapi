using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts
{
    public interface ITempHTSExtractErrorSummaryRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByExtract(string extract);
    }
}

