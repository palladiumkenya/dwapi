using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Data;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using MySql.Data.MySqlClient;
using Serilog;

namespace Dwapi.ExtractsManagement.Infrastructure.Reader.Dwh
{
    public class PatientSourceReader<TempPatientExtract> : IPatientSourceReader
    {
        public int Find(DbProtocol protocol, DbExtract extract)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Core.Model.Source.Dwh.TempPatientExtract> Read(DbProtocol protocol, DbExtract extract)
        {
            throw new NotImplementedException();
        }

        public Task<IDataReader> ExecuteReader(DbProtocol protocol, DbExtract extract)
        {
            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");

            if (null == extract)
                throw new Exception($"{nameof(TempPatientExtract)} settings not configured");


            if (sourceConnection.State != ConnectionState.Open)
                sourceConnection.Open();


            var commandDefinition = new CommandDefinition(extract.ExtractSql, null, null, 0);
            return sourceConnection.ExecuteReaderAsync(commandDefinition, CommandBehavior.CloseConnection);
        }

        public IDbConnection GetConnection(DbProtocol databaseProtocol)
        {
            var connectionString = databaseProtocol.GetConnectionString();

            if (databaseProtocol.DatabaseType == DatabaseType.MicrosoftSQL)
                return new System.Data.SqlClient.SqlConnection(connectionString);

            if (databaseProtocol.DatabaseType == DatabaseType.MySQL)
                return new MySqlConnection(connectionString);

            return null;
        }
    }
}