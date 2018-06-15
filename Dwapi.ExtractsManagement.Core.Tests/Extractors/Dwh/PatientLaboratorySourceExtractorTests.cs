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
    public class PatientLaboratorySourceExtractorTests
    {
        private ExtractsContext _extractsContext, _extractsContextMySql;
        private DbProtocol _iQtoolsDb, _kenyaEmrDb;
        [OneTimeSetUp]
        public void Init()
        {
            var extractIds = TestInitializer.Iqtools.Extracts.Where(x => Extentions.IsSameAs(x.DocketId, "NDWH")).Select(x => x.Id)
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
            _iQtoolsDb.Host = "192.168.100.99\\Koske14";
            _iQtoolsDb.Username = "sa";
            _iQtoolsDb.Password = "maun";

            _kenyaEmrDb = TestInitializer.KenyaEmr.DatabaseProtocols.First();
            _kenyaEmrDb.Host = "192.168.100.99";
            _kenyaEmrDb.Username = "root";
            _kenyaEmrDb.Password = "root";
            _kenyaEmrDb.DatabaseName = "openmrs";
        }

        [Test]
        public void should_Exract_From_Reader_MsSql()
        {
            Assert.False(_extractsContext.TempPatientExtracts.Any());

            var extract = TestInitializer.Iqtools.Extracts.First(x => x.Name.IsSameAs(nameof(PatientLaboratoryExtract)));

            var extractor = TestInitializer.ServiceProvider.GetService<IPatientLaboratorySourceExtractor>();

            var recordcount = extractor.Extract(extract, _iQtoolsDb).Result;
            Assert.True(_extractsContext.TempPatientExtracts.Any());
            Console.WriteLine($"extracted {_extractsContext.TempPatientExtracts.Count()}");
        }

        [Test]
        public void should_Exract_From_Reader_MySql()
        {
            Assert.False(_extractsContextMySql.TempPatientExtracts.ToList().Any());

            var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.Name.IsSameAs(nameof(PatientLaboratoryExtract)));

            var extractor = TestInitializer.ServiceProviderMysql.GetService<IPatientLaboratorySourceExtractor>();

            var recordcount = extractor.Extract(extract, _kenyaEmrDb).Result;
            Assert.True(_extractsContextMySql.TempPatientExtracts.Any());
            Console.WriteLine($"extracted {_extractsContextMySql.TempPatientExtracts.Count()}");
        }
    }
}