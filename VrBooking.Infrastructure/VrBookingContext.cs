using Microsoft.EntityFrameworkCore;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure
{
    public class VrBookingContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<BookingOrder> BookingOrders { get; set; }

        public DbSet<LoginUser> LoginUsers { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}
