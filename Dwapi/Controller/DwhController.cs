using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dwapi.Controller
{
    public class DwhController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;

        public DwhController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IActionResult> Load([FromBody]LoadFromEmrCommand request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _mediator.Send(request, HttpContext.RequestAborted);
            return Ok(result);
        }
    }
}
