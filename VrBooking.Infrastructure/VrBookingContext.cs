using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure
{
    public class VrBookingContext: DbContext
    {
        public DbSet<User> users { get; set; }

    }
}
