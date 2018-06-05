using System;
using System.Linq;
using System.Configuration;
using AutoMapper;
using AutoMapper.Data;
using Dwapi.ExtractsManagement.Core.ComandHandlers.Cbs;
using Dwapi.ExtractsManagement.Core.Extractors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs;
using Dwapi.ExtractsManagement.Core.Loader.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Dwh;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Cbs;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Extractors.Cbs
{
    [TestFixture]
    public class MasterPatientIndexSourceExtractorTests
    {
        private IMasterPatientIndexSourceExtractor _extractor;
        private IServiceProvider _serviceProvider;
        private Dwapi.SettingsManagement.Infrastructure.SettingsContext _settingsContext;
        private ExtractsContext _extractsContext;
        private DbProtocol _protocol;
        private DbExtract _extract;
        private EmrSystem _emrSystem;

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
                .AddTransient<IMasterPatientIndexReader, MasterPatientIndexReader>()
                .AddTransient<IMasterPatientIndexSourceExtractor, MasterPatientIndexSourceExtractor>()
                .AddTransient<IMasterPatientIndexValidator, MasterPatientIndexValidator>()
                .AddTransient<IMasterPatientIndexLoader, MasterPatientIndexLoader>()
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
        }

        [Test]
        public void should_Exract_From_Reader()
        {
            var recordcount=_extractor.Extract(_extract, _protocol).Result;
            Assert.True(recordcount>0);
            Assert.True(_extractsContext.TempMasterPatientIndices.Any());
            Console.WriteLine($"extracted {recordcount}");
        }
    }
}