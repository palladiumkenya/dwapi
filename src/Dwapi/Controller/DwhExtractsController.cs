using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.Hubs.Dwh;
using Dwapi.Models;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Exchange.Dwh.Smart;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Hangfire;
using Hangfire.States;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/DwhExtracts")]
    public class DwhExtractsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;
        private readonly IExtractStatusService _extractStatusService;
        private readonly IHubContext<ExtractActivity> _hubContext;
        private readonly IDwhSendService _dwhSendService;
        private readonly ICbsSendService _cbsSendService;
        private readonly ICTSendService _ctSendService;
        private readonly IExtractRepository _extractRepository;
        private readonly IIndicatorExtractRepository _indicatorExtractRepository;
        private readonly IDiffLogRepository _diffLogRepository;
        private readonly ICTExportService _ctExportService;


        private readonly string _version;

        public DwhExtractsController(IMediator mediator, IExtractStatusService extractStatusService, IHubContext<ExtractActivity> hubContext, IDwhSendService dwhSendService,  ICbsSendService cbsSendService, ICTSendService ctSendService, IExtractRepository extractRepository, IIndicatorExtractRepository indicatorExtractRepository,IDiffLogRepository diffLogRepository, ICTExportService ctExportService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _extractStatusService = extractStatusService;
            _dwhSendService = dwhSendService;
            _cbsSendService = cbsSendService;
            _ctSendService = ctSendService;
            _extractRepository = extractRepository;
            _indicatorExtractRepository = indicatorExtractRepository;
            _diffLogRepository = diffLogRepository;
            _ctExportService = ctExportService;
            Startup.HubContext= _hubContext = hubContext;
            _version = GetType().Assembly.GetName().Version.ToString();
        }

        [HttpPost("extract")]
        public async Task<IActionResult> Load([FromBody]ExtractPatient request)
        {
            if(!request.IsValid())
                return BadRequest();

            var result = await _mediator.Send(request, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpPost("extractAll")]
        public async Task<IActionResult> Load([FromBody] LoadExtracts request)
        {
            if (!request.IsValid())
                return BadRequest();

            string version = GetType().Assembly.GetName().Version.ToString();
            // update LoadChangesOnly to value passed from loadFromEmr() command

            try
            {
                if (!request.LoadMpi)
                {
                    var result = await _mediator.Send(request.LoadFromEmrCommand, HttpContext.RequestAborted);

                    await _mediator.Publish(new ExtractLoaded("CareTreatment", version, request.EmrSetup));

                    return Ok(result);
                }

                var dwhExtractsTask =
                    Task.Run(() => _mediator.Send(request.LoadFromEmrCommand, HttpContext.RequestAborted));
                var mpiExtractsTask = Task.Run(() => _mediator.Send(request.ExtractMpi, HttpContext.RequestAborted));
                var extractTasks = new List<Task<bool>> {mpiExtractsTask, dwhExtractsTask};
                // wait for both tasks but doesn't throw an error for mpi load
                var result1 = await Task.WhenAll(extractTasks);
                return Ok(dwhExtractsTask);
            } catch (Exception e)
            {
                var msg = $"Error Loading Extracts --> {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }



        // GET: api/DwhExtracts/status/id
        [HttpGet("status/{id}")]
        public IActionResult GetStatus(Guid id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest();
            try
            {
                var eventExtract = _extractStatusService.GetStatus(id);
                if (null == eventExtract)
                    return NotFound();

                return Ok(eventExtract);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(Extract)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // POST: api/DwhExtracts/manifest
        [HttpPost("manifest")]
        public async Task<IActionResult> SendManifest([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();

            string version = GetType().Assembly.GetName().Version.ToString();

            await _mediator.Publish(new ExtractSent("CareTreatment", version));

            try
            {
                if (!packageDto.SendMpi)
                {
                    var result = await _dwhSendService.SendManifestAsync(packageDto.DwhPackage,_version);
                    return Ok(result);
                }

                var mpiTask = await _cbsSendService.SendManifestAsync(packageDto.MpiPackage,_version);
                var dwhTask = await _dwhSendService.SendManifestAsync(packageDto.DwhPackage,_version);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending  Manifest {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        // POST: api/DwhExtracts/diffmanifest
        [HttpPost("diffmanifest")]
        public async Task<IActionResult> SendDiffManifest([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();

            string version = GetType().Assembly.GetName().Version.ToString();

            await _mediator.Publish(new ExtractSent("CareTreatment", version));

            try
            {
                // check stale
                if (_indicatorExtractRepository.CheckIfStale())
                {
                    throw new Exception(" ---> Error sending Extracts. Database is stale. Please make sure your Database is up to date");
                }

                if (!packageDto.SendMpi)
                {
                    var result = await _dwhSendService.SendDiffManifestAsync(packageDto.DwhPackage,_version);
                    return Ok(result);
                }

                var mpiTask = await _cbsSendService.SendManifestAsync(packageDto.MpiPackage,_version);
                var dwhTask = await _dwhSendService.SendManifestAsync(packageDto.DwhPackage,_version);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending  Manifest {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        #region Smart Endpoints
        // POST: api/DwhExtracts/manifest
        [HttpPost("smart/manifest")]
        public async Task<IActionResult> SendSmartManifest([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();

            string version = GetType().Assembly.GetName().Version.ToString();

            await _mediator.Publish(new ExtractSent("CareTreatment", version));

            try
            {
                // check stale
                 if (_indicatorExtractRepository.CheckIfStale())
                 {
                     throw new Exception(" ---> Error sending Extracts. Database is stale. Please make sure your Database is up to date");
                 }

                var result = await _ctSendService.SendSmartManifestAsync(packageDto.DwhPackage, _version, "3");
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = $"Error sending Smart Manifest {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("smart/exportmanifest")]
        public async Task<IActionResult> exportSmartManifest([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();

            string version = GetType().Assembly.GetName().Version.ToString();

            await _mediator.Publish(new ExtractSent("CareTreatment", version));

            try
            {
                var result = await _ctExportService.ExportSmartManifestAsync(packageDto.DwhPackage, _version, "3");
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = $"Error sending Smart Manifest {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        // POST: api/DwhExtracts/patients
        [HttpPost("smart/patients")]
        public IActionResult SendSmartPatientExtracts([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                QueueSmartDwh(packageDto.DwhPackage);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Smart Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("smart/exportpatients")]
        public IActionResult ExportSmartPatientExtracts([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                QueueSmartExportDwh(packageDto.DwhPackage);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Smart Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        #endregion

        // POST: api/DwhExtracts/patients
        [HttpPost("patients")]
        public IActionResult SendPatientExtracts([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                if (!packageDto.SendMpi)
                {
                    QueueDwh(packageDto.DwhPackage);
                    return Ok();
                }
                QueueDwh(packageDto.DwhPackage);
                QueueMpi(packageDto.MpiPackage);
                return Ok();

            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("exportpatientsCT")]
        public IActionResult ExportPatientExtracts([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                if (!packageDto.SendMpi)
                {
                    QueueExportDwh(packageDto.DwhPackage);
                    return Ok();
                }
                QueueExportDwh(packageDto.DwhPackage);
                QueueMpi(packageDto.MpiPackage);
                return Ok();

            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }


        // POST: api/DwhExtracts/patients
        [HttpPost("diffpatients")]
        public IActionResult SendDiffPatientExtracts([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                if (!packageDto.SendMpi)
                {
                    QueueDwhDiff(packageDto.DwhPackage);
                    return Ok();
                }
                QueueDwh(packageDto.DwhPackage);
                QueueMpi(packageDto.MpiPackage);
                return Ok();

            }
            catch (Exception e)
            {
                var msg = $"Error sending diff patients Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpGet("checkWhichToSend")]
        public async Task<IActionResult> CheckWhichToSend()
        {

            string whichToSend = "";

            string version = GetType().Assembly.GetName().Version.ToString();

            await _mediator.Publish(new ExtractSent("CareTreatment", version));

            try
            {

                var mflcode =   _indicatorExtractRepository.GetMflCode();
                var changesLoaded =
                    _diffLogRepository.GetIfChangesHasBeenLoadedAlreadyLog("NDWH", "PatientExtract", mflcode);
                // check changes loaded
                if (null==changesLoaded)
                {
                    whichToSend = "SendAll";
                }
                else
                {
                    whichToSend = "SendChanges";
                }
                return Ok(whichToSend);

            }
            catch (Exception e)
            {
                var msg = $"Error checking send {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);

            }

        }

        [AutomaticRetry(Attempts = 0)]
        private void QueueDwh(SendManifestPackageDTO package)
        {
            var extracts = _extractRepository.GetAllRelated(package.ExtractId).ToList();

            if (extracts.Any())
                package.Extracts = extracts.Select(x => new ExtractDto() {Id = x.Id, Name = x.Name}).ToList();

            _ctSendService.NotifyPreSending();

            var job1 =
                BatchJob.StartNew(x => { SendJobBaselines(package); });

            var job2 =
                BatchJob.ContinueBatchWith(job1, x => { SendJobProfiles(package); });

            var job3 =
                BatchJob.ContinueBatchWith(job2, x => { SendNewJobProfiles(package); });

            var job4 =
                BatchJob.ContinueBatchWith(job3, x => { SendNewOtherJobProfiles(package); });

            var job5 =
                BatchJob.ContinueBatchWith(job4, x => { SendCovidJobProfiles(package); });

            var jobEnd =
                BatchJob.ContinueBatchWith(job5, x => { _ctSendService.NotifyPostSending(package,_version); });
        }

        [AutomaticRetry(Attempts = 0)]
        private void QueueExportDwh(SendManifestPackageDTO package)
        {
            var extracts = _extractRepository.GetAllRelated(package.ExtractId).ToList();

            if (extracts.Any())
                package.Extracts = extracts.Select(x => new ExtractDto() { Id = x.Id, Name = x.Name }).ToList();

            _ctSendService.NotifyPreSending();

            var job1 =
                BatchJob.StartNew(x => { ExportJobBaselines(package); });

            var job2 =
                BatchJob.ContinueBatchWith(job1, x => { ExportJobProfiles(package); });

            var job3 =
                BatchJob.ContinueBatchWith(job2, x => { ExportNewJobProfiles(package); });

            var job4 =
                BatchJob.ContinueBatchWith(job3, x => { ExportNewOtherJobProfiles(package); });

            var job5 =
                BatchJob.ContinueBatchWith(job4, x => { ExportCovidJobProfiles(package); });

            var jobEnd =
                BatchJob.ContinueBatchWith(job5, x => { _ctExportService.NotifyPostSending(package, _version); });
        }

        [HttpPost("exportmanifest")]
        public async Task<IActionResult> exportManifest([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();

            string version = GetType().Assembly.GetName().Version.ToString();

            await _mediator.Publish(new ExtractSent("CareTreatment", version));

            try
            {
                if (!packageDto.SendMpi)
                {
                    var result = await _ctExportService.ExportManifestAsync(packageDto.DwhPackage, _version);
                    return Ok(result);
                }

                var mpiTask = await _cbsSendService.SendManifestAsync(packageDto.MpiPackage, _version);
                var dwhTask = await _ctExportService.ExportManifestAsync(packageDto.DwhPackage, _version);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting  Manifest {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [AutomaticRetry(Attempts = 0)]
        private void QueueDwhDiff(SendManifestPackageDTO package)
        {
            var extracts = _extractRepository.GetAllRelated(package.ExtractId).ToList();

            if (extracts.Any())
                package.Extracts = extracts.Select(x => new ExtractDto() {Id = x.Id, Name = x.Name}).ToList();

            _ctSendService.NotifyPreSending();

            var job1 =
                BatchJob.StartNew(x => { SendDiffJobBaselines(package); });

            var job2 =
                BatchJob.ContinueBatchWith(job1, x => { SendDiffJobProfiles(package); });

            var job3 =
                BatchJob.ContinueBatchWith(job2, x => { SendDiffNewJobProfiles(package); });

            var job4 =
                BatchJob.ContinueBatchWith(job3, x => { SendDiffNewOtherJobProfiles(package); });

            var job5 =
                BatchJob.ContinueBatchWith(job4, x => { SendDiffCovidJobProfiles(package); });

            var jobEnd =
                BatchJob.ContinueBatchWith(job5, x => { _ctSendService.NotifyPostSending(package, _version); });
        }

        #region Smart

        [AutomaticRetry(Attempts = 0)]
        private void QueueSmartDwh(SendManifestPackageDTO package)
        {
            var extracts = _extractRepository.GetAllRelated(package.ExtractId).ToList();

            if (extracts.Any())
                package.Extracts = extracts.Select(x => new ExtractDto() {Id = x.Id, Name = x.Name}).ToList();

            _ctSendService.NotifyPreSending();

            var job0 =
                BatchJob.StartNew(x => { SendJobSmartPateints(package); });

            var job1 =
                BatchJob.ContinueBatchWith(job0, x => { SendJobSmartBaselines(package); });

            var job2 =
                BatchJob.ContinueBatchWith(job1, x => { SendJobSmartProfiles(package); });

            var job3 =
                BatchJob.ContinueBatchWith(job2, x => { SendNewJobSmartProfiles(package); });

            var job4 =
                BatchJob.ContinueBatchWith(job3, x => { SendNewOtherJobSmartProfiles(package); });

            var job5 =
                BatchJob.ContinueBatchWith(job4, x => { SendCovidJobSmartProfiles(package); });

            var jobEnd =
                BatchJob.ContinueBatchWith(job5, x => { _ctSendService.NotifyPostSending(package,_version); });
        }

        [AutomaticRetry(Attempts = 0)]
        private void QueueSmartExportDwh(SendManifestPackageDTO package)
        {
            var extracts = _extractRepository.GetAllRelated(package.ExtractId).ToList();

            if (extracts.Any())
                package.Extracts = extracts.Select(x => new ExtractDto() { Id = x.Id, Name = x.Name }).ToList();

            _ctSendService.NotifyPreSending();

            var job0 =
                BatchJob.StartNew(x => { ExportJobSmartPatients(package); });

            var job1 =
                BatchJob.ContinueBatchWith(job0, x => { ExportJobSmartBaselines(package); });

            var job2 =
                BatchJob.ContinueBatchWith(job1, x => { ExportJobSmartProfiles(package); });

            var job3 =
                BatchJob.ContinueBatchWith(job2, x => { ExportNewJobSmartProfiles(package); });

            var job4 =
                BatchJob.ContinueBatchWith(job3, x => { ExportNewOtherJobSmartProfiles(package); });

            var job5 =
                BatchJob.ContinueBatchWith(job4, x => { ExportCovidJobSmartProfiles(package); });

            var jobEnd =
                BatchJob.ContinueBatchWith(job5, x => { _ctSendService.NotifyPostSending(package, _version); });
        }

        public void SendJobSmartPateints(SendManifestPackageDTO package)
        {
            var idsA =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Patients, new PatientMessageSourceBag()).Result;
        }
        public void ExportJobSmartPatients(SendManifestPackageDTO package)
        {
            var idsA = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Patients, new PatientMessageSourceBag()).Result;
        }

        public void SendJobSmartBaselines(SendManifestPackageDTO package)
        {
            var idsA =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Patients, new ArtMessageSourceBag()).Result;
            var idsB=_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Patients, new BaselineMessageSourceBag()).Result;
            var idsC= _ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Patients, new StatusMessageSourceBag()).Result;
            var idsD=_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new AdverseEventMessageSourceBag()).Result;
        }
        public void ExportJobSmartBaselines(SendManifestPackageDTO package)
        {
            var idsA = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Patients, new ArtMessageSourceBag()).Result;
            var idsB = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Patients, new BaselineMessageSourceBag()).Result;
            var idsC = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Patients, new StatusMessageSourceBag()).Result;
            var idsD = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new AdverseEventMessageSourceBag()).Result;
        }

        public void SendJobSmartProfiles(SendManifestPackageDTO package)
        {
            var idsC= _ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Visits, new VisitMessageSourceBag()).Result;
            var idsA =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new PharmacyMessageSourceBag()).Result;
            var idsB=_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new LaboratoryMessageSourceBag()).Result;
        }
        public void ExportJobSmartProfiles(SendManifestPackageDTO package)
        {
            var idsC = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Visits, new VisitMessageSourceBag()).Result;
            var idsA = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new PharmacyMessageSourceBag()).Result;
            var idsB = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new LaboratoryMessageSourceBag()).Result;
        }

        public void SendNewJobSmartProfiles(SendManifestPackageDTO package)
        {
            var idsAllergiesChronicIllness =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new AllergiesChronicIllnessMessageSourceBag()).Result;
            var idsIpt =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new IptMessageSourceBag()).Result;
            var idsDepressionScreening =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new DepressionScreeningMessageSourceBag()).Result;
            var idsContactListing =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new ContactListingMessageSourceBag()).Result;
        }
        public void ExportNewJobSmartProfiles(SendManifestPackageDTO package)
        {
            var idsAllergiesChronicIllness = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new AllergiesChronicIllnessMessageSourceBag()).Result;
            var idsIpt = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new IptMessageSourceBag()).Result;
            var idsDepressionScreening = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new DepressionScreeningMessageSourceBag()).Result;
            var idsContactListing = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new ContactListingMessageSourceBag()).Result;
        }

        public void SendNewOtherJobSmartProfiles(SendManifestPackageDTO package)
        {
            var idsGbvScreening =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new GbvScreeningMessageSourceBag()).Result;
            var idsEnhancedAdherenceCounselling =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new EnhancedAdherenceCounsellingMessageSourceBag()).Result;
            var idsDrugAlcoholScreening =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new DrugAlcoholScreeningMessageSourceBag()).Result;
            var idsOvc =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new OvcMessageSourceBag()).Result;
            var idsOtz =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new OtzMessageSourceBag()).Result;
        }
        public void ExportNewOtherJobSmartProfiles(SendManifestPackageDTO package)
        {
            var idsGbvScreening = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new GbvScreeningMessageSourceBag()).Result;
            var idsEnhancedAdherenceCounselling = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new EnhancedAdherenceCounsellingMessageSourceBag()).Result;
            var idsDrugAlcoholScreening = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new DrugAlcoholScreeningMessageSourceBag()).Result;
            var idsOvc = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new OvcMessageSourceBag()).Result;
            var idsOtz = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new OtzMessageSourceBag()).Result;
        }

        public void SendCovidJobSmartProfiles(SendManifestPackageDTO package)
        {
            var idsCovid =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new CovidMessageSourceBag()).Result;
            var idsDefaulterTracing =_ctSendService.SendSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new DefaulterTracingMessageSourceBag()).Result;
        }
        public void ExportCovidJobSmartProfiles(SendManifestPackageDTO package)
        {
            var idsCovid = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new CovidMessageSourceBag()).Result;
            var idsDefaulterTracing = _ctExportService.ExportSmartBatchExtractsAsync(package, Startup.AppFeature.BatchSize.Extracts, new DefaulterTracingMessageSourceBag()).Result;
        }

        #endregion
        public void SendJobBaselines(SendManifestPackageDTO package)
        {
            var idsA =_ctSendService.SendBatchExtractsAsync(package, 500, new ArtMessageBag()).Result;
            var idsB=_ctSendService.SendBatchExtractsAsync(package, 500, new BaselineMessageBag()).Result;
            var idsC= _ctSendService.SendBatchExtractsAsync(package, 500, new StatusMessageBag()).Result;
            var idsD=_ctSendService.SendBatchExtractsAsync(package, 500, new AdverseEventsMessageBag()).Result;
        }
        public void ExportJobBaselines(SendManifestPackageDTO package)
        {
            var idsA = _ctExportService.ExportBatchExtractsAsync(package, 500, new ArtMessageBag()).Result;
            var idsB = _ctExportService.ExportBatchExtractsAsync(package, 500, new BaselineMessageBag()).Result;
            var idsC = _ctExportService.ExportBatchExtractsAsync(package, 500, new StatusMessageBag()).Result;
            var idsD = _ctExportService.ExportBatchExtractsAsync(package, 500, new AdverseEventsMessageBag()).Result;
        }

        public void SendDiffJobBaselines(SendManifestPackageDTO package)
        {
            var idsA =_ctSendService.SendDiffBatchExtractsAsync(package, 500, new ArtMessageBag()).Result;
            var idsB=_ctSendService.SendDiffBatchExtractsAsync(package, 500, new BaselineMessageBag()).Result;
            var idsC= _ctSendService.SendDiffBatchExtractsAsync(package, 500, new StatusMessageBag()).Result;
            var idsD=_ctSendService.SendDiffBatchExtractsAsync(package, 500, new AdverseEventsMessageBag()).Result;
        }
        public void SendJobProfiles(SendManifestPackageDTO package)
        {
            var idsA =_ctSendService.SendBatchExtractsAsync(package, 500, new PharmacyMessageBag()).Result;
            var idsB=_ctSendService.SendBatchExtractsAsync(package, 500, new LabMessageBag()).Result;
            var idsC= _ctSendService.SendBatchExtractsAsync(package, 500, new VisitsMessageBag()).Result;
        }
        public void ExportJobProfiles(SendManifestPackageDTO package)
        {
            var idsA = _ctExportService.ExportBatchExtractsAsync(package, 500, new PharmacyMessageBag()).Result;
            var idsB = _ctExportService.ExportBatchExtractsAsync(package, 500, new LabMessageBag()).Result;
            var idsC = _ctExportService.ExportBatchExtractsAsync(package, 500, new VisitsMessageBag()).Result;
        }

        public void SendNewJobProfiles(SendManifestPackageDTO package)
        {
            var idsAllergiesChronicIllness =_ctSendService.SendBatchExtractsAsync(package, 200, new AllergiesChronicIllnesssMessageBag()).Result;
            var idsIpt =_ctSendService.SendBatchExtractsAsync(package, 200, new IptsMessageBag()).Result;
            var idsDepressionScreening =_ctSendService.SendBatchExtractsAsync(package, 200, new DepressionScreeningsMessageBag()).Result;
            var idsContactListing =_ctSendService.SendBatchExtractsAsync(package, 200, new ContactListingsMessageBag()).Result;
        }
        public void ExportNewJobProfiles(SendManifestPackageDTO package)
        {
            var idsAllergiesChronicIllness = _ctExportService.ExportBatchExtractsAsync(package, 200, new AllergiesChronicIllnesssMessageBag()).Result;
            var idsIpt = _ctExportService.ExportBatchExtractsAsync(package, 200, new IptsMessageBag()).Result;
            var idsDepressionScreening = _ctExportService.ExportBatchExtractsAsync(package, 200, new DepressionScreeningsMessageBag()).Result;
            var idsContactListing = _ctExportService.ExportBatchExtractsAsync(package, 200, new ContactListingsMessageBag()).Result;
        }



        public void SendNewOtherJobProfiles(SendManifestPackageDTO package)
        {
            var idsGbvScreening =_ctSendService.SendBatchExtractsAsync(package, 200, new GbvScreeningsMessageBag()).Result;
            var idsEnhancedAdherenceCounselling =_ctSendService.SendBatchExtractsAsync(package, 200, new EnhancedAdherenceCounsellingsMessageBag()).Result;
            var idsDrugAlcoholScreening =_ctSendService.SendBatchExtractsAsync(package, 200, new DrugAlcoholScreeningsMessageBag()).Result;
            var idsOvc =_ctSendService.SendBatchExtractsAsync(package, 200, new OvcsMessageBag()).Result;
            var idsOtz =_ctSendService.SendBatchExtractsAsync(package, 200, new OtzsMessageBag()).Result;
        }
        public void ExportNewOtherJobProfiles(SendManifestPackageDTO package)
        {
            var idsGbvScreening = _ctExportService.ExportBatchExtractsAsync(package, 200, new GbvScreeningsMessageBag()).Result;
            var idsEnhancedAdherenceCounselling = _ctExportService.ExportBatchExtractsAsync(package, 200, new EnhancedAdherenceCounsellingsMessageBag()).Result;
            var idsDrugAlcoholScreening = _ctExportService.ExportBatchExtractsAsync(package, 200, new DrugAlcoholScreeningsMessageBag()).Result;
            var idsOvc = _ctExportService.ExportBatchExtractsAsync(package, 200, new OvcsMessageBag()).Result;
            var idsOtz = _ctExportService.ExportBatchExtractsAsync(package, 200, new OtzsMessageBag()).Result;
        }



        public void SendCovidJobProfiles(SendManifestPackageDTO package)
        {
            var idsCovid =_ctSendService.SendBatchExtractsAsync(package, 200, new CovidsMessageBag()).Result;
            var idsDefaulterTracing =_ctSendService.SendBatchExtractsAsync(package, 200, new DefaulterTracingsMessageBag()).Result;
        }
        public void ExportCovidJobProfiles(SendManifestPackageDTO package)
        {
            var idsCovid = _ctExportService.ExportBatchExtractsAsync(package, 200, new CovidsMessageBag()).Result;
            var idsDefaulterTracing = _ctExportService.ExportBatchExtractsAsync(package, 200, new DefaulterTracingsMessageBag()).Result;
        }



        public void SendDiffJobProfiles(SendManifestPackageDTO package)
        {
            var idsA =_ctSendService.SendDiffBatchExtractsAsync(package, 500, new PharmacyMessageBag()).Result;
            var idsB=_ctSendService.SendDiffBatchExtractsAsync(package, 500, new LabMessageBag()).Result;
            var idsC= _ctSendService.SendDiffBatchExtractsAsync(package, 500, new VisitsMessageBag()).Result;
        }

        public void SendDiffNewJobProfiles(SendManifestPackageDTO package)
        {
            var idsAllergiesChronicIllness =_ctSendService.SendDiffBatchExtractsAsync(package, 200, new AllergiesChronicIllnesssMessageBag()).Result;
            var idsIpt =_ctSendService.SendDiffBatchExtractsAsync(package, 200, new IptsMessageBag()).Result;
            var idsDepressionScreening =_ctSendService.SendDiffBatchExtractsAsync(package, 200, new DepressionScreeningsMessageBag()).Result;
            var idsContactListing =_ctSendService.SendDiffBatchExtractsAsync(package, 200, new ContactListingsMessageBag()).Result;
        }

        public void SendDiffNewOtherJobProfiles(SendManifestPackageDTO package)
        {
            var idsGbvScreening =_ctSendService.SendDiffBatchExtractsAsync(package, 200, new GbvScreeningsMessageBag()).Result;
            var idsEnhancedAdherenceCounselling =_ctSendService.SendDiffBatchExtractsAsync(package, 200, new EnhancedAdherenceCounsellingsMessageBag()).Result;
            var idsDrugAlcoholScreening =_ctSendService.SendDiffBatchExtractsAsync(package, 200, new DrugAlcoholScreeningsMessageBag()).Result;
            var idsOvc =_ctSendService.SendDiffBatchExtractsAsync(package, 200, new OvcsMessageBag()).Result;
            var idsOtz =_ctSendService.SendDiffBatchExtractsAsync(package, 200, new OtzsMessageBag()).Result;
        }

        public void SendDiffCovidJobProfiles(SendManifestPackageDTO package)
        {
            var idsCovid =_ctSendService.SendDiffBatchExtractsAsync(package, 200, new CovidsMessageBag()).Result;
            var idsDefaulterTracing =_ctSendService.SendDiffBatchExtractsAsync(package, 200, new DefaulterTracingsMessageBag()).Result;
        }

        [AutomaticRetry(Attempts = 0)]
        private void QueueMpi(SendManifestPackageDTO package)
        {
            var client = new BackgroundJobClient();
            var state = new EnqueuedState("mpi");
            client.Create(() => _cbsSendService.SendMpiAsync(package), state);
        }
    }
}
