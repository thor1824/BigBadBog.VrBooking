using System;
using Microsoft.AspNetCore.Mvc;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;
using VrBooking.RestApi.WebApp.Helper;
using VrBooking.RestApi.WebApp.Model;

namespace VrBooking.RestApi.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticationHelper _authHelper;
        private readonly ILoginUserService _log;

        public TokenController(ILoginUserService log, IAuthenticationHelper authHelper)
        {
            _log = log;
            _authHelper = authHelper;
        }


        [HttpPost]
        public IActionResult Login([FromBody]LoginForm model)
        {
            try
            {
                LoginUser user = _log.Login(model.UserNameInput);

                if (user == null)
                    return Unauthorized();

                // check if password is correct
                if (!_authHelper.VerifyPasswordHash(model.PasswordInput, user.PasswordHash, user.PasswordSalt))
                    return Unauthorized();

                // Authentication successful
                return Ok(new
                {
                    username = user.UserInfo,
                    token = _authHelper.GenerateToken(user)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}