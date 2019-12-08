using System;

namespace VrBooking.Core.Entity
{
    public class BookingOrder
    {
        public long Id { get; set; }
        public UserInfo User { get; set; }
        public Product Product { get; set; }
        public DateTime StartTimeOfBooking { get; set; }
        public DateTime EndTimeOfBooking { get; set; }

    }
}
