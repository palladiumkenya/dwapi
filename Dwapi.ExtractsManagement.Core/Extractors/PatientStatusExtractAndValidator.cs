using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.ExtractValidators;
using Dwapi.SharedKernel.Model;
using Dwapi.Domain;
using Dwapi.Domain.Models;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    public class PatientStatusExtractAndValidator : PatientStatusExtractor, IExtractorValidator
    {
        private readonly IValidator _validator;
        private readonly ProgressHub _progressHub;

        //public PatientStatusExtractAndValidator(IExtractUnitOfWork unitOfWork, IValidator validator, ProgressHub progressHub) 
        //    : base(unitOfWork)
        //{
        //    _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        //    _progressHub = progressHub ?? throw new ArgumentNullException(nameof(progressHub));
        //}

        public PatientStatusExtractAndValidator(IExtractUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _validator = new GenericValidator(unitOfWork, nameof(TempPatientStatusExtract));
        }

        public async Task ExtractAndValidateAsync(DwhExtract extract, DbProtocol dbProtocol)
        {
            await base.ExtractAsync(extract, dbProtocol);
            await _validator.Validate();
        }
    }
}
