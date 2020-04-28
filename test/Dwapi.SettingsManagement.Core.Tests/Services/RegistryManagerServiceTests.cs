using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.SettingsManagement.Core.Tests.Services
{
    [TestFixture]
    public class RegistryManagerServiceTests
    {
        private IRegistryManagerService _registryManagerService;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
        }

        [SetUp]
        public void Setup()
        {
            _registryManagerService = TestInitializer.ServiceProvider.GetService<IRegistryManagerService>();
        }

        [Test]
        public void should_GetByDocket()
        {
           var  centralRegistry = _registryManagerService.GetByDocket("CBS");
           Assert.NotNull(centralRegistry);
        }

        // [Test]
        public void should_Verify()
        {
            var  centralRegistry = _registryManagerService.GetByDocket("CBS");
            var verified = _registryManagerService.Verify(centralRegistry).Result;
            Assert.True(verified.Verified);
            Assert.False(string.IsNullOrEmpty(verified.RegistryName));
            Log.Debug($"{verified}");
        }
    }
}
