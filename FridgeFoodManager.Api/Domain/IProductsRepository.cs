using System.Collections.Generic;

namespace FridgeFoodManager.Api.Domain
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAll();

        void Add(Product product);
    }
}
