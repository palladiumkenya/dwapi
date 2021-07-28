using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mnch
{
    public interface IClearMnchExtracts
    {
        Task<int> Clear(List<Guid> extractIds);
    }
}
