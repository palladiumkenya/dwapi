using System;
using System.Data.SqlClient;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Reader.Csb
{
    [TestFixture]
    public class MasterPatientIndexReaderTests
    {
        private IMasterPatientIndexReader _reader;
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
                .BuildServiceProvider();


            _settingsContext = _serviceProvider.GetService<SettingsContext>();
            _extractsContext = _serviceProvider.GetService<ExtractsContext>();
        }


        [SetUp]
        public void SetUp()
        {
            _emrSystem = _settingsContext.EmrSystems.First(x => x.IsDefault);
            _protocol = _settingsContext.DatabaseProtocols.First(x => x.DatabaseName.ToLower().Contains("iqtools")&&x.EmrSystemId==_emrSystem.Id);
            _extract = _settingsContext.Extracts.First(x => x.EmrSystemId == _emrSystem.Id&&x.DocketId=="CBS");
            _reader = _serviceProvider.GetService<IMasterPatientIndexReader>();
        }

        [Test]
        public void should_Execute_Reade()
        {
            var reader = _reader.ExecuteReader(_protocol, _extract).Result;
            reader.Read();
            var row = reader[0].ToString();
            reader.Close();
            Assert.False(string.IsNullOrWhiteSpace(row));
            Console.WriteLine(reader[$"{nameof(TempMasterPatientIndex.FirstName)}"]);
        }
    }
}