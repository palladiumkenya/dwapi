
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
        private readonly string _authToken = @"268DFA3EB92BC53FAE94A048E23112A1";
        private IRegistryManagerService _registryManagerService;
        private CentralRegistry _centralRegistry;

        [SetUp]
        public void Setup()
        {
            _registryManagerService=new RegistryManagerService(null);
            _centralRegistry=new CentralRegistry("hAPI", "http://52.178.24.227:8026",_authToken);

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