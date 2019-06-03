using System;

namespace FridgeFoodManager.Domain.Commands.AddProduct
{
    internal class AddProductCommandHandler
    {
        private readonly IProductsRepository _productsRepository;

        public AddProductCommandHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public Result Handle(AddProductCommand command)
        {
            if (command.ExpirationDate < SystemTime.Now)
            {
                return Result.Failure("Expiration date should be greater than current date.");
            }

            if (string.IsNullOrWhiteSpace(command.Name))
            {
                return Result.Failure("Product name can not be empty.");
            }

            if (command.MaxDaysAfterOpening < 0)
            {
                return Result.Failure("Max days after opening should be greater than zero.");
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                ExpirationDate = command.ExpirationDate,
                MaxDaysAfterOpening = command.MaxDaysAfterOpening
            };

            _productsRepository.Add(product);

            return Result.Success();
        }
    }
}
