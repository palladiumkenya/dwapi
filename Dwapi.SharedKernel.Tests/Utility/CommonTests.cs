using System;
using Dwapi.SharedKernel.Utility;
using NUnit.Framework;

namespace Dwapi.SharedKernel.Tests.Utility
{
    [TestFixture]
    public class CommonTests
    {
        [Test]
        public void should_GetProgess_Share()
        {
            //  60 %

            for (int i = 0; i <=6; ++i)
            {
                var pc60= Common.GetProgress(6, i,60);
                //Assert.True(pc60<61);
                Console.WriteLine(pc60);
            }

            //  80 %
            for (int i = 0; i <= 2; ++i)
            {
                var pc80 = Common.GetProgress(2, i, 80);

               // Assert.True(pc80 > 60);
                //Assert.True(pc80 < 81);
                Console.WriteLine(pc80);
            }


            // 100 %
            for (int i = 0; i <= 2; ++i)
            {
                var pc100 = Common.GetProgress(2, i, 100);
             //   Assert.True(pc100 > 80);
              //  Assert.True(pc100 < 101);
                Console.WriteLine(pc100);
            }
        }
    }
}

