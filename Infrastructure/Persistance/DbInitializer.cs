
using Domain.Models.Identity;
using Domain.Models.OrderModels;
using Domain.Models.Product;
using Microsoft.AspNetCore.Identity;
using Persistance.Identity;

namespace Persistance
{
    public class DbInitializer
        (StoreDbContext context,
        StoreIdentityDBContext identityDBContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager

        )
        : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            try
            {
                //production -=> create datebase + seeding
                //development -=> seeding
                //if ((await context.Database.GetPendingMigrationsAsync()).Any() )
                //    await context.Database.MigrateAsync();

                if (!context.Set<ProductBrand>().Any())
                {
                    //read from file
                    var data = await File.ReadAllTextAsync(@"../Infrastructure/Persistance/seeding/brands.json");
                    // convert to c# object [deserilize] 
                    var objects = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                    // save to database
                    if (objects is not null && objects.Any())
                    {
                        context.Set<ProductBrand>().AddRange(objects);
                        await context.SaveChangesAsync();
                    }

                }
                if (!context.Set<ProductType>().Any())
                {
                    //read from file
                    var data = await File.ReadAllTextAsync(@"../Infrastructure/Persistance/seeding/types.json");
                    // convert to c# object [deserilize] 
                    var objects = JsonSerializer.Deserialize<List<ProductType>>(data);
                    // save to database
                    if (objects is not null && objects.Any())
                    {
                        context.Set<ProductType>().AddRange(objects);
                        await context.SaveChangesAsync();
                    }

                }
                if (!context.Set<Product>().Any())
                {
                    //read from file
                    var data = await File.ReadAllTextAsync(@"../Infrastructure/Persistance/seeding/products.json");
                    // convert to c# object [deserilize] 
                    var objects = JsonSerializer.Deserialize<List<Product>>(data);
                    // save to database
                    if (objects is not null && objects.Any())
                    {
                        context.Set<Product>().AddRange(objects);
                        await context.SaveChangesAsync();
                    }
                }
                if (!context.Set<DeliveryMethod>().Any())
                {
                    //read from file
                    var data = await File.ReadAllTextAsync(@"../Infrastructure/Persistance/seeding/delivery.json");
                    // convert to c# object [deserilize] 
                    var objects = JsonSerializer.Deserialize<List<DeliveryMethod>>(data);
                    // save to database
                    if (objects is not null && objects.Any())
                    {
                        context.Set<DeliveryMethod>().AddRange(objects);
                        await context.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task InitializeIdentityAsync()
        {
            //production -=> create datebase + seeding
            //development -=> seeding
            //if ((await identityDBContext.Database.GetPendingMigrationsAsync()).Any())
            //{
            //    await identityDBContext.Database.MigrateAsync();
            //}
            try
            {

                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(new IdentityRole("Admin"));

                if (!await roleManager.RoleExistsAsync("SuperAdmin"))
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

                if (!userManager.Users.Any())
                {
                    var SuperAdminUser = new ApplicationUser
                    {
                        DisplayName = "Super Admin",
                        Email = "youssefmoghazy55@gmail.com",
                        UserName = "superAdmin",
                        PhoneNumber = "1234567890"
                    };
                    var AdminUser = new ApplicationUser
                    {
                        DisplayName = "Admin",
                        Email = "youssefmoghazy16@gmail.com",
                        UserName = "Admin",
                        PhoneNumber = "1234567890"
                    };

                    await userManager.CreateAsync(SuperAdminUser, "P@ssw0rdSuperAdmin");
                    await userManager.CreateAsync(AdminUser, "P@ssw0rdAdmin");


                    await userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                    await userManager.AddToRoleAsync(AdminUser, "Admin");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
