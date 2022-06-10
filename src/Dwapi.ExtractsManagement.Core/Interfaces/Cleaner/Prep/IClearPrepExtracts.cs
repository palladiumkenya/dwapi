using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Prep
{
    public interface IClearPrepExtracts
    {
        Task<int> Clear(List<Guid> extractIds);
    }
}
