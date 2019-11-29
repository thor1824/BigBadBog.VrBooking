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
        private readonly IBookingOrderService _service;

        public BookingsController(IBookingOrderService service)
        {
            _service = service;
        }


        // GET: api/Booking
        [HttpGet]
        public ActionResult<IEnumerable<BookingOrder>> Get()
        {
            try
            {
                return Ok(_service.ReadAll());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        // GET: api/Booking/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<BookingOrder> Get(int id)
        {
            try
            {
                return Ok(_service.Read(id));
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
                BookingOrder booking = _service.Create(value);
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
                _service.Update(value);
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
                _service.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
