using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Versionning.Core.Net.Controllers._1._0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            var result = await Task.FromResult("Hello version 1.0 de mon api");
            return Ok(result);  
        }
    }
}
