using Dwapi.UploadManagement.Core.Packager;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Packager
{
    [TestFixture]
    public class PackagePagerTests
    {
        private PackagePager _packager;

        [SetUp]
        public void SetUp()
        {
            _packager=new PackagePager();
        }

        [Test]
        public void Should_Get_PageCount()
        {
            Assert.AreEqual(5, _packager.PageCount(2, 10));
            Assert.AreEqual(4, _packager.PageCount(3, 10));
            Assert.AreEqual(1, _packager.PageCount(3, 2));
            Assert.AreEqual(0, _packager.PageCount(3, 0));

        }
    }
}
