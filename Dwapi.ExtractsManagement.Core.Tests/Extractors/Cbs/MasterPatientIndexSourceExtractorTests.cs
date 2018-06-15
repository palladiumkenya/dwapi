using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Extractors.Cbs
{
    [TestFixture]
    public class MasterPatientIndexSourceExtractorTests
    {
        private ExtractsContext _extractsContext,_extractsContextMySql;
        private DbProtocol _iQtoolsDb, _kenyaEmrDb;
        [OneTimeSetUp]
        public void Init()
        {
            var extractId = TestInitializer.Iqtools.Extracts.First(x => x.Name.IsSameAs(nameof(MasterPatientIndex))).Id;
            var cleaner = TestInitializer.ServiceProvider.GetService<ICleanCbsExtracts>();
            cleaner.Clean(extractId);

            var extractIdMySql = TestInitializer.KenyaEmr.Extracts.First(x => x.Name.IsSameAs(nameof(MasterPatientIndex))).Id;
            var cleanerMySql = TestInitializer.ServiceProviderMysql.GetService<ICleanCbsExtracts>();
            cleanerMySql.Clean(extractIdMySql);

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
            Assert.False(_extractsContext.TempMasterPatientIndices.Any());
            var extract = TestInitializer.Iqtools.Extracts.First(x => x.DocketId.IsSameAs("CBS"));

            var extractor = TestInitializer.ServiceProvider.GetService<IMasterPatientIndexSourceExtractor>();

            var recordcount=extractor.Extract(extract, _iQtoolsDb).Result;
            Assert.True(_extractsContext.TempMasterPatientIndices.Any());
            Console.WriteLine($"extracted {_extractsContext.TempMasterPatientIndices.Count()}");
        }

        [Test]
        public void should_Exract_From_Reader_MySql()
        {
            Assert.False(_extractsContextMySql.TempMasterPatientIndices.Any());
            var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.DocketId.IsSameAs("CBS"));

            var extractor = TestInitializer.ServiceProviderMysql.GetService<IMasterPatientIndexSourceExtractor>();

            var recordcount = extractor.Extract(extract, _kenyaEmrDb).Result;
            Assert.True(_extractsContextMySql.TempMasterPatientIndices.Any());
            Console.WriteLine($"extracted {_extractsContextMySql.TempMasterPatientIndices.Count()}");
        }
    }
}