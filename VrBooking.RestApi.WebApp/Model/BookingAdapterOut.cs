using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VrBooking.Core.Entity;

namespace VrBooking.RestApi.WebApp.Model
{
    public class BookingAdapterOut
    {

        public long Id { get; set; }
        public UserInfo User { get; set; }
        public Product Product { get; set; }
        public double StartTimeOfBooking { get; set; }
        public double EndTimeOfBooking { get; set; }
    }
}
