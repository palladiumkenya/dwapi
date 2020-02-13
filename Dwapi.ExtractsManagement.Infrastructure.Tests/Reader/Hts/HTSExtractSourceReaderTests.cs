using System.Data.SqlClient;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Reader.Hts
{
    [TestFixture]
    [Category("Dwh")]
    public class HTSExtractSourceReaderTests
    {

        private IHTSExtractSourceReader _reader;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.InitDb();
            TestInitializer.InitMysQLDb();
        }

        [TestCase(nameof(HTSClientExtract))]
        [TestCase(nameof(HTSClientLinkageExtract))]
        [TestCase(nameof(HTSClientPartnerExtract))]

        [TestCase(nameof(HtsClients))]
        [TestCase(nameof(HtsClientTests))]
        [TestCase(nameof(HtsClientTracing))]
        [TestCase(nameof(HtsPartnerTracing))]
        [TestCase(nameof(HtsPartnerNotificationServices))]
        [TestCase(nameof(HtsClientTests))]
        [TestCase(nameof(HtsClientLinkage))]

        public void should_Execute_Reader_MsSql(string extractName)
        {
            var extract =
                TestInitializer.Iqtools.Extracts.First(x =>
                    x.DocketId.IsSameAs("HTS") && x.Name.IsSameAs(extractName));
            _reader = TestInitializer.ServiceProvider.GetService<IHTSExtractSourceReader>();
            var reader = _reader.ExecuteReader(TestInitializer.IQtoolsDbProtocol, extract).Result as SqlDataReader;
            Assert.NotNull(reader);
            Assert.True(reader.HasRows);
            reader.Close();
        }

        [TestCase(nameof(HtsClientLinkage))]
        [TestCase("HtsClient")]
        [TestCase(nameof(HtsClientTests))]
        [TestCase(nameof(HtsClientTracing))]
        [TestCase(nameof(HtsPartnerNotificationServices))]
        [TestCase(nameof(HtsPartnerTracing))]
        [TestCase(nameof(HtsTestKits))]
        public void should_Execute_Reader_MySql(string extractName)
        {
            var extract =
                TestInitializer.KenyaEmr.Extracts.First(
                    x => x.DocketId.IsSameAs("HTS") && x.Name.IsSameAs(extractName));
            _reader = TestInitializer.ServiceProviderMysql.GetService<IHTSExtractSourceReader>();
            var reader = _reader.ExecuteReader(TestInitializer.KenyaEmrDbProtocol, extract).Result as MySqlDataReader;
            Assert.NotNull(reader);
            Assert.True(reader.HasRows);
            reader.Close();
        }
    }
}
