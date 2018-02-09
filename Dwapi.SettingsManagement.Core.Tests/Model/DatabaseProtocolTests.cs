using System;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Core.Tests.Model
{
    [TestFixture]
    public class DatabaseProtocolTests
    {
        private DatabaseProtocol _iqcaredbProtocol;
        [SetUp]
        public void SetUp()
        {
            _iqcaredbProtocol=new DatabaseProtocol(DatabaseType.MicrosoftSQL,@".\koske14","sa","maun","iqcare");
        }

        [Test]
        public void should_GetConnectionString()
        {
            Assert.False(string.IsNullOrWhiteSpace(_iqcaredbProtocol.GetConnectionString()));
            Console.WriteLine(_iqcaredbProtocol);
        }
    }
}