﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Tests.Extractors.Hts
{
    [TestFixture]
    public class HtsClientsLinkageSourceExtractorTests
    {
        private IHtsClientsLinkageSourceExtractor _extractor;
        private List<Extract> _extracts;
        private DbProtocol _protocol;
        private ExtractsContext _extractsContext;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrConnectionString));
            _protocol = TestInitializer.Protocol;
            _extracts = TestInitializer.Extracts.Where(x => x.DocketId.IsSameAs("HTS")).ToList();
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [SetUp]
        public void SetUp()
        {
            _extractor = TestInitializer.ServiceProvider.GetService<IHtsClientsLinkageSourceExtractor>();
        }

        [TestCase(nameof(HtsClientLinkage))]
        public void should_Extract(string name)
        {
            Assert.False(_extractsContext.TempHtsClientsLinkageExtracts.Any());
            var extract = _extracts.First(x => x.Name.IsSameAs(name));
            var count = _extractor.Extract(extract, _protocol).Result;
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            Assert.AreEqual(count,_extractsContext.TempHtsClientsLinkageExtracts.Count());
            Log.Debug($"extracted {_extractsContext.TempHtsClientsLinkageExtracts.Count()}");
        }
    }
}
