using System;
using System.Linq;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Packager.Cbs;
using Dwapi.UploadManagement.Core.Profiles;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader;
using Dwapi.UploadManagement.Infrastructure.Reader.Cbs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Packager.Cbs
{
    [TestFixture]
    [Category("Cbs")]
    public class CbsPackagerTests
    {
        private IServiceProvider _serviceProvider;
        private ICbsPackager _packager;

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
                .AddTransient<ICbsExtractReader, CbsExtractReader>()
                .AddTransient<IEmrMetricReader, EmrMetricReader>()
                .AddTransient<ICbsPackager, CbsPackager>()
                .BuildServiceProvider();

            Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile<MasterPatientIndexProfile>();
                }
            );
        }


        [SetUp]
        public void SetUp()
        {
            _packager = _serviceProvider.GetService<ICbsPackager>();
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
            Assert.True(m.Cargoes.Any(x=>x.Type==CargoType.AppMetrics));
            Console.WriteLine($"{m} |{m.Cargoes.First().Type} < {m.Cargoes.First(x=>x.Type==CargoType.Patient).Items} ");
            Console.WriteLine($"{m} |{m.Cargoes.First().Type} < {m.Cargoes.First(x=>x.Type==CargoType.Metrics).Items} ");
            Console.WriteLine($"{m} |{m.Cargoes.First().Type} < {m.Cargoes.First(x=>x.Type==CargoType.AppMetrics).Items} ");
        }

        [Test]
        public void should_Generate_MpiDtos()
        {
            var manfiests = _packager.GenerateDtoMpi().ToList();
            Assert.False(manfiests.Any(x=>!string.IsNullOrWhiteSpace(x.FirstName_Normalized)));
            var m = manfiests.First();
            Console.WriteLine($"{m} |{m.FirstName} ");
        }
    }
}
