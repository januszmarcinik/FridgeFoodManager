using System;

namespace FridgeFoodManager.Api.Domain
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int MaxDaysAfterOpening { get; set; }
    }
}
