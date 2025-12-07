using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace infrastructure.Identity.DataSeed
{
    public static class AppIdentityDataSeed
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<AppUser>>();
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(nameof(AppIdentityDataSeed));
            string[] roles = new[] { "Admin", "Customer" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    logger.LogInformation($"Created role: {role}");
                }
            }
            var adminEmail = "ahmed.eliwa@gmail.com";
            var adminPassword = "Admin@123";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new AppUser
                {
                    DisplayName = "Ahmed Mamdouh",
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    logger.LogInformation($"Created admin user: {adminEmail}");
                }

            }
        }
    }
}
