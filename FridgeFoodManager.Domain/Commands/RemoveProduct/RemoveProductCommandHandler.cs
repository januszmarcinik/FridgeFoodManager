namespace FridgeFoodManager.Domain.Commands.RemoveProduct
{
    internal class RemoveProductCommandHandler
    {
        private readonly IProductsRepository _productsRepository;

        public RemoveProductCommandHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public Result Handle(RemoveProductCommand command)
        {
            var product = _productsRepository.GetById(command.Id);
            if (product == null)
            {
                return Result.Failure("Product with given ID does not exist.");
            }

            _productsRepository.Remove(product);

            return Result.Success();
        }
    }
}
