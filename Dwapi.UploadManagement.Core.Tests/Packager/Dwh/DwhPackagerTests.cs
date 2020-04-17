using System;
using System.Linq;
using CsvHelper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Packager.Dwh;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader;
using Dwapi.UploadManagement.Infrastructure.Reader.Cbs;
using Dwapi.UploadManagement.Infrastructure.Reader.Dwh;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Packager.Dwh
{
    [TestFixture]
    [Category("Dwh")]
    public class DwhPackagerTests
    {
        private IDwhPackager _packager;
        private Guid _pid;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
        }


        [SetUp]
        public void SetUp()
        {
            _packager = TestInitializer.ServiceProvider.GetService<IDwhPackager>();

        }

        [Test]
        public void should_Generate_Manifest()
        {
            var manfiests = _packager.Generate(EmrSetup.SingleFacility).ToList();
            Assert.True(manfiests.Any());
            Assert.True(manfiests.Count==1);
            var m = manfiests.First();
            Assert.True(m.PatientPks.Any());
            Console.WriteLine($"{m}");
        }

        [Test]
        public void should_Generate_Multi_Manifest()
        {
            var manfiests = _packager.Generate(EmrSetup.MultiFacility).ToList();
            Assert.True(manfiests.Any());
            Assert.True(manfiests.Count>1);
            foreach (var m in manfiests)
            {
                Assert.True(m.PatientPks.Any());
                Console.WriteLine($"{m}");
            }

        }

        [Test]
        public void should_Generate_Manifest_With_Metrics()
        {
            var manfiests = _packager.GenerateWithMetrics(EmrSetup.SingleFacility).ToList();
            Assert.True(manfiests.Any());
            var m = manfiests.First();
            Assert.True(m.PatientPks.Any());
            Assert.False(string.IsNullOrWhiteSpace(m.Metrics));
            Console.WriteLine($"{m}");
            Console.WriteLine(m.Metrics);
        }

        [Test]
        public void should_Generate_Extracts()
        {
            var manfiests = _packager.GenerateExtracts(_pid);
            Assert.NotNull(manfiests);
            Assert.True(manfiests.PatientArtExtracts.Any());
        }
    }
}
