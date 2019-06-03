using System;
using System.Collections.Generic;

namespace FridgeFoodManager.Domain
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAll();

        Product GetById(Guid id);

        void Add(Product product);

        void Update(Product product);
    }
}
