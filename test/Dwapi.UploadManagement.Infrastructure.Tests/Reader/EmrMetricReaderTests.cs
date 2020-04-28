using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Infrastructure.Tests.Reader
{
    [TestFixture]
    public class EmrMetricReaderTests
    {
        private IEmrMetricReader _reader;

        [SetUp]
        public void SetUp()
        {
            _reader =TestInitializer.ServiceProvider.GetService<IEmrMetricReader>();
        }

        [Test]
        public void should_Read_Emr()
        {
            var profiles = _reader.ReadAll().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_Read_App()
        {
            var profiles = _reader.ReadAppAll().ToList();
            Assert.True(profiles.Any());
        }

    }
}
