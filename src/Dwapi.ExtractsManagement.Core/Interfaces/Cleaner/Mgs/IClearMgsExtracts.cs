using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mgs
{
    public interface IClearMgsExtracts
    {
        Task Clear(List<Guid> extractIds);
    }
}
