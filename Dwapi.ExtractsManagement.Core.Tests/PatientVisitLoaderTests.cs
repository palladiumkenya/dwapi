using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using FizzWare.NBuilder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Loader.Dwh
{
    [TestFixture]
    public class PatientVisitLoaderTests
    {
        private ExtractsContext _extractsContext, _extractsContextMySql;
        private DbProtocol _iQtoolsDb, _kenyaEmrDb;
        [OneTimeSetUp]
        public void Init()
        {
            var extractId = TestInitializer.Iqtools.Extracts.First(x => x.Name.IsSameAs(nameof(PatientVisitExtract))).Id;
            var cleaner = TestInitializer.ServiceProvider.GetService<ICleanCbsExtracts>();
            cleaner.Clean(extractId);

            var extractIdMySql = TestInitializer.KenyaEmr.Extracts.First(x => x.Name.IsSameAs(nameof(PatientVisitExtract))).Id;
            var cleanerMySql = TestInitializer.ServiceProviderMysql.GetService<ICleanCbsExtracts>();
            cleanerMySql.Clean(extractIdMySql);

            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            _extractsContextMySql = TestInitializer.ServiceProviderMysql.GetService<ExtractsContext>();

            var tempMpis = Builder<TempPatientVisitExtract>.CreateListOfSize(2).All().With(x=>x.CheckError=false).Build().ToList();

            _extractsContext.TempPatientVisitExtracts.AddRange(tempMpis);
            _extractsContext.SaveChanges();

            _extractsContextMySql.TempPatientVisitExtracts.AddRange(tempMpis);
            _extractsContextMySql.SaveChanges();

            _iQtoolsDb = TestInitializer.Iqtools.DatabaseProtocols.First(x => x.DatabaseName.ToLower().Contains("iqtools".ToLower()));
            _iQtoolsDb.Host = ".\\Koske14";
            _iQtoolsDb.Username = "sa";
            _iQtoolsDb.Password = "maun";

            _kenyaEmrDb = TestInitializer.KenyaEmr.DatabaseProtocols.First();
            _kenyaEmrDb.Host = "127.0.0.1";
            _kenyaEmrDb.Username = "root";
            _kenyaEmrDb.Password = "test";
            _kenyaEmrDb.DatabaseName = "openmrs";
        }


        [Test]
        public void should_Load_From_Temp_MsSQL()
        {
            Assert.False(_extractsContext.PatientVisitExtracts.Any());
            var extract = TestInitializer.Iqtools.Extracts.First(x => x.Name.IsSameAs(nameof(PatientVisitExtract)));

            var loader = TestInitializer.ServiceProvider.GetService<IPatientVisitLoader>();

            var loadCount = loader.Load(extract.Id, 0).Result;
            Assert.True(_extractsContext.PatientVisitExtracts.Any());
            Console.WriteLine($"extracted {_extractsContext.PatientVisitExtracts.Count()}");
        }

        [Test]
        public void should_Load_From_Temp_MySQL()
        {
            Assert.False(_extractsContextMySql.PatientVisitExtracts.Any());
            var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.Name.IsSameAs(nameof(PatientVisitExtract)));

            var loader = TestInitializer.ServiceProviderMysql.GetService<IPatientVisitLoader>();

            var loadCount = loader.Load(extract.Id, 0).Result;
            Assert.True(_extractsContextMySql.PatientVisitExtracts.Any());
            Console.WriteLine($"extracted {_extractsContextMySql.PatientVisitExtracts.Count()}");
        }
    }
}