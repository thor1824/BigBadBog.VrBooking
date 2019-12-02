using VrBooking.Infrastructure;

namespace VrBooking.RestApi.WebApp.Seeder.Impl
{
    internal class DbSeeder : IDbSeeder
    {
        public void Seed(VrBookingContext ctx)
        {
            ctx.Database.EnsureCreated();
        }
    }
}