using System.Collections.Generic;
using System.Linq;
using FridgeFoodManager.Api.Domain;

namespace FridgeFoodManager.Api.Infrastructure
{
    internal class ProductsRepository : IProductsRepository
    {
        private readonly EfContext _context;

        public ProductsRepository(EfContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
            => _context.Products.ToList();

        public void Add(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }
    }
}
