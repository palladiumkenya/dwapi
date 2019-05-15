using System;
using System.Linq;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Hts;
using Dwapi.UploadManagement.Core.Packager.Cbs;
using Dwapi.UploadManagement.Core.Packager.Hts;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader;
using Dwapi.UploadManagement.Infrastructure.Reader.Cbs;
using Dwapi.UploadManagement.Infrastructure.Reader.Hts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Packager.Hts
{
    [TestFixture]
    [Category("Hts")]
    public class HtsPackagerTests
    {
        private IServiceProvider _serviceProvider;
        private IHtsPackager _packager;

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
                .AddTransient<IHtsExtractReader, HtsExtractReader>()
                .AddTransient<IEmrMetricReader, EmrMetricReader>()
                .AddTransient<IHtsPackager, HtsPackager>()
                .BuildServiceProvider();
        }


        [SetUp]
        public void SetUp()
        {
            _packager = _serviceProvider.GetService<IHtsPackager>();
        }

        [Test]
        public void should_Generate_Manifest()
        {
            var manfiests = _packager.Generate();
            Assert.True(manfiests.Any());
            var m = manfiests.First();
            Assert.True(m.Cargoes.Any());
            Console.WriteLine($"{m} |{m.Cargoes.First().Type} < {m.Cargoes.First().Items} ");
        }

        [Test]
        public void should_Generate_Manifest_With_Metrics()
        {
            var manfiests = _packager.GenerateWithMetrics();
            Assert.True(manfiests.Any());
            var m = manfiests.First();
            Assert.True(m.Cargoes.Any(x=>x.Type==CargoType.Patient));
            Assert.True(m.Cargoes.Any(x=>x.Type==CargoType.Metrics));
            Console.WriteLine($"{m} |{m.Cargoes.First().Type} < {m.Cargoes.First(x=>x.Type==CargoType.Patient).Items} ");
            Console.WriteLine($"{m} |{m.Cargoes.First().Type} < {m.Cargoes.First(x=>x.Type==CargoType.Metrics).Items} ");
        }
    }
}
