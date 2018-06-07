using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Hangfire;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class LoadFromEmrHandler : IRequestHandler<LoadFromEmrCommand, bool>
    {
        private IMediator _mediator;

        public LoadFromEmrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(LoadFromEmrCommand request, CancellationToken cancellationToken)
        {
            //Generate extract patient command
            var extractPatient = new ExtractPatient()
            {
                //todo check if extracts from all emrs are loaded
                Extract = request.Extracts.FirstOrDefault(x=>x.IsPriority),
                DatabaseProtocol = request.DatabaseProtocol
            };

            var result = await _mediator.Send(extractPatient, cancellationToken);

            if (!result)
            {
                return false;
            }
            return await ExtractAll(request, cancellationToken);
        }

        private async Task<bool> ExtractAll (LoadFromEmrCommand request, CancellationToken cancellationToken)
        {
            var protocal = request.DatabaseProtocol;
            
            // ExtractPatientART
            var patientArtCommand = new ExtractPatientArt()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientArtExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            // ExtractPatientBaselines
            var patientBaselinesCommand = new ExtractPatientBaselines()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientBaselineExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            // ExtractPatientLaboratory
            var patientLaboratoryCommand = new ExtractPatientLaboratory()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientLaboratoryExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            // ExtractPatientPharmacy
            var patientPharmacyCommand = new ExtractPatientBaselines()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientPharmacyExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            // ExtractPatientStatus
            var patientStatusCommand = new ExtractPatientBaselines()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientStatusExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            // ExtractPatientVisit
            var patientVisitCommand = new ExtractPatientVisit()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientVisitExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            IList<Expression<Action>> methodCalls = new List<Expression<Action>>();

            methodCalls.Add(() => _mediator.Send(patientStatusCommand, cancellationToken));

            methodCalls.Add(() => _mediator.Send(patientStatusCommand, cancellationToken));

            methodCalls.Add(() => _mediator.Send(patientPharmacyCommand, cancellationToken));

            methodCalls.Add(() => _mediator.Send(patientLaboratoryCommand, cancellationToken));

            methodCalls.Add(() => _mediator.Send(patientBaselinesCommand, cancellationToken));

            methodCalls.Add(() => _mediator.Send(patientArtCommand, cancellationToken));

            methodCalls.Add(() => _mediator.Send(patientVisitCommand, cancellationToken));
            
            ChainJobs(methodCalls);

            return true;
        }

        public void ChainJobs(IList<Expression<Action>> methodCalls)
        {
            string jobId = null;
            foreach (var methodCall in methodCalls)
            {
                if (string.IsNullOrWhiteSpace(jobId))
                {
                    jobId = BackgroundJob.Enqueue(methodCall);
                    continue;
                }

                jobId = BackgroundJob.ContinueWith(jobId, methodCall);
            }
        }
    }
}