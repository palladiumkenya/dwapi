
using System;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Core.Services;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Core.Tests.Services
{
    [TestFixture]
    public class RegistryServiceTests
    {
        private IRegistryManagerService _registryManagerService;
        private CentralRegistry _centralRegistry;

        [SetUp]
        public void Setup()
        {
            _registryManagerService=new RegistryManagerService(null);
            _centralRegistry=new CentralRegistry("hAPI","http://52.178.24.227:4747/api/cohorts/lists");
        }

        [Test]
        public void should_Verify()
        {
            var verified = _registryManagerService.Verify(_centralRegistry).Result;
            Assert.True(verified);
            Console.WriteLine(_centralRegistry);
        }

        
    }
}