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

           string paswordString1 = "123456";
           string paswordString2 = "123456";
           byte[] passwordHashThor, passwordSaltThor;
           _authenticationHelper.CreatePasswordHash(paswordString1,out passwordHashThor,out passwordSaltThor);
           byte[] passwordHashOle, passwordSaltOle;
           _authenticationHelper.CreatePasswordHash(paswordString2,out passwordHashOle,out passwordSaltOle);


           UserInfo userInfo1 = new UserInfo()
           {
               Email = "ole123@easv365.dk",
               Address = "sgvdsv",
               FirstName = "'slfåb",
               LastName = "eøpjin",
               PhoneNumber = "12345678"
           };

           UserInfo userInfo2 = new UserInfo()
           {
               Address = "BjørneBy",
               Email = "thor666@easv365.dk",
               FirstName = "Thor",
               LastName = "Bjørn",
               PhoneNumber = "12345678"
           };

           LoginUser loginUser2 = new LoginUser()
           {
               IsActivated = true,
               IsAdmin = false,
               PasswordHash = passwordHashThor,
               PasswordSalt = passwordSaltThor,
               UserInfo = userInfo2,
           };

            LoginUser loginUser1 = new LoginUser()
            {
                IsActivated = true,
                IsAdmin = true,
                PasswordHash = passwordHashOle,
                PasswordSalt = passwordSaltOle,
                UserInfo = userInfo1

            };




            userInfo2 = ctx.Add(userInfo2).Entity;
            userInfo1 = ctx.Add(userInfo1).Entity;
            loginUser2 = ctx.Add(loginUser2).Entity;
            loginUser1 = ctx.Add(loginUser1).Entity;
            
           
            ctx.SaveChanges();

        }

    }
}
