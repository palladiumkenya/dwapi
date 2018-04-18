using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Domain;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.ExtractValidators;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    public class PatientVisitExtractorAndValidator : PatientVisitExtractor, IExtractorValidator
    {
        private readonly IValidator _validator;
        private readonly ProgressHub _progressHub;

        //public PatientVisitExtractorAndValidator(IExtractUnitOfWork unitOfWork, IValidator validator,
        //    ProgressHub progressHub) : base(unitOfWork)
        //{
        //    _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        //    _progressHub = progressHub ?? throw new ArgumentNullException(nameof(progressHub));
        //}

        public PatientVisitExtractorAndValidator(IExtractUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _validator = new GenericValidator(unitOfWork, nameof(TempPatientVisitExtract));
        }

        public async Task ExtractAndValidateAsync(DwhExtract extract, DbProtocol dbProtocol)
        {
            DateTime startDate = DateTime.Now;

            await base.ExtractAsync(extract, dbProtocol);
            Debug.WriteLine($"Start: {startDate} End:{DateTime.Now}");
            await _validator.Validate();
        }
    }
}
