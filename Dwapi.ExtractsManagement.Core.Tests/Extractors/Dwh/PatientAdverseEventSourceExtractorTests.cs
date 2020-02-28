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
    public class PatientAdverseEventSourceExtractorTests
    {
        private ExtractsContext _extractsContext, _extractsContextMySql;
        private DbProtocol _iQtoolsDb, _kenyaEmrDb;
        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.InitDb();
            TestInitializer.InitMysQLDb();

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

            _iQtoolsDb = TestInitializer.IQtoolsDbProtocol;

            _kenyaEmrDb = TestInitializer.KenyaEmrDbProtocol;
        }

        [Test]
        public void should_Exract_From_Reader_MsSql()
        {
            Assert.False(_extractsContext.TempPatientAdverseEventExtracts.Any());

            var extract = TestInitializer.Iqtools.Extracts.First(x => x.Name.IsSameAs(nameof(PatientAdverseEventExtract)));

            var extractor = TestInitializer.ServiceProvider.GetService<IPatientAdverseEventSourceExtractor>();

            var recordcount = extractor.Extract(extract, _iQtoolsDb).Result;
            Assert.True(_extractsContext.TempPatientAdverseEventExtracts.Any());
            Console.WriteLine($"extracted {_extractsContext.TempPatientAdverseEventExtracts.Count()}");
        }

        [Test]
        public void should_Exract_From_Reader_MySql()
        {

            Assert.False(_extractsContextMySql.TempPatientAdverseEventExtracts.ToList().Count>0);

            var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.Name.IsSameAs(nameof(PatientAdverseEventExtract)));

            var extractor = TestInitializer.ServiceProviderMysql.GetService<IPatientAdverseEventSourceExtractor>();

            var recordcount = extractor.Extract(extract, _kenyaEmrDb).Result;
            Assert.True(_extractsContextMySql.TempPatientAdverseEventExtracts.Any());
            Console.WriteLine($"extracted {_extractsContextMySql.TempPatientAdverseEventExtracts.Count()}");
        }
    }
}
