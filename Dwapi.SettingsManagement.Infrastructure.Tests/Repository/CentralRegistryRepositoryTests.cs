using System;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class CentralRegistryRepositoryTests
    {
        private ICentralRegistryRepository _centralRegistryRepository;
        private DbContextOptions<SettingsContext> _options;
        private SettingsContext _context;

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
            _centralRegistryRepository =new CentralRegistryRepository(_context);
        }

        [Test]
        public void should_GetDefault_Registry()
        {
            var centralRegistry = _centralRegistryRepository.GetDefault();
            Assert.NotNull(centralRegistry);
            Console.WriteLine(centralRegistry);
        }

        [Test]
        public void should_SaveDefault_Registry()
        {
            var centralRegistries = _centralRegistryRepository.GetAll().ToList();
            Assert.True(centralRegistries.Count > 0);
            var newReg = new CentralRegistry("Test Registry", "http://52.178.24.227:4747/api", "PSMART", "xyz");

            _centralRegistryRepository.SaveDefault(newReg);
            _centralRegistryRepository.SaveChanges();

            var defaultCentralRegistry= _centralRegistryRepository.GetDefault();
            Assert.NotNull(defaultCentralRegistry);
            Assert.AreEqual(newReg.Name, defaultCentralRegistry.Name);
            Assert.AreEqual(newReg.Url, defaultCentralRegistry.Url);
            Assert.AreEqual(newReg.AuthToken, defaultCentralRegistry.AuthToken);

            var registryCount = _centralRegistryRepository.GetAll().ToList().Count;
            Assert.True(registryCount==1);

            Console.WriteLine(defaultCentralRegistry);
        }
    }
}
