using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Services;
using MediatR;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.SharedKernel.Enum;
using Dwapi.Domain;
using Hangfire;
using System.Linq;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class LoadFromEmrCommandHandler : IRequestHandler<LoadFromEmrCommand, LoadFromEmrResponse>
    {
        private readonly IExtractUnitOfWork _unitOfWork;
        private readonly IBackgroundJobInit _backgroundJob;
        private Func<IDatabase> _databaseFactory;

        public LoadFromEmrCommandHandler(IExtractUnitOfWork unitOfWork, IBackgroundJobInit backgroundJobInit)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _backgroundJob = backgroundJobInit ?? throw new ArgumentNullException(nameof(backgroundJobInit));
        }

        public async Task<LoadFromEmrResponse> Handle(LoadFromEmrCommand request, CancellationToken cancellationToken)
        {
            switch (request.DatabaseProtocol.DatabaseType)
            {
                case SharedKernel.Enum.DatabaseType.MicrosoftSQL:
                    _databaseFactory = () 
                        => new Database(request.DatabaseProtocol.GetConnectionString(), NPoco.DatabaseType.SqlServer2012, SqlClientFactory.Instance);
                    break;

                case SharedKernel.Enum.DatabaseType.MySQL:
                    _databaseFactory = () 
                        => new Database(request.DatabaseProtocol.GetConnectionString(), NPoco.DatabaseType.MySQL, SqlClientFactory.Instance);
                    break;

                default:
                    throw new InvalidOperationException();
            }

            var extracts = request.Extracts.ToHashSet().OrderBy(e => e.Rank).ToHashSet();
            _backgroundJob.EnqueueJob(
                () => ExtractPatientExtracts(extracts.Where(x => x.ExtractType == ExtractType.Patient)
                .First())
                .Wait());
            

            return new LoadFromEmrResponse();
        }

        public async Task ExtractPatientExtracts(DwhExtract extract)
        {
            try
            {
                IList<ClientPatientExtract> clientPatientExtracts;
                using (var database = _databaseFactory())
                    clientPatientExtracts = await database.FetchAsync<ClientPatientExtract>(extract.SqlQuery);
                
                await _unitOfWork.Repository<ClientPatientExtract>().AddRangeAsync(clientPatientExtracts);
                await _unitOfWork.SaveAsync();
                
            }

            catch(Exception ex)
            {
                
            }
        }

        public async Task ExtractPatientLabExtract(DwhExtract extract)
        {
            try
            {
                IList<ClientPatientLaboratoryExtract> patientLabExtracts;
                using(var database = _databaseFactory())
                    patientLabExtracts = await database.FetchAsync<ClientPatientLaboratoryExtract>(extract.SqlQuery);
                await _unitOfWork.Repository<ClientPatientLaboratoryExtract>().AddRangeAsync(patientLabExtracts);
                await _unitOfWork.SaveAsync();
            }

            catch(Exception ex)
            {

            }
        }
        
        public async Task ExtractPatientBaselineExtract(DwhExtract extract)
        {
            try
            {
                IList<ClientPatientBaselinesExtract> patientBaselinesExtracts;
                using (var database = _databaseFactory())
                    patientBaselinesExtracts = await database.FetchAsync<ClientPatientBaselinesExtract>(extract.SqlQuery);
                await _unitOfWork.Repository<ClientPatientBaselinesExtract>().AddRangeAsync(patientBaselinesExtracts);
                await _unitOfWork.SaveAsync();
            }

            catch (Exception ex)
            {

            }
        }

        public async Task ExtractPatientStatusExtract(DwhExtract extract)
        {
            try
            {
                IList<ClientPatientStatusExtract> patientStatusExtracts;
                using (var database = _databaseFactory())
                    patientStatusExtracts = await database.FetchAsync<ClientPatientStatusExtract>(extract.SqlQuery);
                await _unitOfWork.Repository<ClientPatientStatusExtract>().AddRangeAsync(patientStatusExtracts);
                await _unitOfWork.SaveAsync();
            }

            catch (Exception ex)
            {

            }
        }

        public async Task ExtractPatientVisitExtract(DwhExtract extract)
        {
            try
            {
                IList<ClientPatientVisitExtract> patientVisitExtracts;
                using (var database = _databaseFactory())
                    patientVisitExtracts = await database.FetchAsync<ClientPatientVisitExtract>(extract.SqlQuery);
                await _unitOfWork.Repository<ClientPatientVisitExtract>().AddRangeAsync(patientVisitExtracts);
                await _unitOfWork.SaveAsync();
            }

            catch (Exception ex)
            {

            }
        }
    }
}
