using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.SharedKernel.Model;
using NPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    public interface IExtractor
    {
        Task Extract(DwhExtract extrac, DbProtocol dbProtocol);
    }
}
