using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Domain;
using Dwapi.Domain.Models;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.SharedKernel.Model;
using NPoco;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    public class PatientBaselineExtractor : IExtractor
    {
        private readonly IExtractUnitOfWork _unitOfWork;
        private Func<IDatabase> _databaseFactory;

        public PatientBaselineExtractor(IExtractUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExtractAsync(DwhExtract extract, DbProtocol dbProtocol)
        {
            try
            {
                _databaseFactory = ExtractorHelper.NPocoEmrDataFactory(dbProtocol);

                IList<TempPatientBaselinesExtract> tempPatientBaselinesExtracts;
                using (var database = _databaseFactory())
                    tempPatientBaselinesExtracts = await database.FetchAsync<TempPatientBaselinesExtract>(extract.SqlQuery);
                await _unitOfWork.Repository<TempPatientBaselinesExtract>().AddRangeAsync(tempPatientBaselinesExtracts);
                await _unitOfWork.SaveAsync();
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
