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

        public IQueryable<Product> Query()
            => _context.Products.AsQueryable();

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

        public void Remove(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }

        internal void InitializeMockData()
        {
            var startDate = SystemTime.Now;
            var mockedProducts = new List<Product>()
            {
                new Product {Id = Guid.NewGuid(), Name = "Mleko", ExpirationDate = startDate.AddDays(10), MaxDaysAfterOpening = 3},
                new Product {Id = Guid.NewGuid(), Name = "Jajka", ExpirationDate = startDate.AddYears(1), MaxDaysAfterOpening = 60},
                new Product {Id = Guid.NewGuid(), Name = "Jogurt", ExpirationDate = startDate.AddDays(-5), MaxDaysAfterOpening = 1},
                new Product {Id = Guid.NewGuid(), Name = "Drożdże", ExpirationDate = startDate.AddDays(-10), MaxDaysAfterOpening = 15},
                new Product {Id = Guid.NewGuid(), Name = "Ser żółty", ExpirationDate = startDate.AddMonths(1), MaxDaysAfterOpening = 10},
                new Product {Id = Guid.NewGuid(), Name = "Szynka", ExpirationDate = startDate.AddDays(14), MaxDaysAfterOpening = 15},
                new Product {Id = Guid.NewGuid(), Name = "Masło", ExpirationDate = startDate.AddMonths(3), MaxDaysAfterOpening = 30},
                new Product {Id = Guid.NewGuid(), Name = "Kanapka z tuńczykiem", ExpirationDate = startDate.AddDays(-7), MaxDaysAfterOpening = 1},
                new Product {Id = Guid.NewGuid(), Name = "Dorsz", ExpirationDate = startDate.AddDays(1), MaxDaysAfterOpening = 2, OpenedAt = startDate.AddDays(-4)},
                new Product {Id = Guid.NewGuid(), Name = "Ogórki kiszone", ExpirationDate = startDate.AddDays(0), MaxDaysAfterOpening = 5},
            };

            _context.AddRange(mockedProducts);
            _context.SaveChanges();
        }
    }
}
