using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Infrastructure.Tests.Reader
{
    [TestFixture]
    public class DiffLogReaderTests
    {
        private IDiffLogReader _reader;

        [SetUp]
        public void SetUp()
        {
            _reader =TestInitializer.ServiceProvider.GetService<IDiffLogReader>();
        }

        [Test]
        public void should_Read_All()
        {
            var profiles = _reader.ReadAll().ToList();
            Assert.True(profiles.Any());
        }
    }
}
