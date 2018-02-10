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
    [Route("api/RegistryManager")]
    public class RegistryManagerController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly IRegistryManagerService _registryManagerService;
        public RegistryManagerController(IRegistryManagerService registryManagerService)
        {
            _registryManagerService = registryManagerService;
        }

        // GET: api/RegistryManager/default
        [HttpGet("default")]
        public IActionResult Default()
        {
            try
            {
                var centralRegistry = _registryManagerService.GetDefault();

                if (null == centralRegistry)
                    return NotFound();

                return Ok(centralRegistry);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(CentralRegistry)}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // POST: api/RegistryManager
        [HttpPost]
        public IActionResult Post([FromBody]CentralRegistry entity)
        {
            if (null == entity)
                return BadRequest();

            if (entity.Id.IsNullOrEmpty())
                entity.Id = LiveGuid.NewGuid();

            try
            {
                _registryManagerService.SaveDefault(entity);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error saving {nameof(CentralRegistry)}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // POST: api/RegistryManager/verify
        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody]CentralRegistry entity)
        {
            if (null == entity)
                return BadRequest();

            try
            {
                var verified =await _registryManagerService.Verify(entity);
                return Ok(verified);
            }
            catch (Exception e)
            {
                var msg = $"Error veryfying {nameof(CentralRegistry)}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
