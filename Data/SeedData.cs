using B2CEcommerceApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2CEcommerceApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate(); // apply migrations

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // 1. Ensure Admin Role
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // 2. Ensure Admin User
            string adminEmail = "admin@shop.com";
            string adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // 3. Seed Categories
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Electronics" },
                    new Category { Name = "Clothing" },
                    new Category { Name = "Books" }
                );
                await context.SaveChangesAsync();
            }

            // 4. Seed Products
            if (!context.Products.Any())
            {
                var electronics = context.Categories.First(c => c.Name == "Electronics");
                var clothing = context.Categories.First(c => c.Name == "Clothing");
                var books = context.Categories.First(c => c.Name == "Books");

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Laptop",
                        Description = "High-performance laptop",
                        Price = 1200,
                        Stock = 5,
                        ImageUrl = "https://via.placeholder.com/150",
                        CategoryId = electronics.Id
                    },
                    new Product
                    {
                        Name = "Smartphone",
                        Description = "Latest model smartphone",
                        Price = 800,
                        Stock = 10,
                        ImageUrl = "https://via.placeholder.com/150",
                        CategoryId = electronics.Id
                    },
                    new Product
                    {
                        Name = "T-Shirt",
                        Description = "Cotton t-shirt",
                        Price = 25,
                        Stock = 50,
                        ImageUrl = "https://via.placeholder.com/150",
                        CategoryId = clothing.Id
                    },
                    new Product
                    {
                        Name = "Novel",
                        Description = "Bestselling novel",
                        Price = 15,
                        Stock = 20,
                        ImageUrl = "https://via.placeholder.com/150",
                        CategoryId = books.Id
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
