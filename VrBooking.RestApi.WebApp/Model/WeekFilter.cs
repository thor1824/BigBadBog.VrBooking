using System;

namespace VrBooking.RestApi.WebApp.Controllers
{
    public class WeekFilter
    {
        public long ProductId { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
    }
}