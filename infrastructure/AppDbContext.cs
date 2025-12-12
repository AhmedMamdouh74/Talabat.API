using Domain.Entities;
using Domain.Entities.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // based on reflect
        }

    }
}
