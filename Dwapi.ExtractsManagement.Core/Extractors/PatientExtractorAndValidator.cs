using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Domain;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.ExtractValidators;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    public class PatientExtractorAndValidator :  PatientExtractor, IExtractorValidator
    {
        private readonly IValidator _validator;

        //public PatientExtractorAndValidator(IExtractUnitOfWork unitOfWork, IValidator validator) 
        //    : base(unitOfWork)
        //{
        //    _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        //}

        public PatientExtractorAndValidator(IExtractUnitOfWork unitOfWork, IExtractStatusService extractStatusService)
            : base(unitOfWork, extractStatusService)
        {
            _validator = new GenericValidator(unitOfWork, nameof(TempPatientExtract));
        }

        public async Task ExtractAndValidateAsync(DwhExtract extract, DbProtocol dbProtocol)
        {
            await base.ExtractAsync(extract, dbProtocol);
            await _validator.Validate();
        }
    }
}
