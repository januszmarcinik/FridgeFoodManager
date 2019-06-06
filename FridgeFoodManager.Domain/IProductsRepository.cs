using System;
using System.Linq;

namespace FridgeFoodManager.Domain
{
    public interface IProductsRepository
    {
        IQueryable<Product> Query(bool onlyNotRemoved = true);

        Product GetById(Guid id);

        void Add(Product product);

        void Update(Product product);
    }
}
