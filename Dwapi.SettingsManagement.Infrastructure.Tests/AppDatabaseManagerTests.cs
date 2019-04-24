using System;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SharedKernel.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Infrastructure.Tests
{
    [TestFixture]
    public class AppDatabaseManagerTests
    {
        private IAppDatabaseManager _dbManager;
        [SetUp]
        public void SetUp()
        {
            _dbManager =TestInitializer.ServiceProvider.GetService<IAppDatabaseManager>();
        }

        [TestCase(DatabaseProvider.MsSql)]
        [TestCase(DatabaseProvider.MySql)]
        public void should_ReadConnection_To_DbProtocol(DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(GetConnection(provider), provider);
            Assert.NotNull(dbprotocol);
            Console.WriteLine(dbprotocol);
        }

        [TestCase(DatabaseProvider.MsSql)]
        [TestCase(DatabaseProvider.MySql)]
        public void should_BuildConnection_From_DbProtocol(DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(GetConnection(provider), provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.BuildConncetion(dbprotocol);
            Assert.False(string.IsNullOrEmpty(cn));
            Console.WriteLine(dbprotocol);
            Console.WriteLine($"    {cn}");
        }

        [TestCase(DatabaseProvider.MsSql)]
        [TestCase(DatabaseProvider.MySql)]
        public void should_verify_Connection_From_ConnectionString(DatabaseProvider provider)
        {
          var cn = _dbManager.Verfiy(GetConnection(provider),provider);
            Assert.True(cn);
            Console.WriteLine($" Connected [{provider}] >>> {GetConnection(provider)} <<<");
        }

        [TestCase(DatabaseProvider.MsSql)]
        [TestCase(DatabaseProvider.MySql)]
        public void should_verify_Server_Connection_From_DbProtocol(DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(GetConnection(provider), provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.VerfiyServer(dbprotocol);
            Assert.True(cn);
            Console.WriteLine($" Connected [{dbprotocol}]");
        }

        [TestCase(DatabaseProvider.MsSql)]
        [TestCase(DatabaseProvider.MySql)]
        public void should_verify_Server_Database_Connection_From_DbProtocol(DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(GetConnection(provider), provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.Verfiy(dbprotocol);
            Assert.True(cn);
            Console.WriteLine($" Connected [{dbprotocol}]");
        }

        [TestCase(DatabaseProvider.MsSql)]
        [TestCase(DatabaseProvider.MySql)]
        public void should_Read_Data_Using_Connection_String(DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(GetConnection(provider), provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.BuildConncetion(dbprotocol);
            var optionsBuilder = new DbContextOptionsBuilder<SettingsContext>();
            if (provider == DatabaseProvider.MsSql)
                optionsBuilder.UseSqlServer(cn);
            if (provider == DatabaseProvider.MySql)
                optionsBuilder.UseMySql(cn);

            using (var context = new SettingsContext(optionsBuilder.Options))
            {
                Console.WriteLine(context.Database.ProviderName);

                var facilities = context.EmrSystems.Include(x => x.Extracts).ToList();
                Assert.True(facilities.Any());
                Assert.True(facilities.First().Extracts.Any());

                foreach (var facility in facilities)
                {
                    Console.WriteLine(facility);
                    foreach (var clinic in facility.Extracts)
                    {
                        Console.WriteLine($" > {clinic}");
                    }
                }

            }

        }

        private string GetConnection(DatabaseProvider provider)
        {


            if (provider == DatabaseProvider.MySql)
            {
                if(string.IsNullOrWhiteSpace(TestInitializer.MySqlConnectionString))
                    TestInitializer.InitConnections();
                return TestInitializer.MySqlConnectionString;
            }

            if(string.IsNullOrWhiteSpace(TestInitializer.MsSqlConnectionString))
                TestInitializer.InitConnections();

            return TestInitializer.MsSqlConnectionString;
        }
    }
}
