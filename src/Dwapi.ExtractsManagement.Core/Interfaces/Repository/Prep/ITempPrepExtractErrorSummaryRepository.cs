using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep
{
    public interface ITempPrepExtractErrorSummaryRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByExtract(string extract);
    }
}
