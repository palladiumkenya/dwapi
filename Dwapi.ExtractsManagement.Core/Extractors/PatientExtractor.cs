using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Domain;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.SharedKernel.Model;
using Microsoft.EntityFrameworkCore;
using NPoco;
using FastMember;
using Dwapi.Domain.Utils;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;

namespace Dwapi.ExtractsManagement.Core.Extractors
{

    public class PatientExtractor : IExtractor
    {
        private readonly IExtractUnitOfWork _unitOfWork;
        private Func<IDatabase> _emrDatabaseFactory;
        private readonly IExtractStatusService _extractStatusService;

        public PatientExtractor(IExtractUnitOfWork unitOfWork, IExtractStatusService extractStatusService)
        {
            _unitOfWork = unitOfWork;
            _extractStatusService = extractStatusService;
        }

        //public async Task ExtractAsync(DwhExtract extract, DbProtocol dbProtocol)
        //{
        //    try
        //    {
        //        _emrDatabaseFactory = ExtractorHelper.NPocoEmrDataFactory(dbProtocol);
        //        var dwapiDbFactory = ExtractorHelper.NpocoDwapiDataFactory(_unitOfWork.Context.Database.GetDbConnection());

        //        using (var command = _emrDatabaseFactory().Connection.CreateCommand())
        //        {
        //            IList<TempPatientExtract> extracts = new List<TempPatientExtract>();

        //            var props = typeof(TempPatientExtract).GetProperties();
        //            var accessor = TypeAccessor.Create(typeof(TempPatientExtract));
        //            var members = accessor.GetMembers()
        //                .Where(x => !x.IsDefined(typeof(DoNotReadAttribute)))
        //                .ToList();

        //            command.CommandText = extract.SqlQuery;

        //            _emrDatabaseFactory().Connection.Open();
        //            using(var result = await command.ExecuteReaderAsync())
        //            {
        //                int batchLimit = 500, count = 0;

        //                while (result.Read())
        //                {
        //                    if (count == batchLimit)
        //                    {
        //                        using(var db = dwapiDbFactory())
        //                        {
        //                            db.InsertBulk(extracts);
        //                        }
        //                        count = 0;
        //                        extracts = new List<TempPatientExtract>(); 
        //                    }
                            

        //                    members.ForEach(x => 
        //                    {
        //                        var ordinal = result.GetOrdinal(x.Name);
        //                        result.GetValue(ordinal);
        //                        var ex = new TempPatientExtract
        //                        {

        //                        };
        //                    });
                            
        //                }
        //            }
        //        }

        //            IList<TempPatientExtract> tempPatientExtracts;
        //        using (var database = _emrDatabaseFactory())
        //            tempPatientExtracts = await database.FetchAsync<TempPatientExtract>(extract.SqlQuery);

        //        await _unitOfWork.Repository<TempPatientExtract>().AddRangeAsync(tempPatientExtracts);
        //        await _unitOfWork.SaveAsync();
                
        //    }

        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public async Task ExtractAsync(DwhExtract extract, DbProtocol dbProtocol)
        //{
        //    try
        //    {
        //        var sourceConn = ExtractorHelper.GetDbConnection(dbProtocol);
        //        var dwapiDbFactory = ExtractorHelper.NpocoDwapiDataFactory(_unitOfWork.Context.Database.GetDbConnection());

        //        sourceConn.Open();
        //        using (var command = sourceConn.CreateCommand())
        //        {
        //            command.CommandText = extract.SqlQuery;
        //            var reader = await command.ExecuteReaderAsync();

        //            using (var conn = dwapiDbFactory().Connection as SqlConnection)
        //            {
        //                conn.Open();

        //                using (var bulkCopy = new SqlBulkCopy(conn))
        //                {
        //                    bulkCopy.DestinationTableName = "dbo.TempPatientExtract";


        //                    try
        //                    {
        //                        var props = typeof(TempPatientExtract).GetProperties();
        //                        var accessor = TypeAccessor.Create(typeof(TempPatientExtract));
        //                        var members = accessor.GetMembers()
        //                            .Where(x => !x.IsDefined(typeof(DoNotReadAttribute)))
        //                            .ToList();

