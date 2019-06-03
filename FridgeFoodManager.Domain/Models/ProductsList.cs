using System;
using System.Collections.Generic;

namespace FridgeFoodManager.Domain.Models
{
    public class ProductsList
    {
        public ProductsList(int count, IEnumerable<Product> products)
        {
            Count = count;
            Products = products;
        }

        public int Count { get; }

        public IEnumerable<Product> Products { get; }

        public class Product
        {
            public Product(Guid id, string name, DateTime expirationDate, int maxDaysAfterOpening)
            {
                Id = id;
                Name = name;
                ExpirationDate = expirationDate;
                MaxDaysAfterOpening = maxDaysAfterOpening;
            }

            public Guid Id { get; }

            public string Name { get; }

            public DateTime ExpirationDate { get; }

            public int MaxDaysAfterOpening { get; }
        }
    }
}
