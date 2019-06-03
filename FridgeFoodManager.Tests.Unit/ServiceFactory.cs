using FridgeFoodManager.Domain;

namespace FridgeFoodManager.Tests.Unit
{
    public class ServiceFactory
    {
        public static IProductsRepository ProductsRepository => new FakeProductsRepository();
    }
}
