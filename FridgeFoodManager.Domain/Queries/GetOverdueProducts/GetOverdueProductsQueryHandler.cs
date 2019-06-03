using System.Linq;
using FridgeFoodManager.Domain.Models;

namespace FridgeFoodManager.Domain.Queries.GetOverdueProducts
{
    internal class GetOverdueProductsQueryHandler
    {
        private readonly IProductsRepository _productsRepository;

        public GetOverdueProductsQueryHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public ProductsList Handle(GetOverdueProductsQuery query)
        {
            var products = _productsRepository
                .GetAll()
                .Where(x => x.IsSuitableForConsumption == false)
                .Select(p => new ProductsList.Product(p.Id, p.Name, p.ExpirationDate, p.MaxDaysAfterOpening))
                .ToList();

            return new ProductsList(products.Count, products);
        }
    }
}
