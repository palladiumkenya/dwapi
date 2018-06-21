using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader;
using Dwapi.ExtractsManagement.Infrastructure.Reader;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Source.Psmart
{
    [TestFixture]
    [Category("Psmart")]
    public class PsmartSourceReaderTests
    {
        private IPsmartSourceReader _psmartSourceReader;
        private DbProtocol _mssql, _mysql;
        private DbExtract _extractA, _extractB;

        [SetUp]
        public void SetUp()
        {
           _mssql = new DbProtocol(DatabaseType.MicrosoftSQL, @".\koske14", "sa", "maun", "IQTools_KeHMIS");
            _extractA = new DbExtract {ExtractSql = @"select [Id],[shr],[date_created],[status],[status_date],[uuid] FROM psmart_store", Emr = "IQCare"};
            _mysql = new DbProtocol(DatabaseType.MySQL, @"localhost", "root", "test", "openmrs");
            _extractB = new DbExtract { ExtractSql = @"select id,shr,date_created,status,status_date,uuid FROM psmart_store", Emr = "KenyaEMR"};
            _psmartSourceReader = new PsmartSourceReader();
        }

        [Test]
        public void should_Find_Psmart_MSSQL()
        {
            var psmartSources = _psmartSourceReader.Find(_mssql, _extractA);
            Assert.True(psmartSources > 0);
            Console.WriteLine($"Found:{psmartSources}");
        }

        [Test]
        public void should_Find_Psmart_MySQL()
        {
            var psmartSources = _psmartSourceReader.Find(_mysql, _extractB);
            Assert.True(psmartSources > 0);
            Console.WriteLine($"Found:{psmartSources}");
        }

        [Test]
        public void should_Read_Psmart_MSSQL()
        {
            var psmartSources = _psmartSourceReader.Read(_mssql, _extractA).ToList();
            Assert.True(psmartSources.Count>0);
            Console.WriteLine(_mssql);
            foreach (var psmartSource in psmartSources)
            {
                Console.WriteLine(psmartSource);
            }
        }

        [Test]
        public void should_Read_Psmart_MySQL()
        {
            var psmartSources = _psmartSourceReader.Read(_mysql, _extractB).ToList();
            Assert.True(psmartSources.Count > 0);
            Console.WriteLine(_mysql);
            foreach (var psmartSource in psmartSources)
            {
                Console.WriteLine(psmartSource);
            }
        }

    }
}