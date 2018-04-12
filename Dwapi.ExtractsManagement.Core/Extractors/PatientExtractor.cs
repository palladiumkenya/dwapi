using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Domain;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.SharedKernel.Model;
using NPoco;

namespace Dwapi.ExtractsManagement.Core.Extractors
{

    public interface IPatientExtractor
    {
        Task ExtractInChain(IOrderedEnumerable<DwhExtract> extracts);
    }

    public class PatientExtractor : IExtractor
    {
        private readonly IExtractUnitOfWork _unitOfWork;
        private readonly IBackgroundJobInit _backgroundJob;
        private Func<IDatabase> _databaseFactory;

        public PatientExtractor(IExtractUnitOfWork unitOfWork, IBackgroundJobInit backgroundJob)
        {
            _unitOfWork = unitOfWork;
            _backgroundJob = backgroundJob;
        }

        public async Task Extract(DwhExtract extract, DbProtocol dbProtocol)
        {
            try
            {
                _databaseFactory = ExtractorHelper.NPocoDataFactory(dbProtocol);

                IList<TempPatientExtract> tempPatientExtracts;
                using (var database = _databaseFactory())
                    tempPatientExtracts = await database.FetchAsync<TempPatientExtract>(extract.SqlQuery);

                await _unitOfWork.Repository<TempPatientExtract>().AddRangeAsync(tempPatientExtracts);
                await _unitOfWork.SaveAsync();
                
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
