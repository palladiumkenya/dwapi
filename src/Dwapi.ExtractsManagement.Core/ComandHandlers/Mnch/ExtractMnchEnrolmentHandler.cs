using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Mnch
{
    public class ExtractMnchEnrolmentHandler :IRequestHandler<ExtractMnchEnrolment, bool>
    {
        private readonly IMnchEnrolmentSourceExtractor _mnchEnrolmentSourceExtractor;
        private readonly IMnchExtractValidator _extractValidator;
        private readonly IMnchEnrolmentLoader _mnchEnrolmentLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;
        private readonly IDiffLogRepository _diffLogRepository;
        private readonly IIndicatorExtractRepository _indicatorExtractRepository;

        public ExtractMnchEnrolmentHandler(IMnchEnrolmentSourceExtractor mnchEnrolmentSourceExtractor, IMnchExtractValidator extractValidator, IMnchEnrolmentLoader mnchEnrolmentLoader, IExtractHistoryRepository extractHistoryRepository, IDiffLogRepository diffLogRepository,IIndicatorExtractRepository indicatorExtractRepository)
        {
            _mnchEnrolmentSourceExtractor = mnchEnrolmentSourceExtractor;
            _extractValidator = extractValidator;
            _mnchEnrolmentLoader = mnchEnrolmentLoader;
            _extractHistoryRepository = extractHistoryRepository;
            _diffLogRepository = diffLogRepository;
            _indicatorExtractRepository = indicatorExtractRepository;
        }

        public async Task<bool> Handle(ExtractMnchEnrolment request, CancellationToken cancellationToken)
        {
            int found;
            var mflcode =   _indicatorExtractRepository.GetMflCode();

            var loadChangesOnly = true;
            var difflog = _diffLogRepository.GetLog("MNCH", "MnchEnrolmentExtract", mflcode);
            var changesLoadedStatus= false;
            
            //Extract
            if (null == difflog)
            {
                found = await _mnchEnrolmentSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);
            }
            else
            {
                found  = await _mnchEnrolmentSourceExtractor.Extract(request.Extract, request.DatabaseProtocol,difflog.MaxCreated,difflog.MaxModified,difflog.SiteCode);
            }
            //update status
            _diffLogRepository.UpdateExtractsSentStatus("MNCH", "MnchEnrolmentExtract", changesLoadedStatus);
            
            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(MnchEnrolmentExtract), $"{nameof(TempMnchEnrolmentExtract)}s");

            //Load
            int loaded = await _mnchEnrolmentLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new MnchExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(MnchEnrolmentExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
