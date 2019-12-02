using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;

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



        // GET: api/Booking
        [HttpGet]
        public ActionResult<IEnumerable<BookingOrder>> Get()
        {
            try
            {
                return Ok(_bookingService.ReadAll());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public ActionResult<BookingOrder> Get(int id)
        {
            try
            {
                return Ok(_bookingService.Read(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: api/Booking
        [HttpPost]
        public ActionResult Post([FromBody] BookingOrder value)
        {
            try
            {
                if (value.Product != null)
                {
                    value.Product = _productService.Read(value.Product.Id);
                }
                if (value.User != null)
                {
                    value.User = _UserInfoservice.Read(value.Id);
                }
                BookingOrder booking = _bookingService.Create(value);
                return Created("" + booking.Id, booking);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/Booking/5
        [HttpPut()]
        public ActionResult Put([FromBody] BookingOrder value)
        {
            try
            {
                _bookingService.Update(value);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Booking/5
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
