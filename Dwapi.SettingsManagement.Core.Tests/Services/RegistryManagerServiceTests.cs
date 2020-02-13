
using System;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Core.Services;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Core.Tests.Services
{
    [TestFixture]
    public class RegistryManagerServiceTests
    {
        private readonly string _authToken = @"1983aeda-6a96-11e8-adc0-fa7ae01bbebc";
        private IRegistryManagerService _registryManagerService;
        private CentralRegistry _centralRegistry;

        [SetUp]
        public void Setup()
        {
            _registryManagerService=new RegistryManagerService(null);
            _centralRegistry=new CentralRegistry("CBS Registry", "http://auth.kenyahmis.org:6767", "CBS", _authToken);

        }

        [Test]
        public void should_Verify()
        {
            var verified = _registryManagerService.Verify(_centralRegistry).Result;
            Assert.True(verified.Verified);
            Assert.False(string.IsNullOrEmpty(verified.RegistryName));
            Console.WriteLine(verified);
        }
    }
}
