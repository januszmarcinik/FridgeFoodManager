using System.Linq;

namespace FridgeFoodManager.Domain.Queries.GetAllProducts
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
                .Select(p => new AllProductsList.Product(p.Id, p.Name, p.ExpirationDate, p.MaxDaysAfterOpening))
                .ToList();

            return new AllProductsList(products.Count, products);
        }
    }
}
