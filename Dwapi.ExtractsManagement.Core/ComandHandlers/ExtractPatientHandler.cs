using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.ExtractValidators;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class ExtractPatientHandler :IRequestHandler<ExtractPatient,bool>
    {
        private readonly IPatientSourceExtractor _patientSourceExtractor;

        public ExtractPatientHandler(IPatientSourceExtractor patientSourceExtractor)
        {
            _patientSourceExtractor = patientSourceExtractor;
        }

        public Task<bool> Handle(ExtractPatient request, CancellationToken cancellationToken)
        {
            var x =_patientSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);
            return x;
        }
    }
}