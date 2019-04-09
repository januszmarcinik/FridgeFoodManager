using FridgeFoodManager.Api.Commands;
using FridgeFoodManager.Api.Commands.AddProduct;
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

        [HttpGet("add-product")]
        public IActionResult AddProduct()
            => Ok(CommandSchema.FromCommand<AddProductCommand>());
    }
}