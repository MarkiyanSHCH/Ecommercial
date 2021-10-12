using Microsoft.AspNetCore.Mvc;

using Core.Services;
using Domain.Models;
using WebAPI.Models;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices _authServices;

        public AuthController(AuthServices authServices)
            => (this._authServices) = (authServices);

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login request)
        {
            Account user = _authServices.GetAccount(request.Email, request.Password);

            if (user != null)
                return Ok(new { access_token = _authServices.GenerateJWT(user) });

            return Unauthorized();
        }
    }
}