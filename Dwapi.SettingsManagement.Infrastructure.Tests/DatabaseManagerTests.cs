using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using FizzWare.NBuilder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.SettingsManagement.Infrastructure.Tests
{
    [TestFixture]
    public class DatabaseManagerTests
    {
        private IDatabaseManager _databaseManager;
        private DatabaseProtocol _databaseProtocol;

        [SetUp]
        public void SetUp()
        {
            _databaseManager = TestInitializer.ServiceProvider.GetService<IDatabaseManager>();
            _databaseProtocol=new DatabaseProtocol(DatabaseType.Sqlite,TestInitializer.EmrConnectionString);
        }

        [Test]
        public void should_GetConnection()
        {
            var connection = _databaseManager.GetConnection(_databaseProtocol);
            Assert.NotNull(connection);

            Log.Debug($"connections Found:[{connection.ConnectionString}] | {_databaseProtocol.DatabaseType}");
        }

        [Test]
        public void should_VerifyConnection()
        {
            var connected = _databaseManager.VerifyConnection(_databaseProtocol);
            Assert.True(connected);

            Log.Debug($"connection OK:[{_databaseProtocol}] | {_databaseProtocol.DatabaseType}");
        }

        [Test]
        public void should_Verify_Query()
        {
            var extract = Builder<Extract>.CreateNew().Build();
            extract.ExtractSql = @"SELECT * FROM TempPatientExtracts";
            var verified = _databaseManager.VerifyQuery(extract, _databaseProtocol);
            Assert.True(verified);

            Log.Debug($"{extract} query [{extract.ExtractSql}] OK | {_databaseProtocol.DatabaseType}");
        }
    }
}
