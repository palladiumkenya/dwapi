using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.ExtractValidators;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class ExtractPatientHandler :IRequestHandler<ExtractPatient,bool>
    {
        private readonly IPatientSourceExtractor _patientSourceExtractor;
        private readonly IPatientValidator _patientValidator;

        public ExtractPatientHandler(IPatientSourceExtractor patientSourceExtractor, IPatientValidator patientValidator)
        {
            _patientSourceExtractor = patientSourceExtractor;
            _patientValidator = patientValidator;
        }

        public async Task<bool> Handle(ExtractPatient request, CancellationToken cancellationToken)
        {
            await _patientSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);
            await _patientValidator.Validate();
            return true;
        }
    }
}