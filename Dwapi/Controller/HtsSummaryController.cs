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
        private readonly ITempHTSClientExtractRepository _tempHtsClientExtractRepository;
        private readonly IHTSClientExtractRepository _htsClientExtractRepository;
        private readonly ITempHTSClientExtractErrorSummaryRepository _htsClientExtractErrorSummaryRepository;

        private readonly ITempHTSClientLinkageExtractRepository _tempHtsClientLinkageExtractRepository;
        private readonly IHTSClientLinkageExtractRepository _htsClientLinkageExtractRepository;
        private readonly ITempHTSClientLinkageExtractErrorSummaryRepository _htsClientLinkageExtractErrorSummaryRepository;

        private readonly ITempHTSClientPartnerExtractRepository _tempHtsClientPartnerExtractRepository;
        private readonly IHTSClientPartnerExtractRepository _htsClientPartnerExtractRepository;
        private readonly ITempHTSClientPartnerExtractErrorSummaryRepository _htsClientPartnerExtractErrorSummaryRepository;

        public HtsSummaryController(
            ITempHTSClientExtractRepository tempHtsClientExtractRepository, IHTSClientExtractRepository htsClientExtractRepository, ITempHTSClientExtractErrorSummaryRepository htsClientExtractErrorSummaryRepository,
            ITempHTSClientLinkageExtractRepository tempHtsClientLinkageExtractRepository, IHTSClientLinkageExtractRepository htsClientLinkageExtractRepository, ITempHTSClientLinkageExtractErrorSummaryRepository htsClientLinkageExtractErrorSummaryRepository,
            ITempHTSClientPartnerExtractRepository tempHtsClientPartnerExtractRepository, IHTSClientPartnerExtractRepository htsClientPartnerExtractRepository, ITempHTSClientPartnerExtractErrorSummaryRepository htsClientPartnerExtractErrorSummaryRepository)
        {
            _tempHtsClientExtractRepository = tempHtsClientExtractRepository;
            _htsClientExtractRepository = htsClientExtractRepository;
            _htsClientExtractErrorSummaryRepository = htsClientExtractErrorSummaryRepository;

            _tempHtsClientLinkageExtractRepository = tempHtsClientLinkageExtractRepository;
            _htsClientLinkageExtractRepository = htsClientLinkageExtractRepository;
            _htsClientLinkageExtractErrorSummaryRepository = htsClientLinkageExtractErrorSummaryRepository;

            _tempHtsClientPartnerExtractRepository = tempHtsClientPartnerExtractRepository;
            _htsClientPartnerExtractRepository = htsClientPartnerExtractRepository;
            _htsClientPartnerExtractErrorSummaryRepository = htsClientPartnerExtractErrorSummaryRepository;
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
                var tempPatientArtExtracts = await  _htsClientExtractRepository.GetAll(page,pageSize);
                return Ok(tempPatientArtExtracts.ToList());
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
        [HttpGet("partnercount")]
        public async Task<IActionResult> GetPartnerCount()
        {
            try
            {
                var count = await _htsClientPartnerExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Patners";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("partner/{page}/{pageSize}")]
        public async Task<IActionResult> LoadPartnerValid(int? page,int pageSize)
        {
            try
            {
                var tempPatientArtExtracts =await _htsClientPartnerExtractRepository.GetAll(page, pageSize);
                return Ok(tempPatientArtExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Partner Extracts";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("partnervalidations")]
        public IActionResult LoadPartnerValidations()
        {
            try
            {
                var errorSummary = _htsClientPartnerExtractErrorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Partner error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
