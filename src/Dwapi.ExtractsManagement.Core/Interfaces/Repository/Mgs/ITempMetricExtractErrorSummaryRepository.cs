using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs
{
    public interface ITempMetricExtractErrorSummaryRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByExtract(string extract);
    }
}

