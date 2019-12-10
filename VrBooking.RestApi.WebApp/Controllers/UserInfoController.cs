using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;

namespace VrBooking.RestApi.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _service;

        public UserInfoController(IUserInfoService service)
        {
            _service = service;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<UserInfo>> Get()
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

        // GET: api/User/5
        [HttpGet("{id}")]
        public ActionResult<UserInfo> Get(int id)
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

        // POST: api/User
        [HttpPost]
        public ActionResult Post([FromBody] UserInfo value)
        {
            try
            {
                UserInfo user = _service.Create(value);
                return Created("" + user.Id, user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/User/5
        [HttpPut]
        public ActionResult Put([FromBody] UserInfo value)
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

        // DELETE: api/User/5
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
