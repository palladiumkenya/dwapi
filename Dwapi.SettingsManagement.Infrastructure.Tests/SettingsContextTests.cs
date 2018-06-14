using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Infrastructure.Tests
{
    [TestFixture]
    [Category("Db")]
    public class SettingsContextTests
    {

        private IServiceProvider _serviceProvider;
        private IServiceProvider _serviceProviderMysql;
        private const string MssqlConnection = "Data Source=.\\koske14;Initial Catalog=dwapidevb;Persist Security Info=True;User ID=sa;Password=maun;MultipleActiveResultSets=True";
        private const string MysqlConnection = "server=127.0.0.1;port=3306;database=dwapidevb;user=root;password=test";

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<SettingsContext>(x => x.UseSqlServer(MssqlConnection))
                .BuildServiceProvider();

            _serviceProviderMysql = new ServiceCollection()
                .AddDbContext<SettingsContext>(x => x.UseMySql(MysqlConnection))
                .BuildServiceProvider();
        }

        [Test]
        public void should_Setup_Mssql_Database()
        {
            var ctx = _serviceProvider.GetService<SettingsContext>();

            ctx.Database.Migrate();
            ctx.EnsureSeeded();

            Assert.True(ctx.Dockets.Any());
            Assert.True(ctx.EmrSystems.Any());
            Assert.True(ctx.DatabaseProtocols.Any());
            Assert.True(ctx.Extracts.Any());
            Assert.True(ctx.CentralRegistries.Any());

            Console.WriteLine(ctx.Database.ProviderName);
        }
        [Test]
        public void should_Setup_MySql_Database()
        {
            var ctx = _serviceProviderMysql.GetService<SettingsContext>();

            ctx.Database.Migrate();
            ctx.EnsureSeeded();

            Assert.True(ctx.Dockets.Any());
            Assert.True(ctx.EmrSystems.Any());
            Assert.True(ctx.DatabaseProtocols.Any());
            Assert.True(ctx.Extracts.Any());
            Assert.True(ctx.CentralRegistries.Any());

            Console.WriteLine(ctx.Database.ProviderName);
        }
    }
}