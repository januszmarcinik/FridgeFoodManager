using FridgeFoodManager.Api.Domain;
using FridgeFoodManager.Api.Queries.GetAllProducts;
using FridgeFoodManager.Common;
using Microsoft.AspNetCore.Mvc;

namespace FridgeFoodManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public QueriesController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet("get-all-products")]
        public IActionResult GetAllProducts()
        {
            var products = new GetAllProductsQueryHandler(_productsRepository).Handle(new GetAllProductsQuery());

            var queryResult = QueryResultSchema.FromQueryResult(products);

            return Ok(queryResult);
        }
    }
}