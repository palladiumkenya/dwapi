using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Data;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Dwh;
using Dwapi.ExtractsManagement.Core.Profiles.Hts;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Profiles;
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
            var manifests = _packager.Generate(TestInitializer.IqEmrDto).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count==1);
            var m = manifests.First();
            Assert.True(m.Cargoes.Any());
            Log.Debug($"{m}");
        }

        [Test]
        public void should_Generate_Multi_Manifest()
        {
            var manifests = _packager.Generate(TestInitializer.IqEmrMultiDto).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count>1);
            foreach (var m in manifests)
            {
                Assert.True(m.Cargoes.Any());
                Log.Debug($"{m}");
            }
        }

        [Test]
        public void should_Generate_Comm_Manifest()
        {
            var manifests = _packager.Generate(TestInitializer.KeEmrCommDto).ToList();
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
            var manifests = _packager.GenerateWithMetrics(TestInitializer.IqEmrDto).ToList();
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
            var emrDto = TestInitializer.IqEmrMultiDto;
            var manfiests = _packager.GenerateWithMetrics(emrDto).ToList();
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
        public void should_Generate_Comm_Manifest_With_Metrics()
        {
            var emrDto = TestInitializer.KeEmrCommDto;
            var manfiests = _packager.GenerateWithMetrics(emrDto).ToList();
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

        [Test]
        public void should_Generate_MpiDecodedDtos()
        {
            Allow();
            var manfiests = _packager.GenerateDtoMpi().ToList();
            Assert.False(manfiests.Any(x => string.IsNullOrWhiteSpace(x.FirstName_Normalized)));
            var m = manfiests.First();
            Log.Debug($"{m} |{m.FirstName} ");
            Reset();
        }

        private void Allow()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
                {
                    cfg.AddDataReaderMapping();
                    cfg.AddProfile<DiffCtExtractProfile>();
                    cfg.AddProfile<TempMasterPatientIndexProfile>();
                    cfg.AddProfile<EmrProfiles>();
                    cfg.AddProfile<TempHtsExtractProfile>();
                    cfg.AddProfile<MasterPatientIndexProfileResearch>();
                }
            );
        }

        private void Reset()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
                {
                    cfg.AddDataReaderMapping();
                    cfg.AddProfile<DiffCtExtractProfile>();
                    cfg.AddProfile<TempMasterPatientIndexProfile>();
                    cfg.AddProfile<EmrProfiles>();
                    cfg.AddProfile<TempHtsExtractProfile>();
                    cfg.AddProfile<MasterPatientIndexProfile>();
                }
            );
        }
    }
}
