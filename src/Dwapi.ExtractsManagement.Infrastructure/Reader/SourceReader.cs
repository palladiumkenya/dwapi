using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Serilog;

namespace Dwapi.ExtractsManagement.Infrastructure.Reader
{

    public abstract class SourceReader : ISourceReader
    {
        public virtual IDbConnection Connection { get; private set; }
        public virtual int Find(DbProtocol protocol, DbExtract extract)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IDataReader> ExecuteReader(DbProtocol protocol, DbExtract extract)
        {
            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");

            if (null == extract)
                throw new Exception("Extract settings not configured");

            if (sourceConnection.State != ConnectionState.Open)
                sourceConnection.Open();

            Connection = sourceConnection;
            var commandDefinition = new CommandDefinition(extract.ExtractSql, null, null, 3600);

            if (sourceConnection is SqliteConnection)
                return Task.FromResult<IDataReader>(sourceConnection.ExecuteReader(commandDefinition));

            return sourceConnection.ExecuteReaderAsync(commandDefinition, CommandBehavior.CloseConnection);
        }

        public Task<IDataReader> ExecuteReader(DbProtocol protocol, DbExtract extract, DateTime? maxCreated, DateTime? maxModified, int siteCode)
        {
            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");

            if (null == extract)
                throw new Exception("Extract settings not configured");

            if (sourceConnection.State != ConnectionState.Open)
                sourceConnection.Open();

            Connection = sourceConnection;
            var commandDefinition = new CommandDefinition(extract.GetDiffSQL(maxCreated, maxModified, siteCode,protocol), null, null, 3600);

            if (sourceConnection is SqliteConnection)
                return Task.FromResult<IDataReader>(sourceConnection.ExecuteReader(commandDefinition));

            return sourceConnection.ExecuteReaderAsync(commandDefinition, CommandBehavior.CloseConnection);
        }

        public IDataReader ExecuteReaderSync(DbProtocol protocol, DbExtract extract)
        {
            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");

            if (null == extract)
                throw new Exception("Extract settings not configured");

            if (sourceConnection.State != ConnectionState.Open)
                sourceConnection.Open();

            Connection = sourceConnection;
            var commandDefinition = new CommandDefinition(extract.ExtractSql, null, null, 3600);

            if (sourceConnection is SqliteConnection)
                return sourceConnection.ExecuteReader(commandDefinition);

            return sourceConnection.ExecuteReader(commandDefinition, CommandBehavior.CloseConnection);
        }

        public bool CheckDiffSupport(DbProtocol protocol)
        {
            if (!protocol.DiffSupport)
                return false;

            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");

            using (sourceConnection)
            {
                if(sourceConnection.State!=ConnectionState.Open)
                    sourceConnection.Open();
                var commandDefinition = new CommandDefinition(protocol.DiffSqlCheck, null, null, 0);

                try
                {
                    if (sourceConnection is SqliteConnection)
                    {
                        using (var diffReader =sourceConnection.ExecuteReader(commandDefinition))
                        {
                            diffReader.Read();
                        }
                    }
                    else
                    {
                        using (var diffReader = sourceConnection.ExecuteReader(commandDefinition, CommandBehavior.CloseConnection))
                        {
                            diffReader.Read();
                        }
                    }

                    return true;
                }
                catch (Exception e)
                {
                    Log.Warning("Missing differential upload feature in EMR");
                }
            }
            return false;
        }

        public IDbConnection GetConnection(DbProtocol databaseProtocol)
        {
            var connectionString = databaseProtocol.GetConnectionString();
            Log.Debug(new string('+',40));
            Log.Debug(connectionString);
            Log.Debug(new string('+',40));

            if (databaseProtocol.DatabaseType == DatabaseType.Sqlite)
                return new SqliteConnection(connectionString);

            if (databaseProtocol.DatabaseType == DatabaseType.MicrosoftSQL)
                return new System.Data.SqlClient.SqlConnection(connectionString);

            if (databaseProtocol.DatabaseType == DatabaseType.MySQL)
                return new MySqlConnection(connectionString);

            return null;
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
            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");
        
            using (sourceConnection)
            {
                var sql = $@"SELECT 'EMR_ETL_Refresh' as 'INDICATORNAME',
                                   stop_time                        as 'INDICATORVALUE',
                                   DATE_FORMAT(stop_time, '%Y%b%d') as 'INDICATORMONTH'
                            FROM kenyaemr_etl.etl_script_status s
                            where s.error is null
                              and script_name in ('scheduled_updates','initial_population_of_tables')
                           
                            order by INDICATORVALUE desc
                            limit 1";
        
                // var getRefreshDates = sourceConnection.Execute(sql);FirstOrDefault
                // var getRefreshDates = sourceConnection.from(sql).ToString().FirstOrDefault();
                var etlRefresh = sourceConnection.Query(sql).FirstOrDefault();
                var refreshDate = etlRefresh.INDICATORVALUE;

                return refreshDate;
            }
            
        }
        
        public void ChangeSQLmode(DbProtocol protocol)
        {
            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");
        
            using (sourceConnection)
            {
                var sql = $@"SET GLOBAL sql_mode=(SELECT REPLACE(@@sql_mode,'ONLY_FULL_GROUP_BY',''))";
                sourceConnection.Query(sql);
            }
            
        }
        
        

    }
}

















