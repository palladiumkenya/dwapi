using System.Collections.Generic;
using PagedList;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface ITempExtractErrorSummaryRepository<T>
    {
        IEnumerable<T> GetAll();
        IPagedList<T> GetAll(int? page, int? pageSize, string search = "");
        IEnumerable<T> GetByExtract(string extract);
    }
}

