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

namespace Dwapi.ExtractsManagement.Infrastructure.Reader.SmartCard
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

        public Task<IDataReader> ExecuteReader(DbProtocol protocol, DbExtract extract, DateTime? maxCreated, DateTime? maxModified, int siteCode)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReaderSync(DbProtocol protocol, DbExtract extract)
        {
            throw new NotImplementedException();
        }

        public bool CheckDiffSupport(DbProtocol protocol)
        {
            throw new NotImplementedException();
        }

        public string RefreshEtlTtables(DbProtocol protocol)
        {
            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");
        
            using (sourceConnection)
            {
                var sql = $@"CALL sp_scheduled_updates()";
        
                sourceConnection.Execute(sql);
                return "status:200";
            }
        }

        public DateTime? GetEtlTtablesRefreshedDate(DbProtocol protocol)
        {
            throw new NotImplementedException();
        }

        public void ChangeSQLmode(DbProtocol protocol)
        {
            throw new NotImplementedException();
        }


        public IDbConnection GetConnection(DbProtocol databaseProtocol)
        {
            var connectionString = databaseProtocol.GetConnectionString();

            if (databaseProtocol.DatabaseType == DatabaseType.Sqlite)
                return new SqliteConnection(connectionString);

            if (databaseProtocol.DatabaseType == DatabaseType.MicrosoftSQL)
                return new Microsoft.Data.SqlClient.SqlConnection(connectionString);

            if (databaseProtocol.DatabaseType == DatabaseType.MySQL)
                return new MySqlConnection(connectionString);

            return null;
        }

        private IMapper GetMapper(string emr)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddDataReaderMapping();
                // cfg.CreateMissingTypeMaps = false;
                cfg.AllowNullCollections = true;
                cfg.CreateMap<IDataReader, PsmartSource>()
                    .ForMember(x=>x.Uuid,opt => opt.MapFrom(src =>src.GetValue(src.GetOrdinal("uuid")).ToString()))
                    .ForMember(x => x.EId, opt => opt.Ignore())
                    .ForMember(x => x.Emr, opt => opt.MapFrom(src => emr))
                    .ForMember(x => x.DateExtracted, opt => opt.Ignore());
            });
            return new Mapper(config);
        }
    }


}
