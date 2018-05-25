using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.ExtractValidators;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class ExtractPatientHandler :IRequestHandler<ExtractPatient,bool>
    {
        private readonly IPatientSourceExtractor _patientSourceExtractor;
        private readonly IPatientValidator _patientValidator;
        private readonly IPatientLoader _patientLoader;

        public ExtractPatientHandler(IPatientSourceExtractor patientSourceExtractor, IPatientValidator patientValidator, IPatientLoader patientLoader)
        {
            _patientSourceExtractor = patientSourceExtractor;
            _patientValidator = patientValidator;
            _patientLoader = patientLoader;
        }

        public async Task<bool> Handle(ExtractPatient request, CancellationToken cancellationToken)
        {
            //Extract
            await _patientSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _patientValidator.Validate();

            //Load
            await _patientLoader.Load();

            return true;
        }
    }
}