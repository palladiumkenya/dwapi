using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts
{
    [Obsolete]
    public interface ICleanHtsExtracts
    {
        Task Clean(List<Guid> extractIds);
    }
}
