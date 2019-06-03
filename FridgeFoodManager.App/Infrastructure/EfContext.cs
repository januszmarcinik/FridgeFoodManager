using FridgeFoodManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace FridgeFoodManager.App.Infrastructure
{
    internal class EfContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("FridgeFoodManager");
        }
    }
}
