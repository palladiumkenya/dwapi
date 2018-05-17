using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/DwhExtracts")]
    public class DwhExtractsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;

        public DwhExtractsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("load")]
        public async Task<IActionResult> Load([FromBody]LoadFromEmrCommand request)
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
                /*var eventExtract = _mediator.GetStatus(id);
                if (null == eventExtract)
                    return NotFound();*/

                return Ok();
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
