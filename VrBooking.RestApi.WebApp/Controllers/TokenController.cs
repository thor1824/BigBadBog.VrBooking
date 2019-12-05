using Microsoft.AspNetCore.Mvc;
using VrBooking.Core.ApplicationServices;
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
            Core.Entity.LoginUser user = _log.Login(model.UserNameInput);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!_authHelper.VerifyPasswordHash(model.PasswordInput, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.UserInfo.Email,
                token = _authHelper.GenerateToken(user)
            });
        }

    }
}