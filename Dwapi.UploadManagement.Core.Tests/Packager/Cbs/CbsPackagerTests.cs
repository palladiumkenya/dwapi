using System;
using System.Linq;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Packager.Cbs;
using Dwapi.UploadManagement.Core.Profiles;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader;
using Dwapi.UploadManagement.Infrastructure.Reader.Cbs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Core.Tests.Packager.Cbs
{
    [TestFixture]
    [Category("Cbs")]
    public class CbsPackagerTests
    {
        private ICbsPackager _packager;

        [SetUp]
        public void Init()
        {
            _packager = TestInitializer.ServiceProvider.GetService<ICbsPackager>();
        }

        [Test]
        public void should_Generate_Manifest()
        {
            var manifests = _packager.Generate(EmrSetup.SingleFacility).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count==1);
            var m = manifests.First();
            Assert.True(m.Cargoes.Any());
            Log.Debug($"{m}");
        }

        [Test]
        public void should_Generate_Multi_Manifest()
        {
            var manifests = _packager.Generate(EmrSetup.MultiFacility).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count>1);
            foreach (var m in manifests)
            {
                Assert.True(m.Cargoes.Any());
                Log.Debug($"{m}");
            }
        }

        [Test]
        public void should_Generate_Manifest_With_Metrics()
        {
            var manifests = _packager.GenerateWithMetrics(EmrSetup.SingleFacility).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count == 1);
            var m = manifests.First();
            Assert.True(m.Cargoes.Any(x => x.Type == CargoType.Patient));
            Assert.True(m.Cargoes.Any(x => x.Type == CargoType.Metrics));
            Assert.True(m.Cargoes.Any(x => x.Type == CargoType.AppMetrics));
            Log.Debug($"{m}");
            m.Cargoes.ForEach(c =>
            {
                Log.Debug($"{c.Type}");
                Log.Debug($"     {c.Items} ");
            });
        }

        [Test]
        public void should_Generate_Multi_Manifest_With_Metrics()
        {
            var manfiests = _packager.GenerateWithMetrics(EmrSetup.MultiFacility).ToList();
            Assert.True(manfiests.Any());
            Assert.True(manfiests.Count>1);
            foreach (var m in manfiests)
            {
                Assert.True(m.Cargoes.Any(x => x.Type == CargoType.Patient));
                Assert.True(m.Cargoes.Any(x => x.Type == CargoType.Metrics));
                Assert.True(m.Cargoes.Any(x => x.Type == CargoType.AppMetrics));
                Log.Debug($"{m}");
                m.Cargoes.ForEach(c =>
                {
                    Log.Debug($"{c.Type}");
                    Log.Debug($"     {c.Items} ");
                });
            }
        }

        [Test]
        public void should_Generate_MpiDtos()
        {
            var manfiests = _packager.GenerateDtoMpi().ToList();
            Assert.False(manfiests.Any(x=>!string.IsNullOrWhiteSpace(x.FirstName_Normalized)));
            var m = manfiests.First();
            Console.WriteLine($"{m} |{m.FirstName} ");
        }
    }
}
