using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Versionning.Core.Net.Controllers._2._0
{
    [ApiVersion("2.0")]    
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            var result = await Task.FromResult("Hello version 2.0 de mon api");
            return Ok(result);
        }
    }
}
