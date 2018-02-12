using System;
using System.Linq;
using Dwapi.Controller;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Core.Services;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Dwapi.Tests.Controller
{
    [TestFixture]
    public class RegistryManagerControllerTests
    {
        private RegistryManagerController _registryManagerController;
        private ICentralRegistryRepository _centralRegistryRepository;
        private DbContextOptions<SettingsContext> _options;
        private SettingsContext _context;
        private CentralRegistry _centralRegistry;

        [OneTimeSetUp]
        public void Init()
        {
            _options = TestDbOptions.GetInMemoryOptions<SettingsContext>();
            var context = new SettingsContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var centralRegistries = Builder<CentralRegistry>.CreateListOfSize(2).Build().ToList();
            context.AddRange(centralRegistries);
            context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _context = new SettingsContext(_options);
            _centralRegistryRepository = new CentralRegistryRepository(_context);
            _registryManagerController=new RegistryManagerController(new RegistryManagerService(_centralRegistryRepository));

            _centralRegistry = Builder<CentralRegistry>.CreateNew()
                .With(x => x.Url = "http://52.178.24.227:5757")
                .Build();
        }

        [Test]
        public void should_Get_Default_Registry()
        {
            var response = _registryManagerController.Default();
            var result = response as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var centralRegistry = result.Value as CentralRegistry;
            Assert.NotNull(centralRegistry);
            Console.WriteLine(centralRegistry);
        }

        [Test]
        public void should_Post_Registry()
        {
            var response = _registryManagerController.Post(_centralRegistry);

            var result = response as OkResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var defaultCentralRegistry = _centralRegistryRepository.GetDefault();
            Assert.NotNull(defaultCentralRegistry);
            Assert.AreEqual(_centralRegistry.Id, defaultCentralRegistry.Id);
            Console.WriteLine(defaultCentralRegistry);
        }

        [Test]
        public void should_Post_VerifyRegistry()
        {
            var response = _registryManagerController.Verify(_centralRegistry).Result;
            var result = response as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var verifyResponse = result.Value as VerificationResponse;

            Assert.True(verifyResponse.Verified);
            Assert.False(string.IsNullOrWhiteSpace(verifyResponse.RegistryName));
            Console.WriteLine(verifyResponse);
        }
    }
}