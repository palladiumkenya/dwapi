using Dwapi.Domain;
using Dwapi.ExtractsManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public interface IDataExtractor
    {
        Task Extract(DwhExtractCommand extractCommand);
        Task<IEnumerable<ClientPatientExtract>> ExtractPatientDetails();
        Task<IEnumerable<ClientPatientBaselinesExtract>> ExtractPatientBaseline();
    }
}
