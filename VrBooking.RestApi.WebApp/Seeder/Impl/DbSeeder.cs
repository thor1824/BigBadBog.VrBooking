using VrBooking.Core.Entity;
using VrBooking.Infrastructure;

namespace VrBooking.RestApi.WebApp.Seeder.Impl
{
    internal class DbSeeder : IDbSeeder
    {
        public void Seed(VrBookingContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            Category catEquip = new Category()
            {
                Name = "Equipment"
            };

            Category catUsers = new Category()
            {
                Name = "Users"
            };

            Category catCategories = new Category()
            {
                Name = "Categories"
            };

            Product prodEquip = new Product()
            {
                Category = catEquip,
                Description = "Its good",
                Name = "HTC VIVE",
            };

            Product prodEquip2 = new Product()
            {
                Category = catEquip,
                Description = "Its very good",
                Name = "Oculus Quest"
            };
            
            Product prodEquip3 = new Product()
            {
                Category = catEquip,
                Description = "Its insanely good",
                Name = "Valve Index"
            };
            
            Product prodEquip4 = new Product()
            {
                Category = catEquip,
                Description = "Its insanely good",
                Name = "HTC VIVE"
            };
            
            Product prodEquip5 = new Product()
            {
                Category = catEquip,
                Description = "Its kinda good",
                Name = "Oculus Rift"
            };

            ctx.Products.Add(prodEquip);
            ctx.Products.Add(prodEquip2);
            ctx.Products.Add(prodEquip3);
            ctx.Products.Add(prodEquip4);
            ctx.Products.Add(prodEquip5);

            ctx.Categories.Add(catCategories);
            ctx.Categories.Add(catEquip);
            ctx.Categories.Add(catUsers);

            ctx.SaveChanges();
        }
    }
}