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


        private IServiceProvider _serviceProvider;
        private IServiceProvider _serviceProviderMysql;
        private IAppDatabaseManager _dbManager;
        private const string MssqlConnection= "Data Source=.\\koske14;Initial Catalog=dwapidev;Persist Security Info=True;User ID=sa;Password=maun;MultipleActiveResultSets=True";
        private const string MysqlConnection= "server=127.0.0.1;port=3306;database=dwapidev;user=root;password=test";

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<SettingsContext>(x=>x.UseSqlServer(MssqlConnection))
                .AddTransient<IAppDatabaseManager, AppDatabaseManager>()
                .BuildServiceProvider();

            _serviceProviderMysql = new ServiceCollection()
                .AddDbContext<SettingsContext>(x => x.UseMySql(MysqlConnection))
                .AddTransient<IAppDatabaseManager, AppDatabaseManager>()
                .BuildServiceProvider();
            var ctx = _serviceProvider.GetService<SettingsContext>();
            ctx.Database.Migrate();
            var ctxMysql = _serviceProviderMysql.GetService<SettingsContext>();
            ctxMysql.Database.Migrate();
        }

        [SetUp]
        public void SetUp()
        {
            _dbManager = _serviceProvider.GetService<IAppDatabaseManager>();
        }

        [TestCase(MssqlConnection,DatabaseProvider.MsSql)]
        [TestCase(MysqlConnection, DatabaseProvider.MySql)]
        public void should_ReadConnection_To_DbProtocol(string connection,DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(connection, provider);
            Assert.NotNull(dbprotocol);
            Console.WriteLine(dbprotocol);
        }

        [TestCase(MssqlConnection, DatabaseProvider.MsSql)]
        [TestCase(MysqlConnection, DatabaseProvider.MySql)]
        public void should_BuildConnection_From_DbProtocol(string connection, DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(connection, provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.BuildConncetion(dbprotocol);
            Assert.False(string.IsNullOrEmpty(cn));
            Console.WriteLine(dbprotocol);
            Console.WriteLine($"    {cn}");
        }

        [TestCase(MssqlConnection, DatabaseProvider.MsSql)]
        [TestCase(MysqlConnection, DatabaseProvider.MySql)]
        public void should_verify_Connection_From_ConnectionString(string connection, DatabaseProvider provider)
        {
          var cn = _dbManager.Verfiy(connection,provider);
            Assert.True(cn);
            Console.WriteLine($" Connected [{provider}] >>> {connection} <<<");
        }

        [TestCase(MssqlConnection, DatabaseProvider.MsSql)]
        [TestCase(MysqlConnection, DatabaseProvider.MySql)]
        public void should_verify_Server_Connection_From_DbProtocol(string connection, DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(connection, provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.VerfiyServer(dbprotocol);
            Assert.True(cn);
            Console.WriteLine($" Connected [{dbprotocol}]");
        }

        [TestCase(MssqlConnection, DatabaseProvider.MsSql)]
        [TestCase(MysqlConnection, DatabaseProvider.MySql)]
        public void should_verify_Server_Database_Connection_From_DbProtocol(string connection, DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(connection, provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.Verfiy(dbprotocol);
            Assert.True(cn);
            Console.WriteLine($" Connected [{dbprotocol}]");
        }

        [TestCase(MssqlConnection, DatabaseProvider.MsSql)]
        [TestCase(MysqlConnection, DatabaseProvider.MySql)]
        public void should_Read_Data_Using_Connection_String(string connection, DatabaseProvider provider)
        {
            var dbprotocol = _dbManager.ReadConnection(connection, provider);
            Assert.NotNull(dbprotocol);

            var cn = _dbManager.BuildConncetion(dbprotocol);
            var optionsBuilder = new DbContextOptionsBuilder<SettingsContext>();
            if(provider == DatabaseProvider.MsSql)
            optionsBuilder.UseSqlServer(connection);
            if(provider == DatabaseProvider.MySql)
                optionsBuilder.UseMySql(connection);
            
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
    }
}