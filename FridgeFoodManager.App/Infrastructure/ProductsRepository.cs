using System;
using System.Collections.Generic;
using System.Linq;
using FridgeFoodManager.Domain;

namespace FridgeFoodManager.App.Infrastructure
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

        public Product GetById(Guid id)
            => _context.Products.SingleOrDefault(x => x.Id == id);

        public void Add(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }
    }
}
