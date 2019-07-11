using Dwapi.Controller.ExtractDetails;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace Dwapi.Tests.Controller
{
    [TestFixture]
    public class PatientsControllerTests
    {
        private PatientsController _patientsController;

        private IPatientExtractRepository _patientExtractRepository;
        private ITempPatientExtractRepository _tempPatientExtractRepository;
        private ITempPatientExtractErrorSummaryRepository _errorSummaryRepository;
        private DbContextOptions<ExtractsContext> _options;
        private ExtractsContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            _options = TestDbOptions.GetInMemoryOptions<ExtractsContext>();
            var context = new ExtractsContext(_options);
            context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _context = new ExtractsContext(_options);
            _patientExtractRepository = new PatientExtractRepository(_context);
            _tempPatientExtractRepository = new TempPatientExtractRepository(_context);
            _errorSummaryRepository = new TempPatientExtractErrorSummaryRepository(_context);
            _patientsController = new PatientsController( _patientExtractRepository, _errorSummaryRepository,_tempPatientExtractRepository);
        }

        [Test]
        public void should_Get_All_Valid_Patients()
        {
            var response = _patientsController.LoadValid(1,1).Result;
            var result = response as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var allPatients = result.Value as List<TempPatientExtract>;
            Assert.NotNull(allPatients);
        }

        [Test]
        public void should_Get_All_Invalid_Patients()
        {
            var response = _patientsController.LoadErrors();
            var result = response as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var allPatients = result.Value as List<TempPatientExtract>;
            Assert.NotNull(allPatients);
        }
    }
}
