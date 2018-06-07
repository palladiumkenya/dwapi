using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SharedKernel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Reader.Dwh
{
    [TestFixture]
    public class PatientSourceReaderTests
    {
        private IExtractSourceReader _reader;
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
            var connectionString = config["ConnectionStrings:DwapiConnectionDevData"];

            _serviceProvider = new ServiceCollection()
                .AddDbContext<Dwapi.SettingsManagement.Infrastructure.SettingsContext>(o => o.UseSqlServer(connectionString))
                .AddDbContext<ExtractsContext>(o => o.UseSqlServer(connectionString))
                .AddTransient<IPatientExtractRepository, PatientExtractRepository>()
                .AddTransient<ITempPatientExtractRepository, TempPatientExtractRepository>()
                .AddTransient<IExtractSourceReader, ExtractSourceReader>()
                .BuildServiceProvider();


            _settingsContext = _serviceProvider.GetService<SettingsContext>();
            _settingsContext.Database.EnsureDeleted();
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
            _extract = _settingsContext.Extracts.First(x => x.EmrSystemId == _emrSystem.Id && x.DocketId == "NDWH");
            _reader = _serviceProvider.GetService<IExtractSourceReader>();
        }



        [Test]
        public void should_Execute_Reader()
        {
            var reader = _reader.ExecuteReader(_protocol, _extract).Result;
            reader.Read();
            var row = reader[0].ToString();
            reader.Close();
            Assert.False(string.IsNullOrWhiteSpace(row));
        }
    }
}