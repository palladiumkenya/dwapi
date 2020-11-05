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


        [Test]
        public void should_check_null_dates()
        {
            DateTime? blank2 = null;
            Assert.IsTrue(blank2.IsNullOrEmpty());


            DateTime? blank3 = blank2.GetValueOrDefault();

            Assert.IsTrue(blank3.IsNullOrEmpty());

            DateTime? blank4 = DateTime.Now;

            Assert.IsFalse(blank4.IsNullOrEmpty());
        }

        [Test]
        public void should_cast_dynamic()
        {
            dynamic dateItem = "2020-11-01 22:03:41";
            DateTime? date = Extentions.CastDateTime(dateItem);
            Assert.False(date.IsNullOrEmpty());

            dynamic dateItem2 = "XXX0-11-01 22:03:41";
            DateTime? date2 = Extentions.CastDateTime(dateItem2);
            Assert.True(date2.IsNullOrEmpty());

            dynamic dateItem3 = string.Empty;
            DateTime? date3 = Extentions.CastDateTime(dateItem3);
            Assert.True(date3.IsNullOrEmpty());
        }

    }
}
