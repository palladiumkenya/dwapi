using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Dwapi.Domain.Utils;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader
{
    public class PatientLoader : IPatientLoader
    {
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly ITempPatientExtractRepository _tempPatientExtractRepository;

        public PatientLoader(IPatientExtractRepository patientExtractRepository, ITempPatientExtractRepository tempPatientExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _tempPatientExtractRepository = tempPatientExtractRepository;
        }

        public async Task<int> Load()
        {
            try
            {

                //load temp extracts without errors
                var tempPatientExtracts = _tempPatientExtractRepository.GetAll().Where(a=>a.CheckError == false).ToList();

                //Auto mapper
                var extractRecords = Mapper.Map<List<TempPatientExtract>, List<PatientExtract>>(tempPatientExtracts);

                //Batch Insert
                _patientExtractRepository.BatchInsert(extractRecords);
                Log.Debug("saved batch");


                return tempPatientExtracts.Count;

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientExtract)} not Loaded");
                return 0;
            }
        }
    }
}
