using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;

namespace VrBooking.RestApi.WebApp.Controllers
{
    public class UserInfoWithFilterController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        public UserInfoWithFilterController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }
        [HttpPost]
        public ActionResult<List<FilterPageList<UserInfo>>> Post([FromBody] FilterPageList<UserInfo> pagefilter)
        {
            try
            {

                return Ok(_userInfoService.ReadAllWithPageFilter(pagefilter));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}