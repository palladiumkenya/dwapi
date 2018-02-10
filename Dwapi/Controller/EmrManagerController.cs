using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/EmrManager")]
    public class EmrManagerController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEmrManagerService _emrManagerService;
        public EmrManagerController(IEmrManagerService emrManagerService)
        {
            _emrManagerService = emrManagerService;
        }

        // GET: api/EmrManager
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var list = _emrManagerService.GetAllEmrs().ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(EmrSystem)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // GET: api/EmrManager/count
        [HttpGet("count")]
        public IActionResult GetCount()
        {
            try
            {
                var count = _emrManagerService.GetEmrCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(EmrSystem)}(s) count";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // POST: api/EmrManager
        [HttpPost]
        public IActionResult SaveEmr([FromBody] EmrSystem entity)
        {
            if (null == entity)
                return BadRequest();

            if (entity.Id.IsNullOrEmpty())
                entity.Id = LiveGuid.NewGuid();

            try
            {
                _emrManagerService.SaveEmr(entity);
                return Ok(entity);
            }
            catch (Exception e)
            {
                var msg = $"Error saving {nameof(EmrSystem)}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // DELETE: api/EmrManager/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmr(Guid id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest();

            try
            {
                _emrManagerService.DeleteEmr(id);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error Deleteing {nameof(EmrSystem)}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // POST: api/EmrManager/protocol
        [HttpPost("protocol")]
        public IActionResult SaveProtocol([FromBody] DatabaseProtocol entity)
        {
            if (null == entity)
                return BadRequest();

            if (entity.Id.IsNullOrEmpty())
                entity.Id = LiveGuid.NewGuid();

            try
            {
                _emrManagerService.SaveProtocol(entity);
                return Ok(entity);
            }
            catch (Exception e)
            {
                var msg = $"Error saving {nameof(DatabaseProtocol)}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // DELETE: api/EmrManager/protocol/5
        [HttpDelete("protocol/{id}")]
        public IActionResult DeleteProtoco(Guid id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest();

            try
            {
                _emrManagerService.DeleteProtocol(id);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error Deleteing {nameof(DatabaseProtocol)}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }


        // POST: api/EmrManager/verify
        [HttpPost("verify")]
        public IActionResult Verify([FromBody]DatabaseProtocol entity)
        {
            if (null == entity)
                return BadRequest();

            try
            {
                var verified =  _emrManagerService.VerifyConnection(entity);
                return Ok(verified);
            }
            catch (Exception e)
            {
                var msg = $"Error veryfying {nameof(DatabaseProtocol)} {_emrManagerService.GetConnectionError()}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
