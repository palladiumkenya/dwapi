using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Utility;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Cleaner.Dwh
{
    [TestFixture]
    public class CleanDwhExtractsTests
    {
        private IServiceProvider _serviceProvider;
        private IServiceProvider _serviceProviderMysql;
        private ExtractsContext _extractsContext;
        private ExtractsContext  _extractsContextMysql;

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = TestInitializer.ServiceProvider;
            _serviceProviderMysql = TestInitializer.ServiceProviderMysql;

            var tp = Builder<TempPatientExtract>.CreateListOfSize(2).Build().ToList();
            var p = Builder<PatientExtract>.CreateListOfSize(2).Build().ToList();

            var ta = Builder<TempPatientArtExtract>.CreateListOfSize(2).Build().ToList();
            var a = Builder<PatientArtExtract>.CreateListOfSize(2).Build().ToList();

            var tb = Builder<TempPatientBaselinesExtract>.CreateListOfSize(2).Build().ToList();
            var b = Builder<PatientBaselinesExtract>.CreateListOfSize(2).Build().ToList();

            var tl = Builder<TempPatientLaboratoryExtract>.CreateListOfSize(2).Build().ToList();
            var l = Builder<PatientLaboratoryExtract>.CreateListOfSize(2).Build().ToList();

            var tph = Builder<TempPatientPharmacyExtract>.CreateListOfSize(2).Build().ToList();
            var ph = Builder<PatientPharmacyExtract>.CreateListOfSize(2).Build().ToList();

            var tv = Builder<TempPatientVisitExtract>.CreateListOfSize(2).Build().ToList();
            var v = Builder<PatientVisitExtract>.CreateListOfSize(2).Build().ToList();

            var ts = Builder<TempPatientStatusExtract>.CreateListOfSize(2).Build().ToList();
            var s = Builder<PatientStatusExtract>.CreateListOfSize(2).Build().ToList();

            var ve = Builder<ValidationError>.CreateListOfSize(2).All().With(x=>x.ValidatorId=TestInitializer.Validator.Id).Build().ToList();

            _extractsContext = _serviceProvider.GetService<ExtractsContext>();

            _extractsContext.TempPatientExtracts.AddRange(tp);
            _extractsContext.PatientExtracts.AddRange(p);

            _extractsContext.TempPatientArtExtracts.AddRange(ta);
            _extractsContext.PatientArtExtracts.AddRange(a);

            _extractsContext.TempPatientBaselinesExtracts.AddRange(tb);
            _extractsContext.PatientBaselinesExtracts.AddRange(b);


            _extractsContext.TempPatientLaboratoryExtracts.AddRange(tl);
            _extractsContext.PatientLaboratoryExtracts.AddRange(l);

            _extractsContext.TempPatientPharmacyExtracts.AddRange(tph);
            _extractsContext.PatientPharmacyExtracts.AddRange(ph);

            _extractsContext.TempPatientStatusExtracts.AddRange(ts);
            _extractsContext.PatientStatusExtracts.AddRange(s);

            _extractsContext.TempPatientVisitExtracts.AddRange(tv);
            _extractsContext.PatientVisitExtracts.AddRange(v);
            _extractsContext.ValidationError.AddRange(ve);

            _extractsContext.SaveChanges();

            _extractsContextMysql = _serviceProviderMysql.GetService<ExtractsContext>();

            _extractsContextMysql.TempPatientExtracts.AddRange(tp);
            _extractsContextMysql.PatientExtracts.AddRange(p);

            _extractsContextMysql.TempPatientArtExtracts.AddRange(ta);
            _extractsContextMysql.PatientArtExtracts.AddRange(a);

            _extractsContextMysql.TempPatientBaselinesExtracts.AddRange(tb);
            _extractsContextMysql.PatientBaselinesExtracts.AddRange(b);

            _extractsContextMysql.TempPatientLaboratoryExtracts.AddRange(tl);
            _extractsContextMysql.PatientLaboratoryExtracts.AddRange(l);

            _extractsContextMysql.TempPatientPharmacyExtracts.AddRange(tph);
            _extractsContextMysql.PatientPharmacyExtracts.AddRange(ph);

            _extractsContextMysql.TempPatientStatusExtracts.AddRange(ts);
            _extractsContextMysql.PatientStatusExtracts.AddRange(s);

            _extractsContextMysql.TempPatientVisitExtracts.AddRange(tv);
            _extractsContextMysql.PatientVisitExtracts.AddRange(v);

            _extractsContextMysql.ValidationError.AddRange(ve);

            _extractsContextMysql.SaveChanges();

        }

        [Test]
        public void should_clear_extracts_MsSQL()
        {
            var extractIds = TestInitializer.Iqtools.Extracts.Where(x => x.DocketId.IsSameAs("NDWH")).Select(x => x.Id);
            CleanExtracts(_extractsContext, _serviceProvider, extractIds);
        }

        [Test]
        public void should_clear_extracts_MySQL()
        {
            var extractIds = TestInitializer.KenyaEmr.Extracts.Where(x => x.DocketId.IsSameAs("NDWH")).Select(x => x.Id);
            CleanExtracts(_extractsContextMysql, _serviceProviderMysql, extractIds);
        }

        private void CleanExtracts(DbContext context,IServiceProvider serviceProvider,IEnumerable<Guid> extractIds)
        {
            Assert.True(context.Set<TempPatientExtract>().Any());
            Assert.True(context.Set<PatientExtract>().Any());

            Assert.True(context.Set<TempPatientArtExtract>().Any());
            Assert.True(context.Set<PatientArtExtract>().Any());

            Assert.True(context.Set<TempPatientBaselinesExtract>().Any());
            Assert.True(context.Set<PatientBaselinesExtract>().Any());

            Assert.True(context.Set<TempPatientPharmacyExtract>().Any());
            Assert.True(context.Set<PatientPharmacyExtract>().Any());

            Assert.True(context.Set<TempPatientLaboratoryExtract>().Any());
            Assert.True(context.Set<PatientLaboratoryExtract>().Any());
        
            Assert.True(context.Set<TempPatientStatusExtract>().Any());
            Assert.True(context.Set<PatientStatusExtract>().Any());

            Assert.True(context.Set<TempPatientVisitExtract>().Any());
            Assert.True(context.Set<PatientVisitExtract>().Any());
            Assert.True(context.Set<ValidationError>().Any());


            var cleanDwhExtracts = serviceProvider.GetService<IClearDwhExtracts>();
            cleanDwhExtracts.Clear(extractIds.ToList()).Wait();

            Assert.False(context.Set<TempPatientExtract>().Any());
            Assert.False(context.Set<PatientExtract>().Any());

            Assert.False(context.Set<TempPatientArtExtract>().Any());
            Assert.False(context.Set<PatientArtExtract>().Any());

            Assert.False(context.Set<TempPatientBaselinesExtract>().Any());
            Assert.False(context.Set<PatientBaselinesExtract>().Any());

            Assert.False(context.Set<TempPatientPharmacyExtract>().Any());
            Assert.False(context.Set<PatientPharmacyExtract>().Any());

            Assert.False(context.Set<TempPatientLaboratoryExtract>().Any());
            Assert.False(context.Set<PatientLaboratoryExtract>().Any());

            Assert.False(context.Set<TempPatientStatusExtract>().Any());
            Assert.False(context.Set<PatientStatusExtract>().Any());

            Assert.False(context.Set<TempPatientVisitExtract>().Any());
            Assert.False(context.Set<PatientVisitExtract>().Any());
            Assert.False(context.Set<ValidationError>().Any());

            Console.WriteLine(context.Database.ProviderName);
            Console.WriteLine(context.Database.GetDbConnection().ConnectionString);
        }
    }
}