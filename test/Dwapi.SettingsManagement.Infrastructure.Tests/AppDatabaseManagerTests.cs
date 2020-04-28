using System.Configuration;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SharedKernel.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.SettingsManagement.Infrastructure.Tests
{
    [TestFixture]
    public class AppDatabaseManagerTests
    {
        private IAppDatabaseManager _dbManager;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
        }

        [SetUp]
        public void SetUp()
        {
            _dbManager =TestInitializer.ServiceProvider.GetService<IAppDatabaseManager>();
        }

        [TestCase(DatabaseProvider.MsSql)]
        [TestCase(DatabaseProvider.MySql)]
        [TestCase(DatabaseProvider.Other)]
        public void should_ReadConnection_To_DbProtocol(DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(GetConnection(provider), provider);
            Assert.NotNull(dbprotocol);
            Log.Debug($"{dbprotocol}");
        }

        [TestCase(DatabaseProvider.MsSql)]
        [TestCase(DatabaseProvider.MySql)]
        [TestCase(DatabaseProvider.Other)]
        public void should_BuildConnection_From_DbProtocol(DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(GetConnection(provider), provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.BuildConncetion(dbprotocol);
            Assert.False(string.IsNullOrEmpty(cn));
            Log.Debug($"{dbprotocol}");
            Log.Debug($"    {cn}");
        }

        [TestCase(DatabaseProvider.Other)]
        public void should_verify_Connection_From_ConnectionString(DatabaseProvider provider)
        {
          var cn = _dbManager.Verfiy(GetConnection(provider),provider);
            Assert.True(cn);
            Log.Debug($" Connected [{provider}] >>> {GetConnection(provider)} <<<");
        }

        [TestCase(DatabaseProvider.Other)]
        public void should_verify_Server_Connection_From_DbProtocol(DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(GetConnection(provider), provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.VerfiyServer(dbprotocol);
            Assert.True(cn);
            Log.Debug($" Connected [{dbprotocol}]");
        }

        [TestCase(DatabaseProvider.Other)]
        public void should_verify_Server_Database_Connection_From_DbProtocol(DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(GetConnection(provider), provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.Verfiy(dbprotocol);
            Assert.True(cn);
            Log.Debug($" Connected [{dbprotocol}]");
        }

        [TestCase(DatabaseProvider.Other)]
        public void should_Read_Data_Using_Connection_String(DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(GetConnection(provider), provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.BuildConncetion(dbprotocol);
            var optionsBuilder = new DbContextOptionsBuilder<SettingsContext>();
            if (provider == DatabaseProvider.Other)
                optionsBuilder.UseSqlite(cn);

            using (var context = new SettingsContext(optionsBuilder.Options))
            {
                Log.Debug(context.Database.ProviderName);

                var emrSystems = context.EmrSystems.Include(x => x.Extracts).ToList();
                Assert.True(emrSystems.Any());
                foreach (var facility in emrSystems)
                {
                    Log.Debug($"{facility}");
                }
            }
        }

        private string GetConnection(DatabaseProvider provider)
        {
            if (provider == DatabaseProvider.Other)
            {
                return TestInitializer.ConnectionString;
            }
            if (provider == DatabaseProvider.MsSql)
            {
                return TestInitializer.MsSqlConnectionString;
            }
            if (provider == DatabaseProvider.MySql)
            {
                return TestInitializer.MySqlConnectionString;
            }
            return string.Empty;
        }
    }
}
