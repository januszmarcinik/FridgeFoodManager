using System;
using System.Collections.Generic;
using System.Linq;
using FridgeFoodManager.Domain;

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

        public Product GetById(Guid id)
            => _products.SingleOrDefault(x => x.Id == id);

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var oldProduct = GetById(product.Id);
            _products.Remove(oldProduct);
            _products.Add(product);
        }
    }
}
