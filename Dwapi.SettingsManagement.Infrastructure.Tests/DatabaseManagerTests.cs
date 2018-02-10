

using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Infrastructure.Tests
{
   [TestFixture]
    public class DatabaseManagerTests
   {
        private IDatabaseManager _databaseManager;

        [SetUp]
        public void SetUp()
        {
            _databaseManager=new DatabaseManager();
        }

        [TestCaseSource(nameof(DatabaseProtocols))]
        public void should_GetConnection(DatabaseProtocol databaseProtocol)
        {
            var connection = _databaseManager.GetConnection(databaseProtocol);
            Assert.NotNull(connection);

            Console.WriteLine($"connections Found:[{connection.ConnectionString}] | {databaseProtocol.DatabaseType}");
        }

       [TestCaseSource(nameof(DatabaseProtocols))]
        public void should_VerifyConnection(DatabaseProtocol databaseProtocol)
        {
            var connected = _databaseManager.VerifyConnection(databaseProtocol);
            Assert.True(connected);

            Console.WriteLine($"connection OK:[{databaseProtocol}] | {databaseProtocol.DatabaseType}");
        }

       [Test]
       public void should_Verify_MSSQL_Query()
       {
           var extract = Builder<Extract>.CreateNew().Build();
           extract.ExtractSql = @"SELECT * FROM [AppAdmin]";

           var databaseProtocol = DatabaseProtocols().First(x=>x.DatabaseType==DatabaseType.MicrosoftSQL);
           var verified = _databaseManager.VerifyQuery(extract,databaseProtocol);
           Assert.True(verified);

           Console.WriteLine($"{extract} query [{extract.ExtractSql}] OK | {databaseProtocol.DatabaseType}");
       }

       [Test]
       public void should_Verify_MySQL_Query()
       {
           var extract = Builder<Extract>.CreateNew().Build();
           extract.ExtractSql = @"SELECT * FROM psmart";

           var databaseProtocol = DatabaseProtocols().First(x => x.DatabaseType == DatabaseType.MySQL);
            var verified = _databaseManager.VerifyQuery(extract, databaseProtocol);
           Assert.True(verified);

           Console.WriteLine($"{extract} query [{extract.ExtractSql}] OK | {databaseProtocol.DatabaseType}");
       }


        public static List<DatabaseProtocol> DatabaseProtocols()
       {
           return new List<DatabaseProtocol>
           {
               new DatabaseProtocol(DatabaseType.MicrosoftSQL, @".\koske14", "sa", "maun", "iqcare"),
               new DatabaseProtocol(DatabaseType.MySQL, @"localhost", "root", "root", "testemr")
           };
       }
   }
}