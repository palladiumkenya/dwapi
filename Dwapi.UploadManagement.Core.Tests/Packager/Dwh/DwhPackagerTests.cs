using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Packager.Dwh;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader.Cbs;
using Dwapi.UploadManagement.Infrastructure.Reader.Dwh;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Packager.Dwh
{
    [TestFixture]
    [Category("Dwh")]
    public class DwhPackagerTests
    {
        private IServiceProvider _serviceProvider;
        private IDwhPackager _packager;
        private Guid _pid;
        
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
                .AddTransient<IDwhExtractReader, DwhExtractReader>()
                .AddTransient<IDwhPackager, DwhPackager>()
                .BuildServiceProvider();

            var ctx = _serviceProvider.GetService<UploadContext>();
            var art= ctx.ClientPatientArtExtracts.First();
            _pid = ctx.ClientPatientExtracts.First(x => x.PatientPK == art.PatientPK && x.SiteCode == art.SiteCode).Id;
        }


        [SetUp]
        public void SetUp()
        {
            _packager = _serviceProvider.GetService<IDwhPackager>();
          
        }

        [Test]
        public void should_Generate_Manifest()
        {
            var manfiests = _packager.Generate().ToList();
            Assert.True(manfiests.Any());
            var m = manfiests.First();
            Assert.True(m.PatientPks.Any());
            Console.WriteLine($"{m}");
        }

        [Test]
        public void should_Generate_Extracts()
        {
            var manfiests = _packager.GenerateExtracts(_pid);
            Assert.NotNull(manfiests);
            Assert.True(manfiests.PatientArtExtracts.Any());
        }
    }
}