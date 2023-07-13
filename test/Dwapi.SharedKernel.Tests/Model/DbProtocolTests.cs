using System;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using NUnit.Framework;

namespace Dwapi.SharedKernel.Tests.Model
{
    [TestFixture]
    public class DbProtocolTests
    {
        private DbProtocol _iqcaredbProtocol;
        [SetUp]
        public void SetUp()
        {
            _iqcaredbProtocol=new DbProtocol(DatabaseType.MicrosoftSQL,@".\koske14","sa","maun","iqcare", Guid.Parse(""));
        }

        [Test]
        public void should_GetConnectionString()
        {
            Assert.False(string.IsNullOrWhiteSpace(_iqcaredbProtocol.GetConnectionString()));
            Console.WriteLine(_iqcaredbProtocol);
        }
    }
}