using Microsoft.AspNetCore.Authorization;
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
    public class BookingsController : ControllerBase
    {
        private readonly IBookingOrderService _bookingService;
        private readonly IProductService _productService;
        private readonly IUserInfoService _UserInfoservice;

        public BookingsController(IBookingOrderService bookingService, IProductService productService, IUserInfoService userInfoservice)
        {
            _bookingService = bookingService;
            _productService = productService;
            _UserInfoservice = userInfoservice;
        }



        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<BookingAdapterOut>> Get([FromQuery] long start, long end, int productId)
        {
            try
            {
                List<BookingOrder> bookings;
                if (start == 0 && end == 0)
                {
                    bookings = _bookingService.ReadAll();
                }
                else
                {
                    bookings = _bookingService.ReadByWeek(DateConverter.FromUTCEpochToDatetime(start), DateConverter.FromUTCEpochToDatetime(end), productId);
                }

                List<BookingAdapterOut> bookingsOut = new List<BookingAdapterOut>();
                foreach (BookingOrder item in bookings)
                {
                    bookingsOut.Add(new BookingAdapterOut
                    {
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

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<BookingAdapterOut> Get(int id)
        {
            try
            {
                BookingOrder booking = _bookingService.Read(id);
                return Ok(new BookingAdapterOut
                {
                    Id = booking.Id,
                    Product = booking.Product,
                    User = booking.User,
                    StartTimeOfBooking = DateConverter.FromDatetimeToUTCEpoch(booking.StartTimeOfBooking),
                    EndTimeOfBooking = DateConverter.FromDatetimeToUTCEpoch(booking.EndTimeOfBooking)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult<BookingAdapterOut> Post([FromBody] BookingAdapterIn value)
        {
            try
            {
                BookingOrder booking = _bookingService.Create(new BookingOrder
                {
                    Id = value.Id,
                    Product = _productService.Read(value.Product.Id),
                    User = _UserInfoservice.Read(value.User.Id),
                    StartTimeOfBooking = DateConverter.FromUTCEpochToDatetime(value.StartTimeOfBooking),
                    EndTimeOfBooking = DateConverter.FromUTCEpochToDatetime(value.EndTimeOfBooking)
                });
                return Created("" + booking.Id, new BookingAdapterOut
                {
                    Id = booking.Id,
                    Product = booking.Product,
                    User = booking.User,
                    StartTimeOfBooking = DateConverter.FromDatetimeToUTCEpoch(booking.StartTimeOfBooking),
                    EndTimeOfBooking = DateConverter.FromDatetimeToUTCEpoch(booking.EndTimeOfBooking)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut()]
        public ActionResult Put([FromBody] BookingAdapterIn value)
        {
            try
            {
                _bookingService.Update(new BookingOrder
                {
                    Id = value.Id,
                    Product = _productService.Read(value.Product.Id),
                    User = _UserInfoservice.Read(value.User.Id),
                    StartTimeOfBooking = DateConverter.FromUTCEpochToDatetime(value.StartTimeOfBooking),
                    EndTimeOfBooking = DateConverter.FromUTCEpochToDatetime(value.EndTimeOfBooking)
                });
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _bookingService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