        //                        members.ForEach(x =>
        //                        {
        //                            bulkCopy.ColumnMappings.Add(x.Name, x.Name);
        //                        });

        //                        bulkCopy.WriteToServer(reader);
        //                    }

        //                    catch(Exception ex)
        //                    {
        //                        throw ex;
        //                    }

        //                    finally
        //                    {
        //                        reader.Close();
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


        // loading works
        // to do: map sql server dates to mysql correctly
        public async Task ExtractAsync(DwhExtract extract, DbProtocol dbProtocol)
        {
            int rowCount;
            var sourceConnection = ExtractorHelper.GetDbConnection(dbProtocol);
            sourceConnection.Open();

            using(var command = sourceConnection.CreateCommand())
            {
                var destinationConnection = _unitOfWork.Context.Database.GetDbConnection() as MySqlConnection;

                command.CommandText = extract.SqlQuery;
                var reader = await command.ExecuteReaderAsync();
                var datatable = new DataTable();
                datatable.Load(reader);
                var filePath = "patientExtractDump.csv";

                using (StreamWriter streamWriter = new StreamWriter(filePath))
                rowCount = WriteDataTable(datatable, streamWriter, GetColumnNames(destinationConnection));
                _extractStatusService.Found(extract, rowCount);

               
                var columns = GetColumnNames(destinationConnection);
                var bulkLoader = new MySqlBulkLoader(destinationConnection)
                {
                    TableName = nameof(TempPatientExtract).ToLower(),
                    FileName = filePath,
                    FieldTerminator = ",",
                    FieldQuotationCharacter = '"'
                };
                bulkLoader.Columns.Clear();
                bulkLoader.Columns.AddRange(columns);

                bulkLoader.Load();
                File.Delete(filePath);
            }
        }

        private List<string> GetDateColumns()
        {
            var accessor = TypeAccessor.Create(typeof(TempPatientExtract));
            var dateMembers = accessor.GetMembers()
                .Where(x => !x.IsDefined(typeof(DoNotReadAttribute)) && x.GetTheType() == typeof(DateTime))
                .Select(x => x.Name).ToList();
            return dateMembers;
        }

        
    
        private List<string> GetColumnNames(MySqlConnection conn)
        {
            var accessor = TypeAccessor.Create(typeof(TempPatientExtract));
            var members = accessor.GetMembers()
                .Where(x => x.IsDefined(typeof(DoNotReadAttribute)))
                .Select(x => x.Name).ToList();

            var result = new List<string>();
            if(conn.State != ConnectionState.Open) conn.Open();
            var sqlCmd = conn.CreateCommand();
                
            sqlCmd.CommandText = $"select * from {nameof(TempPatientExtract).ToLower()} where 1=0";  // No data wanted, only schema
            sqlCmd.CommandType = CommandType.Text;

            var sqlDR = sqlCmd.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(sqlDR);

            foreach (DataColumn column in dataTable.Columns)
                result.Add(column.ColumnName.ToString());

            sqlDR.Close();
            foreach(var elem in members)
            {
                result.Remove(elem);
            }

            return result;
        }

        private int WriteDataTable(DataTable sourceTable, TextWriter writer, List<string> columns, bool includeHeaders = false)
        {
            int count = 0;
            int ordinal = 0;
            foreach(var columnName in columns)
            {
                sourceTable.Columns[columnName].SetOrdinal(ordinal);
                ordinal++;
            }
            if (includeHeaders)
            {
                IEnumerable<String> headerValues = sourceTable.Columns
                    .OfType<DataColumn>()
                    .Select(column => QuoteValue(column.ColumnName));

                writer.WriteLine(String.Join(",", headerValues));
            }

            IEnumerable<String> items = null;

            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => QuoteValue(o?.ToString() ?? String.Empty));
                writer.WriteLine(String.Join(",", items));
                count++;
            }

            writer.Flush();
            return count;
        }

        private static string QuoteValue(string value)
        {
            return String.Concat("\"",
            value.Replace("\"", "\"\""), "\"");
        }
    }
}
