using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public PatientLoader(IPatientExtractRepository patientExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
        }

        public async Task<bool> Load()
        {
            try
            {
                int batch = 500;
                var list = new List<PatientExtract>();
                int count = 0;
                string connectionString = _patientExtractRepository.GetConnectionString();
                SqlConnection connection = new SqlConnection(connectionString);
                    if (connection.State != ConnectionState.Open)
                    connection.Open();
                var commandDefinition = new CommandDefinition($"SELECT * FROM {nameof(TempPatientExtract)}s", null, null, 0);

                //todo check invalid reader.... reader does not stream data
                using (IDataReader reader = await connection.ExecuteReaderAsync(commandDefinition, CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        count++;
                        //Auto mapper
                        var extractRecord = Mapper.Map<IDataRecord, PatientExtract>(reader);

                        extractRecord.Id = LiveGuid.NewGuid();
                        list.Add((PatientExtract)extractRecord);

                        if (count == batch)
                        {
                            _patientExtractRepository.BatchInsert(list);
                            count = 0;
                            Log.Debug("saved batch");
                        }

                    }

                    if (count > 0)
                    {
                        // save remaining list;
                        _patientExtractRepository.BatchInsert(list);
                    }
                    _patientExtractRepository.CloseConnection();
                }
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientExtract)} not Loaded");
                return false;
            }
        }
    }
}
