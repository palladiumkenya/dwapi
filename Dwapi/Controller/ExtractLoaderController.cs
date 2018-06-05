using System;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Utility;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/ExtractLoader")]
    public class ExtractLoaderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPsmartExtractService _psmartExtractService;
        public ExtractLoaderController(IPsmartExtractService psmartExtractService)
        {
            _psmartExtractService = psmartExtractService;
        }

        // GET: api/ExtractLoader/status/id
        [HttpGet("status/{id}")]
        public IActionResult GetStatus(Guid id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest();
            try
            {
                var eventExtract = _psmartExtractService.GetStatus(id);
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

        // POST: api/ExtractLoader/load
        [HttpPost("load")]
        public IActionResult Load([FromBody]DbExtractProtocolDTO entity)
        {
            if (null == entity)
                return BadRequest();
            
            if (!entity.IsValid())
                return BadRequest();

            try
            {
                //check if busy
               var extractHistory= _psmartExtractService.HasStarted(entity.Extract.Id);

                if (extractHistory.IsStarted())
                {
                    var eventHistory = _psmartExtractService.GetStatus(entity.Extract.Id);
                    if (null != eventHistory)
                        return Ok(new
                        {
                            IsComplete = false,
                            IsStarted =true,
                            eEvent=eventHistory
                        });
                }

                _psmartExtractService.Clear();
                _psmartExtractService.Find(entity);
                _psmartExtractService.Sync(entity);
                _psmartExtractService.Complete(entity.Extract.Id);

                var history = _psmartExtractService.GetStatus(entity.Extract.Id);
            
                if (null != history)
                    return Ok(new
                    {
                        IsComplete = true,
                        IsStarted = false,
                        eEvent = history
                    });

               throw new ArgumentException("Server could not precess your");
            }
            catch (Exception e)
            {
                var msg = $"Error parsing {nameof(Extract)} {_psmartExtractService.GetLoadError()}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

    }
}
