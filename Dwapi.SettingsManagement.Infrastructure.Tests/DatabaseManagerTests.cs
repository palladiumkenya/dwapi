using System;
using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Model;
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
            _databaseManager = new DatabaseManager();
        }

        [Test]
        public void should_GetConnection()
        {
            TestInitializer.InitDbMsSql();
            var databaseProtocol = TestInitializer.IQtoolsDbProtocol;
            var connection = _databaseManager.GetConnection(databaseProtocol);
            Assert.NotNull(connection);

            Console.WriteLine($"connections Found:[{connection.ConnectionString}] | {databaseProtocol.DatabaseType}");
        }

        [Test]
        public void should_VerifyConnection()
        {
            TestInitializer.InitDbMsSql();
            var databaseProtocol = TestInitializer.IQtoolsDbProtocol;
            var connected = _databaseManager.VerifyConnection(databaseProtocol);
            Assert.True(connected);

            Console.WriteLine($"connection OK:[{databaseProtocol}] | {databaseProtocol.DatabaseType}");
        }

        [Test]
        public void should_MySQL_GetConnection()
        {
            TestInitializer.InitDbMySql();
            var databaseProtocol = TestInitializer.IQtoolsDbProtocol;
            var connection = _databaseManager.GetConnection(databaseProtocol);
            Assert.NotNull(connection);

            Console.WriteLine($"connections Found:[{connection.ConnectionString}] | {databaseProtocol.DatabaseType}");
        }

        [Test]
        public void should_MySQL_VerifyConnection()
        {
            TestInitializer.InitDbMySql();
            var databaseProtocol = TestInitializer.IQtoolsDbProtocol;
            var connected = _databaseManager.VerifyConnection(databaseProtocol);
            Assert.True(connected);

            Console.WriteLine($"connection OK:[{databaseProtocol}] | {databaseProtocol.DatabaseType}");
        }

        [Test]
        public void should_Verify_MSSQL_Query()
        {
            TestInitializer.InitDbMsSql();
            var extract = Builder<Extract>.CreateNew().Build();
            extract.ExtractSql = @"SELECT * FROM psmart_store";

            var databaseProtocol = TestInitializer.IQtoolsDbProtocol;
            var verified = _databaseManager.VerifyQuery(extract, databaseProtocol);
            Assert.True(verified);

            Console.WriteLine($"{extract} query [{extract.ExtractSql}] OK | {databaseProtocol.DatabaseType}");
        }

        [Test]
        public void should_Verify_MySQL_Query()
        {
            TestInitializer.InitDbMySql();
            var extract = Builder<Extract>.CreateNew().Build();
            extract.ExtractSql = @"SELECT * FROM psmart_store";

            var databaseProtocol = TestInitializer.KenyaEmrDbProtocol;
            var verified = _databaseManager.VerifyQuery(extract, databaseProtocol);
            Assert.True(verified);

            Console.WriteLine($"{extract} query [{extract.ExtractSql}] OK | {databaseProtocol.DatabaseType}");
        }
    }
}
