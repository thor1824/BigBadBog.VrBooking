using VrBooking.Infrastructure;

namespace VrBooking.RestApi.WebApp.Seeder
{
    public interface IDbSeeder
    {
        void Seed(VrBookingContext ctx);
    }
}