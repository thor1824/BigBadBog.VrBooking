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
           _authenticationHelper.CreatePasswordHash(paswordString,out passwordHash,out passwordSalt);

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



            userInfo = ctx.Add(userInfo).Entity;
            user1 = ctx.Add(user1).Entity;
            ctx.SaveChanges();

        }

    }
}
