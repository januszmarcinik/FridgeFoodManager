using System;

namespace FridgeFoodManager.Domain
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int MaxDaysAfterOpening { get; set; }

        public DateTime? OpenedAt { get; set; }

        public bool IsSuitableForConsumption
            => ExpirationDate < SystemTime.Now &&
               (OpenedAt.HasValue == false || OpenedAt.Value.AddDays(MaxDaysAfterOpening) < SystemTime.Now);

        public void Open()
        {
            OpenedAt = SystemTime.Now;
        }
    }
}
