using LibraryManagementSystem.Domain.Interfaces;
using LibraryManagementSystem.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Api.Extensions
{
    public static class DataSeederExtensions
    {
        public static async Task SeedRolesAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = [ "Admin", "Member", "Librarian" ];
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdminUserAsync(this IApplicationBuilder app, IConfiguration config)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var adminUser = await userManager.FindByEmailAsync("admin@example.com");

                var email = config["AdminSeed:Email"]!;
                var password = config["AdminSeed:Password"]!;

                if(adminUser == null)
                {
                    var newAdminUser = new ApplicationUser
                    { 
                        UserName = "admin", 
                        Email = email,
                        MemberId = null
                    };

                    var result = await userManager.CreateAsync(newAdminUser, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newAdminUser, "Admin");
                    }
                }
            }
        }
    }
}
