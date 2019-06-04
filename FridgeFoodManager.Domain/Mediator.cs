using System;
using FridgeFoodManager.Domain.Commands.AddProduct;
using FridgeFoodManager.Domain.Commands.OpenProduct;
using FridgeFoodManager.Domain.Commands.RemoveProduct;
using FridgeFoodManager.Domain.Queries.GetAllProducts;
using FridgeFoodManager.Domain.Queries.GetOverdueProducts;

namespace FridgeFoodManager.Domain
{
    public class Mediator : IMediator
    {
        private readonly IProductsRepository _productsRepository;

        public Mediator(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public Result Command(ICommand command)
        {
            switch (command)
            {
                case AddProductCommand c:
                    return new AddProductCommandHandler(_productsRepository).Handle(c);
                case OpenProductCommand c:
                    return new OpenProductCommandHandler(_productsRepository).Handle(c);
                case RemoveProductCommand c:
                    return new RemoveProductCommandHandler(_productsRepository).Handle(c);
                default:
                    throw new Exception("Given command does not exists or it is not registered in mediator.");
            }
        }

        public T Query<T>(IQuery<T> query) where T : class
        {
            switch (query)
            {
                case GetAllProductsQuery q:
                    return new GetAllProductsQueryHandler(_productsRepository).Handle(q) as T;
                case GetOverdueProductsQuery q:
                    return new GetOverdueProductsQueryHandler(_productsRepository).Handle(q) as T;
                default:
                    throw new Exception("Given query does not exists or it is not registered in mediator.");
            }
        }
    }
}
