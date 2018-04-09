using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Domain;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public class DataExtractor : IDataExtractor
    {
        public Task Extract()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClientPatientBaselinesExtract>> ExtractPatientBaseline()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClientPatientExtract>> ExtractPatientDetails()
        {
            throw new NotImplementedException();
        }
    }
}
