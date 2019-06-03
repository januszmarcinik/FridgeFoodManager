using System;
using System.Linq;
using FluentAssertions;
using FridgeFoodManager.Domain;
using FridgeFoodManager.Domain.Commands.AddProduct;
using Xunit;

namespace FridgeFoodManager.Tests.Unit
{
    public class AddProductCommandTests
    {
        [Fact]
        public void AddProduct_IfIsCorrect_ShouldSuccess()
        {
            SystemTime.NowFunc = () => new DateTime(2019, 4, 1);

            var command = new AddProductCommand
            {
                Name = "Mleko",
                ExpirationDate = new DateTime(2019, 4, 17),
                MaxDaysAfterOpening = 2
            };

            var handler = new AddProductCommandHandler(ServiceFactory.ProductsRepository);
            var result = handler.Handle(command);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void AddProduct_IfNameIsEmpty_ShouldFailure()
        {
            SystemTime.NowFunc = () => new DateTime(2019, 4, 1);

            var command = new AddProductCommand
            {
                Name = "",
                ExpirationDate = new DateTime(2019, 4, 17),
                MaxDaysAfterOpening = 2
            };

            var handler = new AddProductCommandHandler(ServiceFactory.ProductsRepository);
            var result = handler.Handle(command);

            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().BeEquivalentTo("Product name can not be empty.");
        }

        [Fact]
        public void AddProduct_IfMaxDaysAfterOpeningIsLessThanZero_ShouldFailure()
        {
            SystemTime.NowFunc = () => new DateTime(2019, 4, 1);

            var command = new AddProductCommand
            {
                Name = "Mleko",
                ExpirationDate = new DateTime(2019, 4, 17),
                MaxDaysAfterOpening = -1
            };

            var handler = new AddProductCommandHandler(ServiceFactory.ProductsRepository);
            var result = handler.Handle(command);

            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().BeEquivalentTo("Max days after opening should be greater than zero.");
        }

        [Fact]
        public void AddProduct_IfExpirationDateIsLessThanCurrent_ShouldFailure()
        {
            SystemTime.NowFunc = () => new DateTime(2019, 4, 20);

            var command = new AddProductCommand
            {
                Name = "Mleko",
                ExpirationDate = new DateTime(2019, 4, 17),
                MaxDaysAfterOpening = 2
            };

            var handler = new AddProductCommandHandler(ServiceFactory.ProductsRepository);
            var result = handler.Handle(command);

            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().BeEquivalentTo("Expiration date should be greater than current date.");
        }

        [Fact]
        public void AddProduct_IfIsCorrect_ShouldIncreaseProductsCount()
        {
            SystemTime.NowFunc = () => new DateTime(2019, 4, 1);

            var command = new AddProductCommand
            {
                Name = "Mleko",
                ExpirationDate = new DateTime(2019, 4, 17),
                MaxDaysAfterOpening = 2
            };

            var productsRepository = ServiceFactory.ProductsRepository;
            productsRepository.GetAll().Count().Should().Be(0);

            var handler = new AddProductCommandHandler(productsRepository);
            handler.Handle(command);

            productsRepository.GetAll().Count().Should().Be(1);
        }
    }
}
