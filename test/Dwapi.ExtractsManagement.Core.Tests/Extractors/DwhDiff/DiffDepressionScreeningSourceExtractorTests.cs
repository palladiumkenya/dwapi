﻿using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Tests.Extractors.DwhDiff
{
    [TestFixture]
    public class DiffDepressionScreeningSourceExtractorTests
    {
        private IDepressionScreeningSourceExtractor _extractor;
        private List<Extract> _extracts;
        private DbProtocol _protocol;
        private ExtractsContext _extractsContext;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDiffDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrDiffConnectionString));
            _protocol = TestInitializer.Protocol;
            _extracts = TestInitializer.Extracts.Where(x => x.DocketId.IsSameAs("NDWH")).ToList();
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [SetUp]
        public void SetUp()
        {
            _extractor = TestInitializer.ServiceProvider.GetService<IDepressionScreeningSourceExtractor>();
        }

        [TestCase(nameof(DepressionScreeningExtract))]
        public void should_Extract(string name)
        {
            Assert.False(_extractsContext.TempDepressionScreeningExtracts.Any());
            var extract = _extracts.First(x => x.Name.IsSameAs(name));

            var count = _extractor.Extract(extract, _protocol).Result;

            Assert.True(count > 0);
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            Assert.AreEqual(count,_extractsContext.TempDepressionScreeningExtracts.Count());
            Assert.False(_extractsContext.TempDepressionScreeningExtracts.First().Date_Created.IsNullOrEmpty());
            Assert.False(_extractsContext.TempDepressionScreeningExtracts.First().Date_Last_Modified.IsNullOrEmpty());
            Log.Debug($"extracted {_extractsContext.TempDepressionScreeningExtracts.Count()}");
        }
    }
}
