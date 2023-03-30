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
    public class ExtractPncVisitHandler :IRequestHandler<ExtractPncVisit,bool>
    {
        private readonly IPncVisitSourceExtractor _pncVisitSourceExtractor;
        private readonly IMnchExtractValidator _extractValidator;
        private readonly IPncVisitLoader _pncVisitLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;
        private readonly IDiffLogRepository _diffLogRepository;
        private readonly IIndicatorExtractRepository _indicatorExtractRepository;

        public ExtractPncVisitHandler(IPncVisitSourceExtractor pncVisitSourceExtractor, IMnchExtractValidator extractValidator, IPncVisitLoader pncVisitLoader, IExtractHistoryRepository extractHistoryRepository, IDiffLogRepository diffLogRepository,IIndicatorExtractRepository indicatorExtractRepository)
        {
            _pncVisitSourceExtractor = pncVisitSourceExtractor;
            _extractValidator = extractValidator;
            _pncVisitLoader = pncVisitLoader;
            _extractHistoryRepository = extractHistoryRepository;
            _diffLogRepository = diffLogRepository;
            _indicatorExtractRepository = indicatorExtractRepository;
        }

        public async Task<bool> Handle(ExtractPncVisit request, CancellationToken cancellationToken)
        {
            int found;
            var mflcode =   _indicatorExtractRepository.GetMflCode();

            var loadChangesOnly = true;
            var difflog = _diffLogRepository.GetLog("MNCH", "PncVisitExtract", mflcode);
            var changesLoadedStatus= false;
            
            //Extract
            if (null == difflog)
            {
                found = await _pncVisitSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);
            }
            else
            {
                found  = await _pncVisitSourceExtractor.Extract(request.Extract, request.DatabaseProtocol,difflog.MaxCreated,difflog.MaxModified,difflog.SiteCode);
            }
            //update status
            _diffLogRepository.UpdateExtractsSentStatus("MNCH", "PncVisitExtract", changesLoadedStatus);
            
            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PncVisitExtract), $"{nameof(TempPncVisitExtract)}s");

            //Load
            int loaded = await _pncVisitLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new MnchExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PncVisitExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
