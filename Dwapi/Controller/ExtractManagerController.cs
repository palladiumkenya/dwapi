using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/ExtractManager")]
    public class ExtractManagerController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IExtractManagerService _extractManagerService;
        public ExtractManagerController(IExtractManagerService extractManagerService)
        {
            _extractManagerService = extractManagerService;
        }

        // GET: api/ExtractManager/id/dockedtId
        [HttpGet("{id}/{docketId}")]
        public IActionResult Get(Guid id,string docketId)
        {
            if (id.IsNullOrEmpty() || string.IsNullOrWhiteSpace(docketId))
                return BadRequest();

            try
            {
                var extracts = _extractManagerService.GetAllByEmr(id, docketId).ToList();
                return Ok(extracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(Extract)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // POST: api/ExtractManager
        [HttpPost]
        public IActionResult SaveExtract([FromBody] Extract entity)
        {
            if (null == entity)
                return BadRequest();

            if (entity.Id.IsNullOrEmpty())
                entity.Id = LiveGuid.NewGuid();

            try
            {
                _extractManagerService.Save(entity);
                return Ok(entity);
            }
            catch (Exception e)
            {
                var msg = $"Error saving {nameof(Extract)}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // POST: api/ExtractManager/verify
        [HttpPost("verify")]
        public IActionResult Verify([FromBody]ExtractDbProtocolDTO entity)
        {
            if (null == entity)
                return BadRequest();

            if (!entity.IsValid())
                return BadRequest();

            try
            {
                var verified =  _extractManagerService.Verfiy(entity.Extract,entity.DatabaseProtocol);
                return Ok(verified);
            }
            catch (Exception e)
            {
                var msg = $"Error parsing {nameof(Extract)} {_extractManagerService.GetDatabaseError()}";
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
                var verified = _extractManagerService.Verfiy(entity.Extract, entity.DatabaseProtocol);
                return Ok(verified);
            }
            catch (Exception e)
            {
                var msg = $"Error parsing {nameof(Extract)} {_extractManagerService.GetDatabaseError()}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

    }
}
