using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/HtsSummary")]
    public class HtsSummaryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempHtsClientsExtractRepository _tempHtsClientExtractRepository;
        private readonly IHtsClientsExtractRepository _htsClientExtractRepository;
        private readonly ITempHtsClientsExtractErrorSummaryRepository _htsClientExtractErrorSummaryRepository;

        private readonly ITempHtsClientTestsExtractRepository _tempHtsClientTestsExtractRepository;
        private readonly IHtsClientTestsExtractRepository _htsClientTestsExtractRepository;
        private readonly ITempHtsClientTestsErrorSummaryRepository _htsClientTestsExtractErrorSummaryRepository;

        private readonly ITempHtsTestKitsExtractRepository _tempTestKitsExtractRepository;
        private readonly IHtsTestKitsExtractRepository _htsTestKitsExtractRepository;
        private readonly ITempHtsTestKitsErrorSummaryRepository _htsTestKitsExtractErrorSummaryRepository;

        private readonly ITempHtsClientTracingExtractRepository _tempHtsClientTracingExtractRepository;
        private readonly IHtsClientTracingExtractRepository _htsClientTracingExtractRepository;
        private readonly ITempHtsClientTracingErrorSummaryRepository _htsClientTracingExtractErrorSummaryRepository;

        private readonly ITempHtsPartnerTracingExtractRepository _tempHtsPartnerTracingExtractRepository;
        private readonly IHtsPartnerTracingExtractRepository _htsPartnerTracingExtractRepository;
        private readonly ITempHtsPartnerTracingErrorSummaryRepository _htsPartnerTracingExtractErrorSummaryRepository;

        private readonly ITempHtsClientsLinkageExtractRepository _tempHtsClientLinkageExtractRepository;
        private readonly IHtsClientsLinkageExtractRepository _htsClientLinkageExtractRepository;
        private readonly ITempHtsClientLinkageErrorSummaryRepository _htsClientLinkageExtractErrorSummaryRepository;

        private readonly ITempHtsPartnerNotificationServicesExtractRepository _tempHtsPartnerNotificationServicesExtractRepository;
        private readonly IHtsPartnerNotificationServicesExtractRepository _htsPartnerNotificationServicesExtractRepository;
        private readonly ITempHtsPartnerNotificationServicesErrorSummaryRepository _htsPartnerNotificationServicesExtractErrorSummaryRepository;

        private readonly ITempHtsEligibilityExtractRepository _tempHtsEligibilityExtractRepository;
        private readonly IHtsEligibilityExtractRepository _htsEligibilityExtractRepository;
        private readonly ITempHtsEligibilityExtractErrorSummaryRepository _htsEligibilityExtractErrorSummaryRepository;

        private readonly ITempHtsRiskScoresRepository _tempHtsRiskScoresRepository;
        private readonly IHtsRiskScoresRepository _htsRiskScoresRepository;
        private readonly ITempHtsRiskScoresErrorSummaryRepository _htsRiskScoresErrorSummaryRepository;


        public HtsSummaryController(
            ITempHtsClientsExtractRepository tempHtsClientExtractRepository, IHtsClientsExtractRepository htsClientExtractRepository, ITempHtsClientsExtractErrorSummaryRepository htsClientExtractErrorSummaryRepository,
            ITempHtsClientTestsExtractRepository tempHtsClientTestsExtractRepository, IHtsClientTestsExtractRepository htsClientTestsExtractRepository, ITempHtsClientTestsErrorSummaryRepository htsClientTestsExtractErrorSummaryRepository,
            ITempHtsTestKitsExtractRepository tempTestKitsExtractRepository, IHtsTestKitsExtractRepository htsTestKitsExtractRepository, ITempHtsTestKitsErrorSummaryRepository htsTestKitsExtractErrorSummaryRepository,
            ITempHtsClientTracingExtractRepository tempHtsClientTracingExtractRepository, IHtsClientTracingExtractRepository htsClientTracingExtractRepository, ITempHtsClientTracingErrorSummaryRepository htsClientTracingExtractErrorSummaryRepository,
            ITempHtsPartnerTracingExtractRepository tempHtsPartnerTracingExtractRepository, IHtsPartnerTracingExtractRepository htsPartnerTracingExtractRepository, ITempHtsPartnerTracingErrorSummaryRepository htsPartnerTracingExtractErrorSummaryRepository,
            ITempHtsClientsLinkageExtractRepository tempHtsClientLinkageExtractRepository, IHtsClientsLinkageExtractRepository htsClientLinkageExtractRepository, ITempHtsClientLinkageErrorSummaryRepository htsClientLinkageExtractErrorSummaryRepository,
            ITempHtsPartnerNotificationServicesExtractRepository tempHtsPartnerNotificationServicesExtractRepository, IHtsPartnerNotificationServicesExtractRepository htsPartnerNotificationServicesExtractRepository,
            ITempHtsPartnerNotificationServicesErrorSummaryRepository htsPartnerNotificationServicesExtractErrorSummaryRepository,
            ITempHtsEligibilityExtractRepository tempHtsEligibilityExtractRepository, IHtsEligibilityExtractRepository htsEligibilityExtractRepository, ITempHtsEligibilityExtractErrorSummaryRepository htsEligibilityExtractErrorSummaryRepository,
            ITempHtsRiskScoresRepository tempHtsRiskScoresRepository, IHtsRiskScoresRepository htsRiskScoresRepository, ITempHtsRiskScoresErrorSummaryRepository htsRiskScoresErrorSummaryRepository
            )

        {
            _tempHtsClientExtractRepository = tempHtsClientExtractRepository;
            _htsClientExtractRepository = htsClientExtractRepository;
            _htsClientExtractErrorSummaryRepository = htsClientExtractErrorSummaryRepository;

            _tempHtsClientLinkageExtractRepository = tempHtsClientLinkageExtractRepository;
            _htsClientLinkageExtractRepository = htsClientLinkageExtractRepository;
            _htsClientLinkageExtractErrorSummaryRepository = htsClientLinkageExtractErrorSummaryRepository;

            _tempHtsClientTestsExtractRepository = tempHtsClientTestsExtractRepository;
            _htsClientTestsExtractRepository = htsClientTestsExtractRepository;
            _htsClientTestsExtractErrorSummaryRepository = htsClientTestsExtractErrorSummaryRepository;

            _tempHtsPartnerTracingExtractRepository = tempHtsPartnerTracingExtractRepository;
            _htsPartnerTracingExtractRepository = htsPartnerTracingExtractRepository;
            _htsPartnerTracingExtractErrorSummaryRepository = htsPartnerTracingExtractErrorSummaryRepository;

            _tempTestKitsExtractRepository = tempTestKitsExtractRepository;
            _htsTestKitsExtractRepository = htsTestKitsExtractRepository;
            _htsTestKitsExtractErrorSummaryRepository = htsTestKitsExtractErrorSummaryRepository;

            _tempHtsClientTracingExtractRepository = tempHtsClientTracingExtractRepository;
            _htsClientTracingExtractRepository = htsClientTracingExtractRepository;
            _htsClientTracingExtractErrorSummaryRepository = htsClientTracingExtractErrorSummaryRepository;

            _tempHtsPartnerNotificationServicesExtractRepository = tempHtsPartnerNotificationServicesExtractRepository;
            _htsPartnerNotificationServicesExtractRepository = htsPartnerNotificationServicesExtractRepository;
            _htsPartnerNotificationServicesExtractErrorSummaryRepository = htsPartnerNotificationServicesExtractErrorSummaryRepository;

            _tempHtsEligibilityExtractRepository = tempHtsEligibilityExtractRepository;
            _htsEligibilityExtractRepository = htsEligibilityExtractRepository;
            _htsEligibilityExtractErrorSummaryRepository = htsEligibilityExtractErrorSummaryRepository;

            _tempHtsRiskScoresRepository = tempHtsRiskScoresRepository;
            _htsRiskScoresRepository = htsRiskScoresRepository;
            _htsRiskScoresErrorSummaryRepository = htsRiskScoresErrorSummaryRepository;

        }


        [HttpGet("clientcount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _htsClientExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Clients";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("client/{page}/{pageSize}")]
        public async Task<IActionResult> LoadClientValid(int? page,int pageSize)
        {
            try
            {
                var tempClientExtracts = await  _htsClientExtractRepository.GetAll(page,pageSize);
                return Ok(tempClientExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Clients";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("clientvalidations")]
        public IActionResult LoadClientValidations()
        {
            try
            {
                var errorSummary = _htsClientExtractErrorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Clients error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }


        [HttpGet("linkagecount")]
        public async Task<IActionResult> GetLinkageCount()
        {
            try
            {
                var count = await _htsClientLinkageExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Linkages";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("linkage/{page}/{pageSize}")]
        public async Task<IActionResult> LoadLinkageValid(int? page,int pageSize)
        {
            try
            {
                var tempPatientArtExtracts = await _htsClientLinkageExtractRepository.GetAll(page,pageSize);
                return Ok(tempPatientArtExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Linkage Extracts";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("linkagevalidations")]
        public IActionResult LoadLinkageValidations()
        {
            try
            {
                var errorSummary = _htsClientLinkageExtractErrorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Linkage error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("partnertracingcount")]
        public async Task<IActionResult> GetPartnerTrackingCount()
        {
            try
            {
                var count = await _htsPartnerTracingExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Partner Tracking";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("partnertracing/{page}/{pageSize}")]
        public async Task<IActionResult> LoadPartnerTrackingValid(int? page,int pageSize)
        {
            try
            {
                var count =await _htsPartnerTracingExtractRepository.GetAll(page, pageSize);
                return Ok(count.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Partner Tracking";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("partnertracingvalidations")]
        public IActionResult LoadPartnerTrackingValidations()
        {
            try
            {
                var errorSummary = _htsPartnerTracingExtractErrorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Partner Tracking error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("clienttracingcount")]
        public async Task<IActionResult> GetClientTrackingCount()
        {
            try
            {
                var count = await _htsClientTracingExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Client Tracking";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("clienttracing/{page}/{pageSize}")]
        public async Task<IActionResult> LoadClientTrackingValid(int? page, int pageSize)
        {
            try
            {
                var count = await _htsClientTracingExtractRepository.GetAll(page, pageSize);
                return Ok(count.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Client Tracking";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("clienttracingvalidations")]
        public IActionResult LoadClientTrackingValidations()
        {
            try
            {
                var errorSummary = _htsClientTracingExtractErrorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Client Tracking error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("testkitscount")]
        public async Task<IActionResult> GetTestKitsCount()
        {
            try
            {
                var count = await _htsTestKitsExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Test Kits";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("testkits/{page}/{pageSize}")]
        public async Task<IActionResult> LoadTestKitsValid(int? page, int pageSize)
        {
            try
            {
                var count = await _htsTestKitsExtractRepository.GetAll(page, pageSize);
                return Ok(count.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Test Kits";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("testkitsvalidations")]
        public IActionResult LoadTestKitsValidations()
        {
            try
            {
                var errorSummary = _htsTestKitsExtractErrorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Test  Kits error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("pnscount")]
        public async Task<IActionResult> GetPNSCount()
        {
            try
            {
                var count = await _htsPartnerNotificationServicesExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Partner Notification Services";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("pns/{page}/{pageSize}")]
        public async Task<IActionResult> LoadPNSValid(int? page, int pageSize)
        {
            try
            {
                var count = await _htsPartnerNotificationServicesExtractRepository.GetAll(page, pageSize);
                return Ok(count.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Partner Notification Services";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("pnsvalidations")]
        public IActionResult LoadPNSValidations()
        {
            try
            {
                var errorSummary = _htsPartnerNotificationServicesExtractErrorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Partner Notification Services error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("clienttestscount")]
        public async Task<IActionResult> GetClientTestsCount()
        {
            try
            {
                var count = await _htsClientTestsExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Client Tests";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("clienttests/{page}/{pageSize}")]
        public async Task<IActionResult> LoadClientTestsValid(int? page, int pageSize)
        {
            try
            {
                var count = await _htsClientTestsExtractRepository.GetAll(page, pageSize);
                return Ok(count.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Client Tests";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("clienttestsvalidations")]
        public IActionResult LoadClientTestsValidations()
        {
            try
            {
                var errorSummary = _htsClientTestsExtractErrorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Client Tests error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("eligibilitycount")]
        public async Task<IActionResult> GetEligibilityCount()
        {
            try
            {
                var count = await _htsEligibilityExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Linkages";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }


        [HttpGet("eligibility/{page}/{pageSize}")]
        public async Task<IActionResult> LoadEligibilityValid(int? page, int pageSize)
        {
            try
            {
                var count = await _htsEligibilityExtractRepository.GetAll(page, pageSize);
                return Ok(count.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid eligibility";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("eligibilityvalidations")]
        public IActionResult LoadEligibilityValidations()
        {
            try
            {
                var errorSummary = _htsEligibilityExtractErrorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading eligibility error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }


        [HttpGet("riskscorescount")]
        public async Task<IActionResult> GetRiskScoresCount()
        {
            try
            {
                var count = await _htsRiskScoresRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Linkages";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("riskscores/{page}/{pageSize}")]
        public async Task<IActionResult> LoadRiskScoresValid(int? page, int pageSize)
        {
            try
            {
                var count = await _htsRiskScoresRepository.GetAll(page, pageSize);
                return Ok(count.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid RiskScores";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("riskscoresvalidations")]
        public IActionResult LoadRiskScoresValidations()
        {
            try
            {
                var errorSummary = _htsRiskScoresErrorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading RiskScores error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }


    }
}
