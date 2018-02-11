using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Services.Psmart;
using Dwapi.SettingsManagement.Core.DTOs;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.AspNetCore.Http;
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

        // GET: api/ExtractManager/id/dockedtId
        [HttpGet("{id}")]
        public IActionResult GetStatus(Guid id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest();

            try
            {
//                var extracts = _psmartExtractService.GetAllByEmr(id, docketId).ToList();
//                return Ok(extracts);

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

       // POST: api/ExtractManager/load
        [HttpPost("load")]
        public IActionResult Load([FromBody]ExtractDbProtocolDTO entity)
        {
            if (null == entity)
                return BadRequest();

            if (!entity.IsValid())
                return BadRequest();

            try
            {
                //var verified = _psmartExtractService.Verfiy(entity.Extract, entity.DatabaseProtocol);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error parsing {nameof(Extract)}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

    }
}
