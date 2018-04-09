using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dwapi.Domain;
using NPoco;
using NPoco.Expressions;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public class IQCareDataExtractor 
    {
        private readonly IDatabase _database;
        public IQCareDataExtractor(IDatabase database)
        {
            _database = database ?? throw new ArgumentException(nameof(database));
        }

        public async Task Extract()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientPatientExtract>> ExtractPatientDetails()
        {
            return await _database.FetchAsync<ClientPatientExtract>(""); // insert sql statement
        }

        public async Task<IEnumerable<ClientPatientBaselinesExtract>> ExtractPatientBaseline()
        {
            return await _database.FetchAsync<ClientPatientBaselinesExtract>(""); 
        }
        
    }
}
