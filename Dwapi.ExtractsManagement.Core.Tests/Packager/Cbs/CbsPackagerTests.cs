using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Data;
using Dwapi.ExtractsManagement.Core.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.ComandHandlers.Cbs;
using Dwapi.ExtractsManagement.Core.Extractors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Packager;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs;
using Dwapi.ExtractsManagement.Core.Loader.Cbs;
using Dwapi.ExtractsManagement.Core.Packager.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Cbs;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Packager.Cbs
{
    [TestFixture]
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
                .AddDbContext<ExtractsContext>(o => o.UseSqlServer(connectionString))
                .AddTransient<IMasterPatientIndexRepository, MasterPatientIndexRepository>()
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