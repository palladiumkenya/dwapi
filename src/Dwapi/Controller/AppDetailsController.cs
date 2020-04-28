using Microsoft.AspNetCore.Mvc;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/appDetails")]
    public class AppDetailsController : Microsoft.AspNetCore.Mvc.Controller
    {
        // GET
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        // GET: api/appDetails/version
        [HttpGet("version")]
        public IActionResult Version()
        {
            var ver = GetType().Assembly.GetName().Version;
            string version = $"{ver.Major}.{ver.Minor}.{ver.Build}";
            return Ok(version);
        }
    }
}
