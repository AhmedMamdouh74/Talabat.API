
using Application.DI;
using Domain.Entities.Identity;
using infrastructure;
using infrastructure.Data;
using infrastructure.Data.DI;
using infrastructure.Identity.DataSeed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.API.Filters;
using Talabat.API.Middelware;
using Talabat.API.Responses;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            //builder.Services.AddIdentity<AppUser,IdentityRole>()
            //    .AddEntityFrameworkStores<infrastructure.Identity.AppIdentityDbContext>();  


            builder.Services.AddControllers(
          
            );
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //  builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ValidationErrorReponse
                    {
                        Message = "invalid data",
                        Errors = errors,
                        StatusCode = 400

                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
          
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ErrorHandlingMiddelware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.UseMiddleware<NotFoundEndpointMiddelware>();
            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var identityContext = scope.ServiceProvider.GetRequiredService<infrastructure.Identity.AppIdentityDbContext>();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreDataSeed.SeedAsync(context);
                await identityContext.Database.MigrateAsync();
                await AppIdentityDataSeed.SeedUsersAndRolesAsync(scope.ServiceProvider);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");


            }


            app.Run();
        }
    }
}
