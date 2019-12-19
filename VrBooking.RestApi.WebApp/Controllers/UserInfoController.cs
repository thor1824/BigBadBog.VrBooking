using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult<IEnumerable<UserInfo>> Get([FromQuery] int pageIndex, int itemsPrPage)
        {
            try
            {
                if (pageIndex == 0 && itemsPrPage == 0)
                {
                    return Ok(_service.ReadAll());
                }
                else
                {
                    return Ok(_service.ReadAllWithPageFilter(pageIndex, itemsPrPage));
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<UserInfo> Get(int id)
        {
            try
            {
                return Ok(_service.Read(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
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
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
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
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
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
                return BadRequest(e.Message);
            }
        }
    }
}
