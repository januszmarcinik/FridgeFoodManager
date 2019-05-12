using FridgeFoodManager.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace FridgeFoodManager.Api.Infrastructure
{
    internal class EfContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public EfContext(DbContextOptions options) : base(options)
        {
        }
    }
}
