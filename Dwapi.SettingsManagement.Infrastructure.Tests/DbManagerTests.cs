using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using myspot.Custom;
using myspot.Data;
using myspot.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Frameworks;
using NUnit.Framework;
using Remotion.Linq.Clauses.ResultOperators;

namespace myspot.Tests
{
    [TestFixture]
    public class DbManagerTests
    {


        private IServiceProvider _serviceProvider;
        private IServiceProvider _serviceProviderMysql;
        private IDbManager _dbManager;
        private const string MssqlConnection= "Data Source=209.97.129.83;Initial Catalog=myspotdev;Persist Security Info=True;User ID=sa;Password=M@un4747;MultipleActiveResultSets=True";
        private const string MysqlConnection= "server=209.97.129.83;port=3306;database=myspotdev;user=root;password=root";

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<MySpotContext>(x=>x.UseSqlServer(MssqlConnection))
                .AddTransient<IDbManager, DbManager>()
                .BuildServiceProvider();

            _serviceProviderMysql = new ServiceCollection()
                .AddDbContext<MySpotContext>(x => x.UseMySql(MysqlConnection))
                .AddTransient<IDbManager, DbManager>()
                .BuildServiceProvider();
            var ctx = _serviceProvider.GetService<MySpotContext>();
            ctx.Database.Migrate();
            var ctxMysql = _serviceProviderMysql.GetService<MySpotContext>();
            ctxMysql.Database.Migrate();
        }

        [SetUp]
        public void SetUp()
        {
            _dbManager = _serviceProvider.GetService<IDbManager>();
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
            var optionsBuilder = new DbContextOptionsBuilder<MySpotContext>();
            if(provider == DatabaseProvider.MsSql)
            optionsBuilder.UseSqlServer(connection);
            if(provider == DatabaseProvider.MySql)
                optionsBuilder.UseMySql(connection);
            
            using (var context = new MySpotContext(optionsBuilder.Options))
            {
                var facilities = context.Facilities.Include(x => x.Clinics).ToList();
                Assert.True(facilities.Any());
                Assert.True(facilities.First().Clinics.Any());

                foreach (var facility in facilities)
                {
                    Console.WriteLine(facility);
                    foreach (var clinic in facility.Clinics)
                    {
                        Console.WriteLine($" > {clinic}");
                    }
                }
                
            }
            
        }
    }
}