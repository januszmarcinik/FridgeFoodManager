using System.Collections.Generic;

namespace FridgeFoodManager.Domain.Models
{
    public class ShoppingList
    {
        public ShoppingList(int count, IEnumerable<Product> products)
        {
            Count = count;
            Products = products;
        }

        public int Count { get; }

        public IEnumerable<Product> Products { get; }

        public class Product
        {
            public Product(string name, string reasonForShopping)
            {
                Name = name;
                ReasonForShopping = reasonForShopping;
            }

            public string Name { get; }

            public string ReasonForShopping { get; }
        }
    }
}
