using System.Linq;
using FridgeFoodManager.Domain.Models;

namespace FridgeFoodManager.Domain.Queries.GetOpenProducts
{
    internal class GetOpenProductsQueryHandler
    {
        private readonly IProductsRepository _productsRepository;

        public GetOpenProductsQueryHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public ProductsList Handle(GetOpenProductsQuery query)
        {
            var products = _productsRepository
                .Query()
                .Where(x => x.OpenedAt.HasValue)
                .Select(p => new ProductsList.Product(p.Id, p.Name, p.ExpirationDate, p.MaxDaysAfterOpening))
                .ToList();

            return new ProductsList(products.Count, products);
        }
    }
}
