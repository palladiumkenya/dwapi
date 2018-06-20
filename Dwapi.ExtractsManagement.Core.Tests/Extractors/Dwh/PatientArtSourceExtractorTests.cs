using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Extractors.Dwh
{
    [TestFixture]
    public class PatientArtSourceExtractorTests
    {
        private ExtractsContext _extractsContext, _extractsContextMySql;
        private DbProtocol _iQtoolsDb, _kenyaEmrDb;
        [OneTimeSetUp]
        public void Init()
        {
            var extractIds = TestInitializer.Iqtools.Extracts.Where(x => x.DocketId.IsSameAs("NDWH")).Select(x => x.Id)
                .ToList();
            var cleaner = TestInitializer.ServiceProvider.GetService<IClearDwhExtracts>();
            cleaner.Clear(extractIds);

            var extractIdsMySql = TestInitializer.KenyaEmr.Extracts.Where(x => x.DocketId.IsSameAs("NDWH")).Select(x => x.Id)
                .ToList();
            var cleanerMySql = TestInitializer.ServiceProviderMysql.GetService<IClearDwhExtracts>();
            cleanerMySql.Clear(extractIdsMySql);

            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            _extractsContextMySql = TestInitializer.ServiceProviderMysql.GetService<ExtractsContext>();

            _iQtoolsDb = TestInitializer.Iqtools.DatabaseProtocols.First(x => x.DatabaseName.ToLower().Contains("iqtools".ToLower()));
            _iQtoolsDb.Host = ".\\Koske14";
            _iQtoolsDb.Username = "sa";
            _iQtoolsDb.Password = "maun";

            _kenyaEmrDb = TestInitializer.KenyaEmr.DatabaseProtocols.First();
            _kenyaEmrDb.Host = "127.0.0.1";
            _kenyaEmrDb.Username = "root";
            _kenyaEmrDb.Password = "test";
            _kenyaEmrDb.DatabaseName = "openmrs";
        }

        [Test]
        public void should_Exract_From_Reader_MsSql()
        {
            Assert.False(_extractsContext.TempPatientArtExtracts.Any());

            var extract = TestInitializer.Iqtools.Extracts.First(x => x.Name.IsSameAs(nameof(PatientArtExtract)));

            var extractor = TestInitializer.ServiceProvider.GetService<IPatientArtSourceExtractor>();

            var recordcount = extractor.Extract(extract, _iQtoolsDb).Result;
            Assert.True(_extractsContext.TempPatientArtExtracts.Any());
            Console.WriteLine($"extracted {_extractsContext.TempPatientArtExtracts.Count()}");
        }

        [Test]
        public void should_Exract_From_Reader_MySql()
        {
            
            Assert.False(_extractsContextMySql.TempPatientArtExtracts.ToList().Count>0);

            var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.Name.IsSameAs(nameof(PatientArtExtract)));

            var extractor = TestInitializer.ServiceProviderMysql.GetService<IPatientArtSourceExtractor>();

            var recordcount = extractor.Extract(extract, _kenyaEmrDb).Result;
            Assert.True(_extractsContextMySql.TempPatientArtExtracts.Any());
            Console.WriteLine($"extracted {_extractsContextMySql.TempPatientArtExtracts.Count()}");
        }
    }
}