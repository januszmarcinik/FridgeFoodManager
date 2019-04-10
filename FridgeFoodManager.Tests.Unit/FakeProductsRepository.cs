using System.Collections.Generic;
using FridgeFoodManager.Api.Domain;

namespace FridgeFoodManager.Tests.Unit
{
    internal class FakeProductsRepository : IProductsRepository
    {
        private readonly List<Product> _products;

        public FakeProductsRepository()
        {
            _products = new List<Product>();
        }

        public IEnumerable<Product> GetAll()
            => _products;

        public void Add(Product product)
        {
            _products.Add(product);
        }
    }
}
