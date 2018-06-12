using System;
using System.Linq;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader.Dwh;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Infrastructure.Tests.Reader
{
    [TestFixture]
    public class DwhExtractReaderTests
    {
        private IServiceProvider _serviceProvider;
        private UploadContext _context;
        private IDwhExtractReader _reader;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DwapiConnection"];

            _serviceProvider = new ServiceCollection()
                .AddDbContext<Dwapi.SettingsManagement.Infrastructure.SettingsContext>(o => o.UseSqlServer(connectionString))
                .AddDbContext<ExtractsContext>(o => o.UseSqlServer(connectionString))
                .AddDbContext<UploadContext>(o => o.UseSqlServer(connectionString))
                .AddTransient<IDwhExtractReader, DwhExtractReader>()
                .BuildServiceProvider();

            _context = _serviceProvider.GetService<UploadContext>();
        }

        [SetUp]
        public void SetUp()
        {
            _reader = _serviceProvider.GetService<IDwhExtractReader>();
        }

        [Test]
        public void should_ReadProfiles()
        {
            var profiles = _reader.ReadProfiles().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_ReadExtracts()
        {
            var extractViews = _reader.ReadAll().ToList();
            Assert.True(extractViews.Any());
        }
    }
}