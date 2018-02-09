using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/RegistryManager")]
    public class RegistryManagerController : Microsoft.AspNetCore.Mvc.Controller
    {
        // GET: api/RegistryManager
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RegistryManager/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/RegistryManager
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/RegistryManager/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
