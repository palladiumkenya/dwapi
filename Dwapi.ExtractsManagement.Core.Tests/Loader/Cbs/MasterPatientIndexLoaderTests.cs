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
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs;
using Dwapi.ExtractsManagement.Core.Loader.Cbs;
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

namespace Dwapi.ExtractsManagement.Core.Tests.Loader.Cbs
{
    [TestFixture]
    public class MasterPatientIndexLoaderTests
    {
        private IMasterPatientIndexSourceExtractor _extractor;
        private IMasterPatientIndexLoader _loader;
        private IServiceProvider _serviceProvider;
        private Dwapi.SettingsManagement.Infrastructure.SettingsContext _settingsContext;
        private ExtractsContext _extractsContext;
        private DbProtocol _protocol;
        private DbExtract _extract;
        private EmrSystem _emrSystem;
        private ICleanCbsExtracts _clearCbsExtracts;

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
                .AddTransient<IMasterPatientIndexRepository, MasterPatientIndexRepository>()
                .AddTransient<ITempMasterPatientIndexRepository, TempMasterPatientIndexRepository>()
                .AddTransient<IEmrSystemRepository, EmrSystemRepository>()
                .AddTransient<IMasterPatientIndexReader, MasterPatientIndexReader>()
                .AddTransient<IMasterPatientIndexSourceExtractor, MasterPatientIndexSourceExtractor>()
                .AddTransient<IMasterPatientIndexValidator, MasterPatientIndexValidator>()
                .AddTransient<IMasterPatientIndexLoader, MasterPatientIndexLoader>()
                .AddTransient<ICleanCbsExtracts, CleanCbsExtracts>()
                .AddMediatR(typeof(ExtractMasterPatientIndexHandler))
                .BuildServiceProvider();

            Mapper.Initialize(cfg =>
            {
                cfg.AddDataReaderMapping();
                cfg.AddProfile<TempMasterPatientIndexProfile>();
            }
            );


            _settingsContext = _serviceProvider.GetService<Dwapi.SettingsManagement.Infrastructure.SettingsContext>();
            _settingsContext.Database.Migrate();
            _settingsContext.EnsureSeeded();
            _extractsContext = _serviceProvider.GetService<ExtractsContext>();
            _extractsContext.Database.Migrate();
            _extractsContext.EnsureSeeded();
        }


        [SetUp]
        public void SetUp()
        {

            _emrSystem = _settingsContext.EmrSystems.First(x => x.IsDefault);
            _protocol = _settingsContext.DatabaseProtocols.First(x => x.DatabaseName.ToLower().Contains("iqtools") && x.EmrSystemId == _emrSystem.Id);
            _extract = _settingsContext.Extracts.First(x => x.EmrSystemId == _emrSystem.Id && x.DocketId == "CBS");
            _extractor = _serviceProvider.GetService<IMasterPatientIndexSourceExtractor>();
            _clearCbsExtracts = _serviceProvider.GetService<ICleanCbsExtracts>();
            _loader = _serviceProvider.GetService<IMasterPatientIndexLoader>();
        }

        [Test]
        public void should_Load_From_Temp()
        {
            _clearCbsExtracts.Clean(_extract.Id).Wait();
            Assert.False(_extractsContext.MasterPatientIndices.Any());
            var recordcount = _extractor.Extract(_extract, _protocol).Result;
           var loadCount=  _loader.Load(0).Result;
            Assert.True(_extractsContext.MasterPatientIndices.Any());
            Console.WriteLine($"extracted {_extractsContext.MasterPatientIndices.Count()}");
        }
    }
}