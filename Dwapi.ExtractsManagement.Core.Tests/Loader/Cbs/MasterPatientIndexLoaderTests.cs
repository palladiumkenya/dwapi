using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Data;
using Dwapi.ExtractsManagement.Core.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.ComandHandlers.Cbs;
using Dwapi.ExtractsManagement.Core.Extractors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs;
using Dwapi.ExtractsManagement.Core.Loader.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Cbs;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using FizzWare.NBuilder;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Tests.Loader.Cbs
{
    [TestFixture]
    public class MasterPatientIndexLoaderTests
    {
        private IMasterPatientIndexLoader _loader;
        private IMasterPatientIndexSourceExtractor _extractor;
        private List<Extract> _extracts;
        private DbProtocol _protocol;
        private ExtractsContext _extractsContext;
        private Extract _extract;
        private int _count;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrConnectionString));
            _protocol = TestInitializer.Protocol;
            _extracts = TestInitializer.Extracts.Where(x => x.DocketId.IsSameAs("CBS")).ToList();
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [SetUp]
        public void SetUp()
        {
            _loader = TestInitializer.ServiceProvider.GetService<IMasterPatientIndexLoader>();
            _extractor = TestInitializer.ServiceProvider.GetService<IMasterPatientIndexSourceExtractor>();
            _extract = _extracts.First(x => x.Name.IsSameAs(nameof(MasterPatientIndex)));
            _count = _extractor.Extract(_extract, _protocol).Result;

        }

        [Test]
        public void should_Load()
        {
            Assert.True(_count > 0);
            Assert.False(_extractsContext.MasterPatientIndices.Any());

            var count = _loader.Load(_extract.Id,_count).Result;

            Assert.True(count > 0);
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            Assert.AreEqual(count,_extractsContext.MasterPatientIndices.Count());
            Log.Debug($"Temp {_count} Main {_extractsContext.MasterPatientIndices.Count()}");
        }
    }
}
