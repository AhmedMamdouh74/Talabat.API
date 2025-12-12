using Domain.Contracts;
using Domain.Entities.Identity;
using infrastructure.Identity;
using infrastructure.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace infrastructure.Data.DI
{
    public static class infrastructureLayerConfigurations
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));

            });
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = configuration.GetConnectionString("redis");
                if (string.IsNullOrEmpty(connection))
                {
                    throw new InvalidOperationException("Redis connection string is not configured.");
                }

                return ConnectionMultiplexer.Connect(connection);
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped(typeof( IUnitOfWork), typeof( UnitOfWork));

            // FIX: Add the required Identity services using AddIdentityCore and AddRoles
            //services.AddIdentityCore<AppUser>(options =>
            //{
            //    options.Password.RequiredUniqueChars = 2;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequiredLength = 6;
            //})
            //.AddRoles<IdentityRole>()
            //.AddEntityFrameworkStores<AppIdentityDbContext>();
            //services.AddIdentity<AppUser, IdentityRole>(options =>
            //{
            //    options.Password.RequiredUniqueChars = 2;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequiredLength = 6;
            //});

            return services;
        }
    }
}
