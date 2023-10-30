using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/ArtFastTrack")]
    public class ArtFastTrackController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempArtFastTrackExtractRepository _tempArtFastTrackExtractRepository;
        private readonly IArtFastTrackExtractRepository _artFastTrackExtractRepository;
        private readonly ITempArtFastTrackExtractErrorSummaryRepository _errorSummaryRepository;

        public ArtFastTrackController(ITempArtFastTrackExtractRepository tempArtFastTrackExtractRepository, IArtFastTrackExtractRepository ArtFastTrackExtractRepository, ITempArtFastTrackExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempArtFastTrackExtractRepository = tempArtFastTrackExtractRepository;
            _artFastTrackExtractRepository = ArtFastTrackExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _artFastTrackExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Patient Extracts";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("LoadValid/{page}/{pageSize}")]
        public async Task<IActionResult> LoadValid(int? page,int pageSize)
        {
            try
            {
                var tempArtFastTrackExtracts = await _artFastTrackExtractRepository.GetAll(page,pageSize);
                return Ok(tempArtFastTrackExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid ArtFastTrack Extracts";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("LoadErrors")]
        public IActionResult LoadErrors()
        {
            try
            {
                var tempArtFastTrackExtracts = _tempArtFastTrackExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempArtFastTrackExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading ArtFastTrack Extracts with errors";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("LoadValidations")]
        public IActionResult LoadValidations()
        {
            try
            {
                var errorSummary = _errorSummaryRepository.GetAll().OrderByDescending(x=>x.Type).ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Patient art fast track error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
