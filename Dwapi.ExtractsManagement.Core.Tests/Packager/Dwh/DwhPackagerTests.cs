using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Packager.Cbs;
using Dwapi.ExtractsManagement.Core.Packager.Dwh;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Packager.Dwh
{
    [TestFixture]
    public class DwhPackagerTests
    {
        private IServiceProvider _serviceProvider;
        private IDwhPackager _packager;
        
        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DwapiConnection"];

            _serviceProvider = new ServiceCollection()
                .AddDbContext<ExtractsContext>(o => o.UseSqlServer(connectionString))
                .AddTransient<IPatientExtractRepository, PatientExtractRepository>()
                .AddTransient<IDwhPackager, DwhPackager>()
                .BuildServiceProvider();
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
    }
}