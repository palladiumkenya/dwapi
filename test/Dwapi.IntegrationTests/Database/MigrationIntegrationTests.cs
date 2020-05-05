using System.Data;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.IntegrationTests.Database
{
    [TestFixture]
    public class MigrationIntegrationTests
    {
        private IDbConnection _connection;

        [Test]
        public void should_run_MsSql_Migrations()
        {
            TestInitializer.SetupMsSql();
            var settingsContext = TestInitializer.MsSqlServiceProvider.GetService<SettingsContext>();
            var extractsContext = TestInitializer.MsSqlServiceProvider.GetService<ExtractsContext>();

            Assert.DoesNotThrow(() => settingsContext.Database.Migrate());
            Assert.DoesNotThrow(() => extractsContext.Database.Migrate());

            Assert.True(settingsContext.Database.IsSqlServer());
            Assert.True(extractsContext.Database.IsSqlServer());
            Assert.True(IsOnline(settingsContext.Database.GetDbConnection()));
            Assert.AreEqual(settingsContext.Database.GetDbConnection().ConnectionString,extractsContext.Database.GetDbConnection().ConnectionString);
            Log.Debug(settingsContext.Database.ProviderName);
            Log.Debug(settingsContext.Database.GetDbConnection().ServerVersion);
            _connection.Close();
        }


        [Test]
        public void should_run_MySqlL_Migrations()
        {
            TestInitializer.SetupMySql();
            var settingsContext = TestInitializer.MySqlServiceProvider.GetService<SettingsContext>();
            var extractsContext = TestInitializer.MySqlServiceProvider.GetService<ExtractsContext>();

            Assert.DoesNotThrow(() => settingsContext.Database.Migrate());
            Assert.DoesNotThrow(() => extractsContext.Database.Migrate());

            Assert.True(settingsContext.Database.IsMySql());
            Assert.True(extractsContext.Database.IsMySql());
            Assert.True(IsOnline(settingsContext.Database.GetDbConnection()));
            Assert.AreEqual(settingsContext.Database.GetDbConnection().ConnectionString,extractsContext.Database.GetDbConnection().ConnectionString);
            Log.Debug(settingsContext.Database.ProviderName);
            Log.Debug(settingsContext.Database.GetDbConnection().ServerVersion);
            _connection.Close();
        }

        private bool IsOnline(IDbConnection connection)
        {
            _connection = null;
            _connection = connection;
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            return _connection.State == ConnectionState.Open;
        }
    }
}
