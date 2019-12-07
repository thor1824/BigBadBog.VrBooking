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

            UserInfo userInfo = new UserInfo()
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
                UserInfo = userInfo

            };

            Category cat = new Category
            {
                Name = "Category"
            };

            Product product1 = new Product()
            {
                Name = "product",
                Category = cat,
                Description = "desciption"
            };
            DateTime s1 = new DateTime(2019, 12, 6, 15, 0, 0, DateTimeKind.Utc);
            DateTime e1 = new DateTime(2019, 12, 6, 16, 0, 0, DateTimeKind.Utc);
            DateTime s2 = new DateTime(2019, 12, 7, 13, 0, 0, DateTimeKind.Utc);
            DateTime e2 = new DateTime(2019, 12, 7, 19, 0, 0, DateTimeKind.Utc);

            List<BookingOrder> list = new List<BookingOrder>{
                new BookingOrder
                {
                    User = userInfo,
                    Product = product1,
                    StartTimeOfBooking = s1,
                    EndTimeOfBooking = e1,
                    
                },
                new BookingOrder
                {
                    User = userInfo,
                    Product = product1,
                    StartTimeOfBooking = s2,
                    EndTimeOfBooking = e2,

                }
            };

            ctx.Add(userInfo);
            ctx.Add(user1);
            ctx.AddRange(list);
            ctx.SaveChanges();

        }

    }
}
