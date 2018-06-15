using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Reader.Csb
{
    [TestFixture]
    public class MasterPatientIndexReaderTests
    {
        private SettingsContext _settingsContext;
        private SettingsContext _settingsContextMysql;
        private DbProtocol _iQtoolsDb, _kenyaEmrDb;
        
        private IMasterPatientIndexReader _reader;

        [OneTimeSetUp]
        public void Init()
        {
            _settingsContext = TestInitializer.ServiceProvider.GetService<SettingsContext>();
            _settingsContextMysql = TestInitializer.ServiceProviderMysql.GetService<SettingsContext>();

            _iQtoolsDb = TestInitializer.Iqtools.DatabaseProtocols.First(x=>x.DatabaseName.ToLower().Contains("iqtools".ToLower()));
            _iQtoolsDb.Host = ".\\Koske14";
            _iQtoolsDb.Username = "sa";
            _iQtoolsDb.Password = "maun";
            
            _kenyaEmrDb = TestInitializer.KenyaEmr.DatabaseProtocols.First();
            _kenyaEmrDb.Host = "192.168.100.99";
            _kenyaEmrDb.Username = "root";
            _kenyaEmrDb.Password = "root";
            _kenyaEmrDb.DatabaseName = "openmrs";

        }


       [Test]
        public void should_Execute_Reader_MsSql()
       {
           var extract = TestInitializer.Iqtools.Extracts.First(x => x.DocketId.IsSameAs("CBS"));

            _reader = TestInitializer.ServiceProvider.GetService<IMasterPatientIndexReader>();
            var reader = _reader.ExecuteReader(_iQtoolsDb, extract).Result;
            reader.Read();
            var row = reader[0].ToString();
            Assert.False(string.IsNullOrWhiteSpace(row));
            Console.WriteLine(reader[$"{nameof(TempMasterPatientIndex.FirstName)}"]);
           reader.Close();
        }

        [Test]
        public void should_Execute_Reader_MySql()
        {
            var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.DocketId.IsSameAs("CBS"));

            _reader = TestInitializer.ServiceProviderMysql.GetService<IMasterPatientIndexReader>();
            var reader = _reader.ExecuteReader(_kenyaEmrDb, extract).Result;
            reader.Read();
            var row = reader[0].ToString();
         
            Assert.False(string.IsNullOrWhiteSpace(row));
            Console.WriteLine(reader[$"{nameof(TempMasterPatientIndex.FirstName)}"]);

            reader.Close();
        }   
    }
}