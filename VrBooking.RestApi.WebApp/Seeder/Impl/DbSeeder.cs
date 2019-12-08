using System;
using System.Collections.Generic;
using VrBooking.Core.Entity;
using VrBooking.Infrastructure;
using VrBooking.RestApi.WebApp.Helper;

namespace VrBooking.RestApi.WebApp.Seeder.Impl
{
    internal class DbSeeder : IDbSeeder
    {
        private IAuthenticationHelper _authenticationHelper;
        public DbSeeder(IAuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }
        public void Seed(VrBookingContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            string paswordString = "123456";

            byte[] passwordHash, passwordSalt;
            _authenticationHelper.CreatePasswordHash(paswordString, out passwordHash, out passwordSalt);

            UserInfo userInfo1 = new UserInfo()
            {
                Email = "ole123@easv365.dk",
                Address = "sgvdsv",
                FirstName = "'slfåb",
                LastName = "eøpjin",
                PhoneNumber = "12345678"
            };

            LoginUser user1 = new LoginUser()
            {
                IsActivated = true,
                IsAdmin = true,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserInfo = userInfo1

            };

            UserInfo userInfo2 = new UserInfo()
            {
                Email = "ole456@easv365.dk",
                Address = "sgvdsv",
                FirstName = "'slfåb",
                LastName = "eøpjin",
                PhoneNumber = "12345678"
            };

            LoginUser user2 = new LoginUser()
            {
                IsActivated = true,
                IsAdmin = false,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserInfo = userInfo2

            };


            Category cat1 = new Category
            {
                Name = "VR Room"
            };
            Category cat2 = new Category
            {
                Name = "VR & AR Equipment"
            };
            Category cat3 = new Category
            {
                Name = "Other Equipment"
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
            List<Product> prods = new List<Product> { product1, product2, product3, product4, product5, product6, product7 };
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
            ctx.AddRange(new List<LoginUser> { user1, user2 });
            ctx.AddRange(list);
            ctx.SaveChanges();

        }

    }
}
