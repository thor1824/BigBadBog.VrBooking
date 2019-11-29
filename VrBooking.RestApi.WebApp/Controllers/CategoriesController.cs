using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;

namespace VrBooking.RestApi.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service=service;
        }


        // GET: api/Category
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
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

        // GET: api/Category/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Category> Get(int id)
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

        // POST: api/Category
        [HttpPost]
        public ActionResult Post([FromBody] Category value)
        {
            try
            {
                Category category = _service.Create(value);
                return Created("" + category.Id, category);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/Category/5
        [HttpPut()]
        public ActionResult Put([FromBody] Category value)
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

        // DELETE: api/Category/5
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
