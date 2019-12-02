using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure
{
    public class VrBookingContext: DbContext
    {
        public DbSet<User> users { get; set; }

        public DbSet<Product> products { get; set; }

        public DbSet<LoginUser> loginUsers { get; set; }

        public DbSet<BookingOrder> BookingOrders { get; set; }

    }
}
