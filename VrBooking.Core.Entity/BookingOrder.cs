using System;

namespace VrBooking.Core.ApplicationServices
{
    public class BookingOrder
    {

        public long UserId { get; set; }
        public long ProductId { get; set; }
        public DateTime StartTimeOfBooking { get; set; }
        public DateTime EndTimeOfBooking { get; set; }

    }
}
