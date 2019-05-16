using FridgeFoodManager.Api.Commands.AddProduct;
using FridgeFoodManager.Api.Commands.OpenProduct;
using FridgeFoodManager.Api.Domain;
using FridgeFoodManager.Common;
using Microsoft.AspNetCore.Mvc;

namespace FridgeFoodManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public CommandsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet("test")]
        public IActionResult Test()
            => Ok("Ok");

        [HttpGet("add-product")]
        public IActionResult AddProduct()
            => Ok(CommandSchema.FromCommand<AddProductCommand>());

        [HttpPost("add-product")]
        public IActionResult AddProduct(AddProductCommand command)
        {
            var handler = new AddProductCommandHandler(_productsRepository);
            var result = handler.Handle(command);

            return HandleResult(result);
        }

        [HttpGet("open-product")]
        public IActionResult OpenProduct()
            => Ok(CommandSchema.FromCommand<OpenProductCommand>());

        [HttpPost("open-product")]
        public IActionResult OpenProduct(OpenProductCommand command)
        {
            var handler = new OpenProductCommandHandler(_productsRepository);
            var result = handler.Handle(command);

            return HandleResult(result);
        }

        private IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
            {
                return Accepted();
            }

            return BadRequest(result.ErrorMessage);
        }
    }
}