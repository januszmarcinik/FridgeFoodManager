using Microsoft.AspNetCore.Mvc;

namespace FridgeFoodManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
            => Ok("Ok");
    }
}