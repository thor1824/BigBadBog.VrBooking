using System.Collections.Generic;
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

            Category cat1 = new Category
            {
                Name = "VR Room",
                Description = "Rooms on EASV",
                ImgUrl = "https://i.pinimg.com/originals/ec/93/0f/ec930fe87c4c1391de4946351d0967c2.jpg"
            };
            Category cat2 = new Category
            {
                Name = "VR & AR Equipment",
                Description = "VR & AR Equipment on EASV",
                ImgUrl = "https://www.avrspot.com/wp-content/uploads/2019/06/AR-2-1024x629-1024x585.jpg"
            };
            Category cat3 = new Category
            {
                Name = "Other Equipment",
                Description = "Drones or Camera equipment on EASV",
                ImgUrl = "https://149355317.v2.pressablecdn.com/wp-content/uploads/2018/09/Mavic-2-Pro-Image-1.jpg"
            };
            List<Category> cats = new List<Category> { cat1, cat2, cat3 };

            string desciption = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

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