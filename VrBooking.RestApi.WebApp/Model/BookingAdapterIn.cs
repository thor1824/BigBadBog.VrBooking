using VrBooking.Core.Entity;

namespace VrBooking.RestApi.WebApp.Model
{
    public class BookingAdapterIn
    {
        public long Id { get; set; }
        public UserInfo User { get; set; }
        public Product Product { get; set; }
        public long StartTimeOfBooking { get; set; }
        public long EndTimeOfBooking { get; set; }
    }
}
