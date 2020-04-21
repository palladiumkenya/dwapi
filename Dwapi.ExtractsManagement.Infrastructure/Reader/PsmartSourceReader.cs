using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Data;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader;
using Dwapi.ExtractsManagement.Core.Model.Source;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Serilog;

namespace Dwapi.ExtractsManagement.Infrastructure.Reader
{
    public class PsmartSourceReader : IPsmartSourceReader
    {
        private IMapper _mapper;


        public IDbConnection Connection { get; private set; }
        public object CommandDefinition { get; private set; }

        public int Find(DbProtocol protocol, DbExtract extract)
        {
            Log.Debug($"Finding {nameof(PsmartSource)}...");

            _mapper = GetMapper(extract.Emr);
            int extractCount = 0;
            var connection = GetConnection(protocol);


            using (connection)
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (var command = connection.CreateCommand())
                {
                    //Extract SQL
                    command.CommandText = extract.GetCountSQL();
                    int.TryParse(command.ExecuteScalar().ToString(), out extractCount);
                }
            }

            Log.Debug($"Finding {nameof(PsmartSource)} Completed");
            return extractCount;
        }

        public IEnumerable<PsmartSource> Read(DbProtocol protocol, DbExtract extract)
        {
            _mapper = GetMapper(extract.Emr);
            IList<PsmartSource> extracts = new List<PsmartSource>();
            var connection = GetConnection(protocol);

            using (connection)
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (var command = connection.CreateCommand())
                {
                    //Extract SQL
                    command.CommandText = extract.ExtractSql;

                    extracts = _mapper.Map<IDataReader, IList<PsmartSource>>(command.ExecuteReader());
                }
            }

            if (extracts.Count > 0)
            {
                using (connection = GetConnection(protocol))
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (var updateCommand = connection.CreateCommand())
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = 0; i < extracts.Count; i++)
                        {
                            if (i == extracts.Count - 1)
                                stringBuilder.Append($"'{extracts[i].Uuid}'");
                            else
                                stringBuilder.Append($"'{extracts[i].Uuid}',");
                        }
                        updateCommand.CommandText = $"update psmart_store set Status = 'Collected', Status_date = '{DateTime.Now.Date:yyyy-MM-dd HH:mm:ss}' where UUID in ({stringBuilder.ToString()})";
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
            return extracts;

        }

        public Task<IDataReader> ExecuteReader(DbProtocol protocol, DbExtract extract)
        {
            throw new NotImplementedException();
        }

        public void PrepReader(DbProtocol protocol, DbExtract extract)
        {
            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");

            if (null == extract)
                throw new Exception("Extract settings not configured");

            Connection = sourceConnection;
            CommandDefinition = new CommandDefinition(extract.ExtractSql, null, null, 0);
        }

        public IDbConnection GetConnection(DbProtocol databaseProtocol)
        {
            var connectionString = databaseProtocol.GetConnectionString();

            if (databaseProtocol.DatabaseType == DatabaseType.Sqlite)
                return new SqliteConnection(connectionString);

            if (databaseProtocol.DatabaseType == DatabaseType.MicrosoftSQL)
                return new System.Data.SqlClient.SqlConnection(connectionString);

            if (databaseProtocol.DatabaseType == DatabaseType.MySQL)
                return new MySqlConnection(connectionString);

            return null;
        }

        private IMapper GetMapper(string emr)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddDataReaderMapping();
                cfg.CreateMissingTypeMaps = false;
                cfg.AllowNullCollections = true;
                cfg.CreateMap<IDataReader, PsmartSource>()
                    .ForMember(x=>x.Uuid,opt => opt.MapFrom(src =>src.GetValue(src.GetOrdinal("uuid")).ToString()))
                    .ForMember(x => x.EId, opt => opt.Ignore())
                    .ForMember(x => x.Emr, opt => opt.UseValue(emr))
                    .ForMember(x => x.DateExtracted, opt => opt.Ignore());
            });
            return new Mapper(config);
        }
    }


}
