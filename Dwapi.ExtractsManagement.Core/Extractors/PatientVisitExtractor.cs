using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Domain;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.SharedKernel.Model;
using NPoco;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    public class PatientVisitExtractor : IExtractor
    {
        private readonly IExtractUnitOfWork _unitOfWork;
        private Func<IDatabase> _databaseFactory;

        public PatientVisitExtractor(IExtractUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExtractAsync(DwhExtract extract, DbProtocol dbProtocol)
        {
            try
            {
                _databaseFactory = ExtractorHelper.NPocoDataFactory(dbProtocol);
                IList<TempPatientVisitExtract> tempPatientVisitExtracts;

                var fetchingStart = DateTime.Now;

                using (var database = _databaseFactory())
                    tempPatientVisitExtracts = await database.FetchAsync<TempPatientVisitExtract>(extract.SqlQuery);

                var fetchingStop = DateTime.Now;
                Debug.WriteLine($"FETCHING : start => {fetchingStart} end => {fetchingStop}");

                var loadingStart = DateTime.Now;

                await _unitOfWork.Repository<TempPatientVisitExtract>().AddRangeAsync(tempPatientVisitExtracts);
                await _unitOfWork.SaveAsync();

                var loadingStop = DateTime.Now;
                Debug.WriteLine($"LOADING : start => {loadingStart} end => {loadingStop}");
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
