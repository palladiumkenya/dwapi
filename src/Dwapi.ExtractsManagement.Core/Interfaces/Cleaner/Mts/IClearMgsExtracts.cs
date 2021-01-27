using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mts
{
    public interface IClearMtsExtracts
    {
        Task Clear(List<Guid> extractIds);
    }
}
