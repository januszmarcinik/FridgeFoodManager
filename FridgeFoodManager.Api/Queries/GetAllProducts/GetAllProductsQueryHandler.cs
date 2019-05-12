using System.Linq;
using FridgeFoodManager.Api.Domain;

namespace FridgeFoodManager.Api.Queries.GetAllProducts
{
    internal class GetAllProductsQueryHandler
    {
        private readonly IProductsRepository _productsRepository;

        public GetAllProductsQueryHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public AllProductsList Handle(GetAllProductsQuery query)
        {
            var products = _productsRepository
                .GetAll()
                .Select(p => new AllProductsList.Product(p.Name, p.ExpirationDate, p.MaxDaysAfterOpening))
                .ToList();

            return new AllProductsList(products.Count, products);
        }
    }
}
