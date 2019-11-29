using System;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public class BookingOrder
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public DateTime StartTimeOfBooking { get; set; }
        public DateTime EndTimeOfBooking { get; set; }

    }
}
