using Dwapi.SharedKernel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model
{
    public class DwhExtractCommand
    {
        public DbProtocol DbProtocol { get; set; }
        public DbExtract DbExtract { get; set; }
    }

    
}
