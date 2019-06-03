namespace FridgeFoodManager.Domain.Commands.OpenProduct
{
    internal class OpenProductCommandHandler
    {
        private readonly IProductsRepository _productsRepository;

        public OpenProductCommandHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public Result Handle(OpenProductCommand command)
        {
            var product = _productsRepository.GetById(command.Id);
            if (product == null)
            {
                return Result.Failure("Product with given ID does not exist.");
            }

            product.Open();

            _productsRepository.Update(product);

            return Result.Success();
        }
    }
}
