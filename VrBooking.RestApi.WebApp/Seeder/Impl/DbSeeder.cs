using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using VrBooking.Core.Entity;
using VrBooking.Infrastructure;
using VrBooking.RestApi.WebApp.Helper;

namespace VrBooking.RestApi.WebApp.Seeder.Impl
{
    internal class DbSeeder : IDbSeeder
    {
        private readonly IAuthenticationHelper _authenticationHelper;
        public DbSeeder(IAuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }
        public void Seed(VrBookingContext ctx)
        {


            if (ctx.Database.IsSqlServer())
            {
                ctx.Database.ExecuteSqlCommand("DROP DATABASE VrBooking-db");
            } else {
                ctx.Database.EnsureDeleted();
            }

            ctx.Database.EnsureCreated();

            string paswordString = "123456";

            _authenticationHelper.CreatePasswordHash(paswordString, out byte[] passwordHash, out byte[] passwordSalt);

            UserInfo userInfo1 = new UserInfo()
           {
               Address = "BjørneBy",
               Email = "thor666@easv365.dk",
               FirstName = "Thorbjørn",
               LastName = "Damkjær",
               PhoneNumber = "12345678"
           };

           LoginUser user1 = new LoginUser()
           {
               IsActivated = true,
               IsAdmin = false,
               PasswordHash = passwordHash,
               PasswordSalt = passwordSalt,
               UserInfo = userInfo1,
           };

           UserInfo userInfo2 = new UserInfo()
           {
               Email = "ole123@easv365.dk",
               Address = "RoadRoad 1.",
               FirstName = "Christian",
               LastName = "Andersen",
               PhoneNumber = "12345678"
           };
            LoginUser user2 = new LoginUser()
            {
                IsActivated = true,
                IsAdmin = true,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserInfo = userInfo2

            };

            UserInfo userInfo3 = new UserInfo()
            {
                Email = "ole456@easv365.dk",
                Address = "StreetStreet 2.",
                FirstName = "Nijas",
                LastName = "Hansen",
                PhoneNumber = "12345678"
            };

            LoginUser user3 = new LoginUser()
            {
                IsActivated = true,
                IsAdmin = false,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserInfo = userInfo3


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

            Product product1 = new Product()
            {
                Name = "product",
                Category = cat1,
                Description = desciption
            };
            Product product2 = new Product()
            {
                Name = "product",
                Category = cat1,
                Description = desciption
            };
            Product product3 = new Product()
            {
                Name = "product",
                Category = cat1,
                Description = desciption
            };
            Product product4 = new Product()
            {
                Name = "product",
                Category = cat1,
                Description = desciption
            };
            Product product5 = new Product()
            {
                Name = "product",
                Category = cat1,
                Description = desciption
            };
            Product product6 = new Product()
            {
                Name = "product",
                Category = cat2,
                Description = desciption
            };
            Product product7 = new Product()
            {
                Name = "product",
                Category = cat3,
                Description = desciption
            };
            Product prodEquip2 = new Product()
            {
                Category = cat2,
                Description = "Its very good",
                Name = "Oculus Quest"
            };

            Product prodEquip3 = new Product()
            {
                Category = cat2,
                Description = "Its insanely good",
                Name = "Valve Index"
            };

            Product prodEquip4 = new Product()
            {
                Category = cat2,
                Description = "Its insanely good",
                Name = "HTC VIVE"
            };

            Product prodEquip5 = new Product()
            {
                Category = cat3,
                Description = "Its kinda good",
                Name = "Oculus Rift"
            };
            Product prodEquip = new Product()
            {
                Category = cat2,
                Description = "Its good",
                Name = "HTC VIVE",
            };
            List<Product> prods = new List<Product> { product1, product2, product3, product4, product5, product6, product7, prodEquip, prodEquip2, prodEquip3, prodEquip4, prodEquip5 };
            DateTime s1 = new DateTime(2019, 12, 6, 15, 0, 0, DateTimeKind.Utc);
            DateTime e1 = new DateTime(2019, 12, 6, 16, 0, 0, DateTimeKind.Utc);
            DateTime s2 = new DateTime(2019, 12, 7, 13, 0, 0, DateTimeKind.Utc);
            DateTime e2 = new DateTime(2019, 12, 7, 19, 0, 0, DateTimeKind.Utc);

            List<BookingOrder> list = new List<BookingOrder>{
                new BookingOrder
                {
                    User = userInfo1,
                    Product = product1,
                    StartTimeOfBooking = s1,
                    EndTimeOfBooking = e1,

                },
                new BookingOrder
                {
                    User = userInfo1,
                    Product = product1,
                    StartTimeOfBooking = s2,
                    EndTimeOfBooking = e2,

                }
            };

            ctx.AddRange(prods);
            ctx.AddRange(cats);
            ctx.AddRange(new List<LoginUser> { user1, user2, user3 });
            ctx.AddRange(list);
            ctx.SaveChanges();

        }

    }
}
