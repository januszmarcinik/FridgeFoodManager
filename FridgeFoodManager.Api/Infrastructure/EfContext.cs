using System;
using FridgeFoodManager.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace FridgeFoodManager.Api.Infrastructure
{
    internal class EfContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        }
    }
}
