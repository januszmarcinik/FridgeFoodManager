using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FridgeFoodManager.Domain;
using FridgeFoodManager.Domain.Queries.Shopping;
using Xunit;

namespace FridgeFoodManager.Tests.Unit
{
    public class ShoppingQueryTests
    {
        [Fact]
        public void RunQuery_BasedOnCurrentFridgeStatusAndProductsHistory_ShouldReturnShoppingList()
        {
            var startDate = SystemTime.Now;
            var repository = ServiceFactory.ProductsRepository;
            foreach (var product in GetMockedProducts(startDate))
            {
                repository.Add(product);
            }

            var result = new ShoppingQueryHandler(repository).Handle(new ShoppingQuery());

            var productNames = result.Products.Select(x => x.Name);
            productNames.Should().BeEquivalentTo("Mleko", "Jogurt", "Drożdże", "Kanapka z tuńczykiem", "Dorsz", "Ogórki kiszone");
        }

        private static IEnumerable<Product> GetMockedProducts(DateTime startDate)
        {
            yield return new Product {Id = Guid.NewGuid(), Name = "Mleko", ExpirationDate = startDate.AddDays(4), MaxDaysAfterOpening = 3};
            yield return new Product {Id = Guid.NewGuid(), Name = "Jajka", ExpirationDate = startDate.AddYears(1), MaxDaysAfterOpening = 60};
            yield return new Product {Id = Guid.NewGuid(), Name = "Jogurt", ExpirationDate = startDate.AddDays(-5), MaxDaysAfterOpening = 1};
            yield return new Product {Id = Guid.NewGuid(), Name = "Drożdże", ExpirationDate = startDate.AddDays(-10), MaxDaysAfterOpening = 15, RemovedAt = startDate.AddDays(-1)};
            yield return new Product {Id = Guid.NewGuid(), Name = "Ser żółty", ExpirationDate = startDate.AddMonths(1), MaxDaysAfterOpening = 10};
            yield return new Product {Id = Guid.NewGuid(), Name = "Szynka", ExpirationDate = startDate.AddDays(14), MaxDaysAfterOpening = 15};
            yield return new Product {Id = Guid.NewGuid(), Name = "Masło", ExpirationDate = startDate.AddMonths(3), MaxDaysAfterOpening = 30};
            yield return new Product {Id = Guid.NewGuid(), Name = "Kanapka z tuńczykiem", ExpirationDate = startDate.AddDays(-7), MaxDaysAfterOpening = 1};
            yield return new Product {Id = Guid.NewGuid(), Name = "Dorsz", ExpirationDate = startDate.AddDays(1), MaxDaysAfterOpening = 2, OpenedAt = startDate.AddDays(-4)};
            yield return new Product {Id = Guid.NewGuid(), Name = "Ogórki kiszone", ExpirationDate = startDate.AddDays(0), MaxDaysAfterOpening = 5};
            yield return new Product {Id = Guid.NewGuid(), Name = "Śmietana", ExpirationDate = startDate.AddDays(3), MaxDaysAfterOpening = 4, RemovedAt = startDate.AddDays(-9) };
        }
    }
}
