using System;
using System.Linq;
using FridgeFoodManager.Domain.Models;

namespace FridgeFoodManager.Domain.Queries.Shopping
{
    internal class ShoppingQueryHandler
    {
        private readonly IProductsRepository _productsRepository;

        public ShoppingQueryHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public ShoppingList Handle(ShoppingQuery query)
        {
            var products = _productsRepository
                .Query(onlyNotRemoved: false)
                .Where(x =>
                    IsNotRemovedAndIsNotSuitableForConsumptionEvenNextWeek(x) ||
                    WasRemovedNotBeforeSevenDaysAgo(x)
                )
                .ToList()
                .Select(p =>
                {
                    if (IsNotRemovedAndIsNotSuitableForConsumptionEvenNextWeek(p))
                    {
                        if (p.IsSuitableForConsumption && !p.IsSuitableForConsumptionForDate(SystemTime.Now.AddDays(7)))
                        {
                            return new ShoppingList.Product(p.Name,
                                $"Product won't be suitable for consumption for {GetDaysDifferenceFromNow(p.ExpirationDate)} days.");
                        }

                        if (!p.IsSuitableForConsumption)
                        {
                            return new ShoppingList.Product(p.Name,
                                $"Product isn't suitable for consumption since {GetDaysDifferenceFromNow(p.ExpirationDate)} days.");
                        }
                    }

                    if (WasRemovedNotBeforeSevenDaysAgo(p))
                    {
                        return new ShoppingList.Product(p.Name,
                            $"Product was removed from fridge {GetDaysDifferenceFromNow(p.RemovedAt.Value)} days ago. Probably you will need it again.");
                    }

                    return new ShoppingList.Product(string.Empty, string.Empty);
                })
                .ToList();

            return new ShoppingList(products.Count, products);
        }

        private static bool IsNotRemovedAndIsNotSuitableForConsumptionEvenNextWeek(Product product)
            => !product.RemovedAt.HasValue && (!product.IsSuitableForConsumption || !product.IsSuitableForConsumptionForDate(SystemTime.Now.AddDays(7)));

        private static bool WasRemovedNotBeforeSevenDaysAgo(Product product)
            => product.RemovedAt.HasValue && product.RemovedAt.Value.AddDays(7) > SystemTime.Now;

        private static int GetDaysDifferenceFromNow(DateTime date)
            => Math.Abs((date - new DateTime(SystemTime.Now.Year, SystemTime.Now.Month, SystemTime.Now.Day)).Days);
    }
}
