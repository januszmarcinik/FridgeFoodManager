using System;
using System.Linq;

namespace FridgeFoodManager.Domain
{
    public interface IProductsRepository
    {
        IQueryable<Product> Query();

        Product GetById(Guid id);

        void Add(Product product);

        void Update(Product product);

        void Remove(Product product);
    }
}
