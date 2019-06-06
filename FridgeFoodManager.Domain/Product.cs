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

        public DateTime? RemovedAt { get; set; }

        public bool IsSuitableForConsumption => IsSuitableForConsumptionForDate(SystemTime.Now);
            
        public void Open()
        {
            OpenedAt = SystemTime.Now;
        }

        public void Remove()
        {
            RemovedAt = SystemTime.Now;
        }

        public bool IsSuitableForConsumptionForDate(DateTime date) 
            => ExpirationDate > date &&
               RemovedAt.HasValue == false &&
               (OpenedAt.HasValue == false || OpenedAt.Value.AddDays(MaxDaysAfterOpening) > date);

        public bool WasRemovedNotBefore(int numberOfDays)
            => RemovedAt.HasValue && RemovedAt.Value.AddDays(-numberOfDays) > SystemTime.Now;
    }
}
