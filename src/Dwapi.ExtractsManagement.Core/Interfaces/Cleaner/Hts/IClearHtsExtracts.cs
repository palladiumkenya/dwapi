using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts
{
    public interface IClearHtsExtracts
    {
        Task Clear(List<Guid> extractIds);
    }
}
