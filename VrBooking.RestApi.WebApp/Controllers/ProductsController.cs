using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;

namespace VrBooking.RestApi.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service=service;
        }

        // GET: api/Product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
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

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Product> Get(int id)
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

        // POST: api/Product
        [HttpPost]
        public ActionResult Post([FromBody] Product value)
        {
            try
            {
                Product product = _service.Create(value);
                return Created("" + product.Id, product);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/Product/5
        [HttpPut()]
        public ActionResult Put([FromBody] Product value)
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

        // DELETE: api/Product/5
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
