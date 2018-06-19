using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Reader.Dwh
{
    [TestFixture]
    public class ExtractSourceReaderTests
    {
        private SettingsContext _settingsContext;
        private SettingsContext _settingsContextMysql;
        private DbProtocol _iQtoolsDb, _kenyaEmrDb;

        private IExtractSourceReader _reader;

        [OneTimeSetUp]
        public void Init()
        {
            _settingsContext = TestInitializer.ServiceProvider.GetService<SettingsContext>();
            _settingsContextMysql = TestInitializer.ServiceProviderMysql.GetService<SettingsContext>();

            _iQtoolsDb = TestInitializer.Iqtools.DatabaseProtocols.First(x => x.DatabaseName.ToLower().Contains("iqtools".ToLower()));
            _iQtoolsDb.Host = ".\\Koske14";
            _iQtoolsDb.Username = "sa";
            _iQtoolsDb.Password = "maun";

            _kenyaEmrDb = TestInitializer.KenyaEmr.DatabaseProtocols.First();
            _kenyaEmrDb.Host = "127.0.0.1";
            _kenyaEmrDb.Username = "root";
            _kenyaEmrDb.Password = "root";
            _kenyaEmrDb.DatabaseName = "openmrs";
        }

        [TestCase(nameof(PatientExtract))]
        [TestCase(nameof(PatientArtExtract))]
      
        [TestCase(nameof(PatientPharmacyExtract))]
        [TestCase(nameof(PatientStatusExtract))]
        [TestCase(nameof(PatientVisitExtract))]
        [TestCase("PatientLabExtract")]
        [TestCase("PatientBaselineExtract")]
        public void should_Execute_Reader_MsSql(string extractName)
        {
            var extract = TestInitializer.Iqtools.Extracts.First(x => x.DocketId.IsSameAs("NDWH")&&x.Name.IsSameAs(extractName));

            _reader = TestInitializer.ServiceProvider.GetService<IExtractSourceReader>();
            var reader = _reader.ExecuteReader(_iQtoolsDb, extract).Result;
            reader.Read();
            var row = reader[0].ToString();
            Assert.False(string.IsNullOrWhiteSpace(row));
            Console.WriteLine(reader[$"{nameof(TempExtract.PatientPK)}"]);
            Console.WriteLine(reader[$"{nameof(TempExtract.SiteCode)}"]);
            Console.WriteLine(reader[$"{nameof(TempExtract.PatientID)}"]);
            reader.Close();
        }

  
        [TestCase(nameof(PatientExtract))]
        [TestCase(nameof(PatientArtExtract))]
        [TestCase(nameof(PatientPharmacyExtract))]
        [TestCase(nameof(PatientStatusExtract))]
        [TestCase(nameof(PatientVisitExtract))]
        [TestCase("PatientLabExtract")]
        [TestCase("PatientBaselineExtract")]
        public void should_Execute_Reader_MySql(string extractName)
        {
            var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.DocketId.IsSameAs("NDWH") && x.Name.IsSameAs(extractName));

            _reader = TestInitializer.ServiceProviderMysql.GetService<IExtractSourceReader>();
            var reader = _reader.ExecuteReader(_kenyaEmrDb, extract).Result;
            reader.Read();
            var row = reader[0].ToString();

            Assert.False(string.IsNullOrWhiteSpace(row));
            Console.WriteLine(reader[$"{nameof(TempMasterPatientIndex.FirstName)}"]);
            Console.WriteLine(reader[$"{nameof(TempExtract.PatientPK)}"]);
            Console.WriteLine(reader[$"{nameof(TempExtract.SiteCode)}"]);
            Console.WriteLine(reader[$"{nameof(TempExtract.PatientID)}"]);

            reader.Close();
        }
    }
}