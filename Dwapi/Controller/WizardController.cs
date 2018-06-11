using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/Wizard")]
    public class WizardController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IWritableOptions<ConnectionStrings> _options;
        private readonly IAppDatabaseManager _appDatabaseManager;

        public WizardController(IWritableOptions<ConnectionStrings> options, IAppDatabaseManager appDatabaseManager)
        {
            _options = options;
            _appDatabaseManager = appDatabaseManager;
        }

        // GET: api/wizard/db
        [HttpGet("db")]
        public virtual IActionResult GetDb()
        {

            try
            {
                var appDatabase = _appDatabaseManager.ReadConnection(_options.Value.SpotConnection, _options.Value.Provider);

                if (null == appDatabase)
                    return NotFound();

                return Ok(appDatabase);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Database setting";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpPost("verifyserver")]
        public IActionResult VerifyServer([FromBody] AppDatabase appDatabase)
        {
            if (null == appDatabase)
                return BadRequest();

            try
            {
                var connected = _appDatabaseManager.VerfiyServer(appDatabase);

                if (connected)
                    return Ok(true);

                throw new Exception("Database could not connect");
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpPost("verifydb")]
        public IActionResult VerifyDb([FromBody] AppDatabase appDatabase)
        {
            if (null == appDatabase)
                return BadRequest();

            try
            {
                var connected = _appDatabaseManager.Verfiy(appDatabase);

                if (connected)
                    return Ok(true);

                throw new Exception("Database could not connect");
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpPost("db")]
        public IActionResult SaveConnection([FromBody] AppDatabase appDatabase)
        {
            if (null == appDatabase)
                return BadRequest();
            try
            {
                _options.Update(opt =>
                {
                    opt.Provider = appDatabase.Provider;
                    opt.SpotConnection = _appDatabaseManager.BuildConncetion(appDatabase);
                });

                return Ok(true);
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
