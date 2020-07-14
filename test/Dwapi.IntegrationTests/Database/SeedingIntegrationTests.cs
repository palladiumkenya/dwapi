using System.Linq;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.IntegrationTests.Database
{
    [TestFixture]
    public class SeedingIntegrationTests
    {
        [Test]
        public void should_seed_MsSql()
        {
            TestInitializer.SetupMsSql();
            var settingsContext = TestInitializer.MsSqlServiceProvider.GetService<SettingsContext>();
            var extractsContext = TestInitializer.MySqlServiceProvider.GetService<ExtractsContext>();
            settingsContext.Database.Migrate();
            extractsContext.Database.Migrate();

            Assert.DoesNotThrow(() => settingsContext.EnsureSeeded());
            Assert.DoesNotThrow(() => extractsContext.EnsureSeeded());

            Assert.True(settingsContext.Database.IsSqlServer());
            Assert.True(settingsContext.Dockets.Any());
            Assert.True(settingsContext.CentralRegistries.Any());
            Assert.True(settingsContext.EmrSystems.Any());
            Assert.True(settingsContext.DatabaseProtocols.Any());
            Assert.True(settingsContext.Extracts.Any());
            Assert.True(settingsContext.RestProtocols.Any());
            Assert.True(settingsContext.Resources.Any());

            Assert.True(extractsContext.Validator.Any());
        }

        [Test]
        public void should_seed_MySql()
        {
            TestInitializer.SetupMySql();
            var settingsContext = TestInitializer.MySqlServiceProvider.GetService<SettingsContext>();
            var extractsContext = TestInitializer.MySqlServiceProvider.GetService<ExtractsContext>();
            settingsContext.Database.Migrate();
            extractsContext.Database.Migrate();

            Assert.DoesNotThrow(() => settingsContext.EnsureSeeded());
            Assert.DoesNotThrow(() => extractsContext.EnsureSeeded());

            Assert.True(settingsContext.Database.IsMySql());
            Assert.True(settingsContext.Dockets.Any());
            Assert.True(settingsContext.CentralRegistries.Any());
            Assert.True(settingsContext.EmrSystems.Any());
            Assert.True(settingsContext.DatabaseProtocols.Any());
            Assert.True(settingsContext.Extracts.Any());
            Assert.True(settingsContext.RestProtocols.Any());
            Assert.True(settingsContext.Resources.Any());

            Assert.True(extractsContext.Validator.Any());
        }
    }
}
