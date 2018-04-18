using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Domain;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Model;
using NPoco;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    public class PatientArtExtractor : IExtractor
    {
        private readonly IExtractUnitOfWork _unitOfWork;
        private Func<IDatabase> _databaseFactory;
        private readonly ProgressHub _progressHub;

        public PatientArtExtractor(IExtractUnitOfWork unitOfWork, ProgressHub progressHub)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _progressHub = progressHub ?? throw new ArgumentNullException(nameof(progressHub));
        }

        public async Task ExtractAsync(DwhExtract extract, DbProtocol dbProtocol)
        {
            try
            {
                _databaseFactory = ExtractorHelper.NPocoDataFactory(dbProtocol);
                IList<TempPatientArtExtract> tempPatientArtExtracts;

                await _progressHub.Send(new LoadProgress { Extract = extract.ExtractName, Status = LoadStatus.Fetching });
                
                using (var database = _databaseFactory())
                    tempPatientArtExtracts = await database.FetchAsync<TempPatientArtExtract>(extract.SqlQuery);

                await _unitOfWork.Repository<TempPatientArtExtract>().AddRangeAsync(tempPatientArtExtracts);
                await _unitOfWork.SaveAsync();
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
