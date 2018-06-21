using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Packager.Cbs;
using Dwapi.UploadManagement.Infrastructure.Data;
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
                .AddTransient<ICbsPackager, CbsPackager>()
                .BuildServiceProvider();
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
    }
}