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

        [Test]
        public void should_Setup_Mssql_Database()
        {
            var ctx = TestInitializer.ServiceProvider.GetService<SettingsContext>();
            Console.WriteLine(ctx.Database.GetDbConnection().ConnectionString);
            ctx.Database.EnsureDeleted();
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
           var ctx =TestInitializer.ServiceProviderMysql.GetService<SettingsContext>();
            Console.WriteLine(ctx.Database.GetDbConnection().ConnectionString);
            ctx.Database.EnsureDeleted();
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
