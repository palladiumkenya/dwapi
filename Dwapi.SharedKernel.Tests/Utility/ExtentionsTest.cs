using System;
using Dwapi.SharedKernel.Utility;
using NUnit.Framework;

namespace Dwapi.SharedKernel.Tests.Utility
{
    [TestFixture]
    public class ExtentionsTests
    {
        [Test]
        public void should_check_null_guids()
        {
            Guid blank;
            Assert.IsTrue(blank.IsNullOrEmpty());

             var blank2=new Guid();
            Assert.IsTrue(blank2.IsNullOrEmpty());

            var blank3 = Guid.Empty;
            Assert.IsTrue(blank3.IsNullOrEmpty());

            var blank4 = LiveGuid.NewGuid();
            Assert.IsFalse(blank4.IsNullOrEmpty());

            Guid? blank5=null;
            Assert.IsTrue(blank5.IsNullOrEmpty());

            blank5 = new Guid();
            Assert.IsTrue(blank5.IsNullOrEmpty());

            blank5 = Guid.Empty;
            Assert.IsTrue(blank5.IsNullOrEmpty());

            blank5 = LiveGuid.NewGuid();
            Assert.IsFalse(blank5.IsNullOrEmpty());
        }
        
    }
}