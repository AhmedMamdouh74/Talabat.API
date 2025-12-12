using Domain.Entities;
using Domain.Entities.Order_Aggregate;
using System.Text.Json;

namespace infrastructure.Data
{
    public static class StoreDataSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.categories.Any())
            {
                var jsonData = await File.ReadAllTextAsync("../infrastructure/Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(jsonData) ?? new List<Category>();

                context.categories.AddRange(categories);

            }
            if (!context.products.Any())
            {
                var jsonData = await File.ReadAllTextAsync("../infrastructure/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(jsonData) ?? new List<Product>();
                context.products.AddRange(products);

            }
            if (!context.Brands.Any())
            {
                var jsonData = await File.ReadAllTextAsync("../infrastructure/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<Brand>>(jsonData) ?? new List<Brand>();
                context.Brands.AddRange(brands);

            }
            if (!context.DeliveryMethods.Any())
            {
                var jsonData = await File.ReadAllTextAsync("../infrastructure/Data/DataSeed/delivery.json");
                var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(jsonData) ?? new List<DeliveryMethod>();
                context.DeliveryMethods.AddRange(deliveries);

            }
            await context.SaveChangesAsync();
        }
    }
}
