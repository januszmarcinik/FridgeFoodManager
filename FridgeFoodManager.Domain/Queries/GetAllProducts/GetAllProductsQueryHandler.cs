using System.Linq;
using FridgeFoodManager.Domain.Models;

namespace FridgeFoodManager.Domain.Queries.GetAllProducts
{
    internal class GetAllProductsQueryHandler
    {
        private readonly IProductsRepository _productsRepository;

        public GetAllProductsQueryHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public ProductsList Handle(GetAllProductsQuery query)
        {
            var products = _productsRepository
                .Query()
                .Select(p => new ProductsList.Product(p.Id, p.Name, p.ExpirationDate, p.MaxDaysAfterOpening))
                .ToList();

            return new ProductsList(products.Count, products);
        }
    }
}
