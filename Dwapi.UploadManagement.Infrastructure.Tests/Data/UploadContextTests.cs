using System;
using System.Linq;
using System.Reflection;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Infrastructure.Tests.Data
{
    [TestFixture]
    public class UploadContextTests
    {
        private IServiceProvider _serviceProvider;
        private UploadContext _context;


        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DwapiConnection"];

            _serviceProvider = new ServiceCollection()
                .AddDbContext<Dwapi.SettingsManagement.Infrastructure.SettingsContext>(o => o.UseSqlServer(connectionString))
                .AddDbContext<UploadContext>(o => o.UseSqlServer(connectionString))
                .BuildServiceProvider();

            _context = _serviceProvider.GetService<UploadContext>();
        }


        [Test]
        public void should_load_data_From_References()
        {
            Assert.True(_context.ClientMasterPatientIndices.Any());
            Assert.True(_context.ClientPatientExtracts.Any());
            Assert.True(_context.ClientPatientArtExtracts.Any());
            Assert.True(_context.ClientPatientBaselinesExtracts.Any());
            Assert.True(_context.ClientPatientLaboratoryExtracts.Any());
            Assert.True(_context.ClientPatientPharmacyExtracts.Any());
            Assert.True(_context.ClientPatientStatusExtracts.Any());
            Assert.True(_context.ClientPatientVisitExtracts.Any());
            Assert.True(_context.ClientPatientAdverseEventExtracts.Any());
        }

        [Test]
        public void should_load_Metrics()
        {
            Assert.True(_context.EmrMetrics.Any());
        }
    }
}
