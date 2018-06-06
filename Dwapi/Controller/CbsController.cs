using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.Hubs.Dwh;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/Cbs")]
    public class CbsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;
        private readonly IExtractStatusService _extractStatusService;
        private readonly IHubContext<CbsActivity> _hubContext;
        private readonly IMasterPatientIndexRepository _masterPatientIndexRepository;
        public CbsController(IMediator mediator, IExtractStatusService extractStatusService, IHubContext<CbsActivity> hubContext, IMasterPatientIndexRepository masterPatientIndexRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _extractStatusService = extractStatusService;
            _masterPatientIndexRepository = masterPatientIndexRepository;
            Startup.CbsHubContext= _hubContext = hubContext;
        }

    
        [HttpPost("extract")]
        public async Task<IActionResult> Load([FromBody]ExtractMasterPatientIndex request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _mediator.Send(request, HttpContext.RequestAborted);
            return Ok(result);
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

        [HttpGet("count")]
        public IActionResult GetExtractCount()
        {
            try
            {
                var count = _masterPatientIndexRepository.GetAll().ToList().Count;
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(Extract)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
        [HttpGet]
        public IActionResult GetExtracts()
        {
            try
            {
                var eventExtract = _masterPatientIndexRepository.GetAll().ToList().OrderBy(x => x.sxFirstName);
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
    }
}
