using Application.Helpers;
using Application.Mapping;
using Application.Services;
using Application.Services.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI
{
    public static class ApplicationLayerConfigurations
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)

        {
            // Add application services and configurations here
            services.AddAutoMapper(op => op.AddProfile<MapConfig>());
            services.AddScoped<ProductPictureUrlReslover>();
            services.AddScoped<IOrderService,OrderService>();
            
            return services;
        }
    }
}
