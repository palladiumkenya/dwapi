using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Data;
using Dwapi.ExtractsManagement.Core.Interfaces.Stage.Source.Psmart.Reader;
using Dwapi.ExtractsManagement.Core.Model.Source;
using Dwapi.ExtractsManagement.Core.Model.Source.Psmart;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MySql.Data.MySqlClient;
using Serilog;

namespace Dwapi.ExtractsManagement.Infrastructure.Source.Psmart.Reader
{
    public class PsmartSourceReader : IPsmartSourceReader
    {
        private readonly ReadSummary _summary = new ReadSummary();
        private IMapper _mapper;


        public ReadSummary Summary => _summary;

        public int Find(DbProtocol protocol, DbExtract extract)
        {
            Log.Debug($"Finding {nameof(PsmartSource)}...");

            _mapper = GetMapper(extract.Emr);
            int extractCount = 0;
            var connection = GetConnection(protocol);


            _summary.Status = $"Analyzing {nameof(PsmartSource)}...";


            using (connection)
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (var command = connection.CreateCommand())
                {
                    //Extract SQL
                    command.CommandText = extract.GetCountSQL();
                    int.TryParse(command.ExecuteScalar().ToString(),out extractCount);
                }
            }

            _summary.Status = $"Analyzing {nameof(PsmartSource)} Completed";

            Log.Debug($"Finding {nameof(PsmartSource)} Completed");
            return extractCount;
        }

        public IEnumerable<PsmartSource> Read(DbProtocol protocol, DbExtract extract)
        {
            _mapper = GetMapper(extract.Emr);
            IEnumerable<PsmartSource> extracts = new List<PsmartSource>();
            var connection = GetConnection(protocol);


            _summary.Status = $"Reading {nameof(PsmartSource)}...";


            using (connection)
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (var command = connection.CreateCommand())
                {
                    //Extract SQL
                    command.CommandText = extract.ExtractSql;

                    extracts = _mapper.Map<IDataReader, IEnumerable<PsmartSource>>(command.ExecuteReader());
                }
            }

            _summary.Status = $"Reading {nameof(PsmartSource)} Completed";

            return extracts;
        }

        public IDbConnection GetConnection(DbProtocol databaseProtocol)
        {
            var connectionString = databaseProtocol.GetConnectionString();

            if (databaseProtocol.DatabaseType == DatabaseType.MicrosoftSQL)
                return new SqlConnection(connectionString);

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
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.Emr, opt => opt.UseValue(emr))
                    .ForMember(x => x.FacilityCode, opt => opt.Ignore())
                    .ForMember(x => x.DateExtracted, opt => opt.Ignore());
            });
            return new Mapper(config);
        }
    }
}