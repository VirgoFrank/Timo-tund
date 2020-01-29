using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

       

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value, [FromServices]ServerPool servers)
        {
            
            servers.Add("http://localhost:" + value);
        }

       
    }
}
