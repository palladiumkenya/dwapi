using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Extractors.Hts
{
    [TestFixture]
    public class HTSClientLinkageSourceExtractorTests
    {
        private ExtractsContext _extractsContext,_extractsContextMySql;
        private DbProtocol _iQtoolsDb, _kenyaEmrDb;

        [SetUp]
        public void SetUp()
        {
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            _extractsContextMySql = TestInitializer.ServiceProviderMysql.GetService<ExtractsContext>();
            _iQtoolsDb = TestInitializer.IQtoolsDbProtocol;
            _kenyaEmrDb = TestInitializer.KenyaEmrDbProtocol;
        }
        [Test]
        public void should_Exract_From_Reader_MsSql()
        {
            Assert.False(_extractsContext.TempHtsClientLinkageExtracts.Any());
            var extract = TestInitializer.Iqtools.Extracts.First(x => x.Name.IsSameAs(nameof(HTSClientLinkageExtract)));

            var extractor = TestInitializer.ServiceProvider.GetService<IHTSClientLinkageSourceExtractor>();

            var recordcount=extractor.Extract(extract, _iQtoolsDb).Result;
            Assert.True(_extractsContext.TempHtsClientLinkageExtracts.Any());
            Console.WriteLine($"extracted {_extractsContext.TempHtsClientLinkageExtracts.Count()}");
        }

        [Test]
        public void should_Exract_From_Reader_MySql()
        {
            Assert.False(_extractsContextMySql.TempHtsClientLinkageExtracts.Any());
            var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.Name.IsSameAs(nameof(HTSClientLinkageExtract)));

            var extractor = TestInitializer.ServiceProviderMysql.GetService<IHTSClientLinkageSourceExtractor>();

            var recordcount = extractor.Extract(extract, _kenyaEmrDb).Result;
            Assert.True(_extractsContextMySql.TempHtsClientLinkageExtracts.Any());
            Console.WriteLine($"extracted {_extractsContextMySql.TempHtsClientLinkageExtracts.Count()}");
        }
    }
}
