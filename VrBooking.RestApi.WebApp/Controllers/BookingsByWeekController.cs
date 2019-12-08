using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;
using VrBooking.RestApi.WebApp.Helper;
using VrBooking.RestApi.WebApp.Model;

namespace VrBooking.RestApi.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsByWeekController : ControllerBase
    {
        private readonly IBookingOrderService _bookingService;

        public BookingsByWeekController(IBookingOrderService bookingService)
        {
            _bookingService = bookingService;
        }

        // POST: api/Booking
        [HttpPost]
        public ActionResult<List<BookingAdapterOut>> Post([FromBody] WeekFilter week)
        {
            try
            {
                List<BookingAdapterOut> bookings = new List<BookingAdapterOut>();
                foreach (var item in _bookingService.ReadByWeek(week.WeekStart, week.WeekEnd, week.ProductId))
                {
                    bookings.Add(new BookingAdapterOut { 
                        Id = item.Id, 
                        Product = item.Product, 
                        User = item.User,
                        StartTimeOfBooking = DateConverter.FromDatetimeToUTCEpoch(item.StartTimeOfBooking),
                        EndTimeOfBooking = DateConverter.FromDatetimeToUTCEpoch(item.EndTimeOfBooking)
                    });
                }
                return Ok(bookings);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


    }
}
